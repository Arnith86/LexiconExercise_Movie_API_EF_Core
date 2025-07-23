// Ignore Spelling: Dto

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.DomainContracts.RequestParameters;
using MovieCore.Models.DTOs.ActorDTOs;
using MovieCore.Models.DTOs.MovieActorDto;
using MovieCore.Models.Entities;
using MovieCore.Models.Exceptions;
using Services.Contracts;
using ServicesContracts.Contracts;

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
		var movie = await _unitOfWork.Movies.GetMovieAsync(movieId, changeTracker: true);

		if (movie is null)
			throw new MovieNotFoundException(movieId);

		bool actorExists = await _unitOfWork.Actors.AnyAsync(movieActorCreateDto.ActorId);

		if (!actorExists)
			throw new ActorNotFoundException(movieActorCreateDto.ActorId);

		MovieActor movieActor = _mapper.Map<MovieActor>(movieActorCreateDto);

		movie.MovieActors.Add(_mapper.Map<MovieActor>(movieActorCreateDto));

		await _unitOfWork.CompleteAsync();

		return true;
	}

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
