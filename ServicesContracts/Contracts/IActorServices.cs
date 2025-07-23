using MovieCore.DomainContracts.RequestParameters;
using MovieCore.Models.DTOs.ActorDTOs;
using MovieCore.Models.DTOs.MovieActorDto;



namespace ServicesContracts.Contracts;

/// <summary>
/// Provides service-level operations related to actor entities, acting as an intermediary between the controller and data access layers.
/// </summary>
public interface IActorServices
{
	/// <summary>
	/// Retrieves a single actor instance that matches the supplied id.
	/// </summary>
	/// <param name="id">The id of the actor to retrieve.</param>
	/// <returns>A <see cref="ActorDto"/> if found, otherwise null.</returns>
	Task<ActorDto> GetActorAsync(int id);

	/// <summary>
	/// Retrieves a paginated collection of actors with or without associated movies and with optional change tracking.
	/// </summary>
	/// <param name="requestParameters">Parameters specifying pagination settings, such as page number and page size and movie retrieval.</param>
	/// <param name="trackChanges">Indicates whether to track changes in the underlying entities (default is <c>false</c>).</param>
	/// <returns>
	/// A tuple containing a collection of <see cref="ActorDto"/> and 
	/// pagination metadata implementing <see cref="IPaginationMetaData"/>.
	/// </returns>
	Task<(IEnumerable<ActorDto> actorDtos, IPaginationMetaData metaData)> GetAllActorsAsync(
		ActorRequestParameters requestParameters,
		bool trackChanges = false);

	/// <summary>
	/// Creates a new actor entity based on the provided data.
	/// </summary>
	/// <param name="actorCreateDto">The data used to create the actor.</param>
	/// <param name="trackChanges">Indicates whether change tracking should be enabled during creation.</param>
	/// <returns>A <see cref="ActorDto"/> representing the newly created actor.</returns>
	Task<(ActorDto actorDto, int actorId)> AddActorAsync(ActorCreateDto actorCreateDto); 


	/// <summary>
	/// Links an actor to a movie by creating an association between them.
	/// </summary>
	/// <param name="movieActorCreateDto">The data transfer object containing the actor's ID.</param>
	/// <param name="movieId">The ID of the movie to associate the actor with.</param>
	/// <returns>A task representing the asynchronous operation. Returns <c>true</c> if the association was successful.</returns>
	Task<bool> LinkMovieAndActorAsync(MovieActorCreateDto movieActorCreateDto, int movieId);

	/// <summary>
	/// Updates an existing actor's information based on the provided actor ID and updated data.
	/// </summary>
	/// <param name="actorId">The ID of the actor to update.</param>
	/// <param name="actorUpdateDto">The updated actor data, including name and birth year.</param>
	/// <returns>
	/// A boolean indicating whether the update was successful.
	/// Returns <c>true</c> if the actor was found and updated; otherwise, <c>false</c>.
	/// </returns>
	Task<bool> UpdateActorAsync(int actorId, ActorUpdateDto actorUpdateDto);
}
