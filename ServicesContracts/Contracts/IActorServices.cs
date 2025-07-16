using MovieCore.Models.DTOs.MovieActorDto;

namespace ServicesContracts.Contracts;

/// <summary>
/// Provides service-level operations related to actor entities, acting as an intermediary between the controller and data access layers.
/// </summary>
public interface IActorServices
{
	/// <summary>
	/// Links an actor to a movie by creating an association between them.
	/// </summary>
	/// <param name="movieActorCreateDto">The data transfer object containing the actor's ID.</param>
	/// <param name="movieId">The ID of the movie to associate the actor with.</param>
	/// <returns>A task representing the asynchronous operation. Returns <c>true</c> if the association was successful.</returns>
	Task<bool> LinkMovieAndActorAsync(MovieActorCreateDto movieActorCreateDto, int movieId);
}
