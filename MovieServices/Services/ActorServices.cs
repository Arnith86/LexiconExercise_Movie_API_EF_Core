// Ignore Spelling: Dto

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.DomainContracts.RequestParameters;
using MovieCore.Models.DTOs.ActorDTOs;
using MovieCore.Models.DTOs.MovieActorDto;
using MovieCore.Models.Entities;
using MovieCore.Models.Exceptions;
using MovieCore.Models.Exceptions.BusinessRuleViolationExceptions;
using Services.Contracts;
using ServicesContracts.Contracts;
using System.Diagnostics;

namespace MovieServices.Services;

/// <summary>
/// Provides functionality for managing associations between movies and actors,
/// such as linking an actor to a specific movie. Implements the <see cref="IActorServices"/> interface.
/// </summary>
public class ActorServices : IActorServices
{
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;

	public ActorServices(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}

	/// <inheritdoc/>
	public async Task<(ActorDto actorDto, int actorId)> AddActorAsync (ActorCreateDto actorCreateDto)
	{
		Actor actor = _mapper.Map<Actor>(actorCreateDto);

		_unitOfWork.Actors.Add(actor);
		await _unitOfWork.CompleteAsync();

		return (_mapper.Map<ActorDto>(actor), actor.Id);
	}

	/// <inheritdoc/>
	public async Task<ActorDto> GetActorAsync(int id)
	{
		var actor = await _unitOfWork.Actors.GetActorAsync(id);

		if (actor is null) throw new ActorNotFoundException(id);

		return _mapper.Map<ActorDto>(actor);
	}

	/// <inheritdoc/>
	public async Task<(IEnumerable<ActorDto> actorDtos, IPaginationMetaData metaData)> GetAllActorsAsync(
		ActorRequestParameters requestParameters,
		bool trackChanges = false)
	{
		var actorsDtosWithMetaDate = await _unitOfWork.Actors.GetAllActorsAsync(requestParameters, trackChanges);
		var actorDtos = _mapper.Map<IEnumerable<ActorDto>>(actorsDtosWithMetaDate.Items);

		return (actorDtos, actorsDtosWithMetaDate.MetaData);
	}

	/// <inheritdoc/>
	public async Task<bool> LinkMovieAndActorAsync(MovieActorCreateDto movieActorCreateDto, int movieId)
	{
		int actorId = movieActorCreateDto.ActorId;

		var movie = await _unitOfWork.Movies.GetMovieAsync(movieId, changeTracker: true);

		if (movie is null)
			throw new MovieNotFoundException(movieId);

		bool actorExists = await _unitOfWork.Actors.AnyAsync(actorId);

		if (!actorExists)
			throw new ActorNotFoundException(actorId);

		await ValidatingActorBusienessRules(actorId, movieId, movie);

		movie.MovieActors.Add(_mapper.Map<MovieActor>(movieActorCreateDto));

		await _unitOfWork.CompleteAsync();

		return true;
	}

	// ToDo: Extract ValidatingActorBusienessRules to its own class
	private async Task ValidatingActorBusienessRules(int actorId, int movieId, VideoMovie movie)
	{
		if (await IsAlreadyAssignedToMovie(actorId, movieId))
			throw new DuplicateActorAssignmentException(actorId, movieId);

		// Documentary has a actor limit of 10
		int actorCount = 
			await _unitOfWork.Movies.CountActorsByMovieAndGenreAsync(movieId, movie.MoviesGenre!.Genre);
		
		if (actorCount > 9)	throw new MaximumActorsReachedException(movieId);
	}

	private Task<bool> IsAlreadyAssignedToMovie( int actorId,int movieId) =>
		_unitOfWork.Actors.IsActorAssignedToMovie(actorId, movieId);
	

	/// <inheritdoc/>
	public async Task<bool> UpdateActorAsync(int actorId, ActorUpdateDto actorUpdateDto)
	{
		Actor? actor = await _unitOfWork.Actors.GetActorAsync(actorId, changeTracker: true);

		if (actor is null) throw new ActorNotFoundException(actorId);

		_mapper.Map(actorUpdateDto, actor);

		try
		{
			await _unitOfWork.CompleteAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!await _unitOfWork.Actors.AnyAsync(actorId)) throw new ActorNotFoundException(actorId);
			else throw;
		}

		return true;
	}
}
