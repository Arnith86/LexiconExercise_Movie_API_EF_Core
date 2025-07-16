// Ignore Spelling: Dto

using AutoMapper;
using MovieCore.DomainContracts;
using MovieCore.Models.DTOs.MovieActorDto;
using MovieCore.Models.Entities;
using ServicesContracts.Contracts;
using System.Net.Http;

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
		{
			//return Problem(
			//	statusCode: StatusCodes.Status400BadRequest,
			//	title: "Invalid movie ID",
			//	detail: $"No movie with ID {movieId} was found.",
			//	instance: HttpContext.Request.Path
			//);

			// ToDo : Create a custom exception and handle this exception in program.cs 
			throw new ArgumentNullException(nameof(movie), $"No movie with ID {movieId} was found.");
		}

		bool actorExists = await _unitOfWork.Actors.AnyAsync(movieActorCreateDto.ActorId);

		if (!actorExists)
		{
			//return Problem(
			//	statusCode: StatusCodes.Status400BadRequest,
			//	title: "Invalid actor ID",
			//	detail: $"No actor with ID {movieId} was found.",
			//	instance: HttpContext.Request.Path
			//);

			// ToDo : Create a custom exception and handle this exception in program.cs 
			throw new ArgumentNullException($"No actor with ID {movieId} was found.");
		}

		MovieActor movieActor = _mapper.Map<MovieActor>(movieActorCreateDto);

		movie.MovieActors.Add(_mapper.Map<MovieActor>(movieActorCreateDto));


		await _unitOfWork.CompleteAsync();

		return true;
	}
}
