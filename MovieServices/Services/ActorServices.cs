// Ignore Spelling: Dto

using AutoMapper;
using MovieCore.DomainContracts;
using MovieCore.Models.DTOs.MovieActorDto;
using MovieCore.Models.Entities;
using MovieCore.Models.Exceptions;
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
}
