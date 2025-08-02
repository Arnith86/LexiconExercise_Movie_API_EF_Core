using Microsoft.AspNetCore.JsonPatch;
using MovieCore.DomainContracts.RequestParameters;
using MovieCore.Models.DTOs.MovieDtos;

namespace Services.Contracts.Contracts;

/// <summary>
/// Provides service-level operations related to movie entities, acting as an intermediary between the controller and data access layers.
/// </summary>
public interface IMoviesServices
{

	/// <summary>
	/// Retrieves a paginated collection of movies with associated genre information.
	/// </summary>
	/// <param name="requestParameters">Parameters specifying pagination settings, such as page number and page size.</param>
	/// <param name="trackChanges">Indicates whether to track changes in the underlying entities (default is <c>false</c>).</param>
	/// <returns>
	/// A tuple containing a collection of <see cref="MovieWithGenreDto"/> and 
	/// pagination meta-data implementing <see cref="IPaginationMetaData"/>.
	/// </returns>
	Task<(IEnumerable<MovieWithGenreDto> moviesWithGenreDto, IPaginationMetaData metaData)> GetAllMoviesAsync(
		MovieRequestParameters requestParameters,
		bool trackChanges = false);

	/// <summary>
	/// Retrieves a single movie by its ID, including its genre information.
	/// </summary>
	/// <param name="id">The ID of the movie to retrieve.</param>
	/// <returns>A <see cref="MovieWithGenreDto"/> if found.</returns>
	Task<MovieWithGenreDto> GetMovieAsync(int id);

	/// <summary>
	/// Retrieves detailed information for a specific movie, including synopsis, language, and budget.
	/// </summary>
	/// <param name="id">The ID of the movie.</param>
	/// <returns>A <see cref="MovieWithGenreDetailsDto"/> if found.</returns>
	Task<MovieWithGenreDetailsDto> GetMovieDetailsAsync(int id);

	/// <summary>
	/// Retrieves all movie data including genre, reviews, actors and movie details.
	/// </summary>
	/// <param name="id">The ID of the movie.</param>
	/// <returns>A <see cref="MovieDetailDto"/> with all movie details if found.</returns>
	Task<MovieDetailDto?> GetMovieFullDetailsAsync(int id);

	/// <summary>
	/// Adds a new movie to the database.
	/// </summary>
	/// <param name="movieCreateDto">The DTO containing movie creation data.</param>
	/// <returns>
	/// A tuple where:
	/// - <see cref="MovieWithGenreIdDto"/> is the created movie's basic data.
	/// - <c>int</c> is the generated movie ID.
	/// </returns>
	Task<(MovieWithGenreIdDto mwgiDto, int movieId)> AddMovieAsync(MovieCreateDto movieCreateDto);

	/// <summary>
	/// Associate the supplied movieDetails with specified movie.
	/// </summary>
	/// <param name="movieDetailsCreateDto">
	/// Contains the movie details data, and the id of movie to associate with.
	/// </param>
	/// <returns>True, if linked successfully.</returns>
	Task<bool> LinkMovieAndMovieDetailsAsync(MovieDetailsCreateDto movieDetailsCreateDto);


	/// <summary>
	/// Updates an existing movie with new title, year, duration, and genre.
	/// </summary>
	/// <param name="id">The ID of the movie to update.</param>
	/// <param name="movieWithGenreIdUpdateDto">The updated movie data.</param>
	/// <returns><c>true</c> if the update was successful.</returns>
	Task<bool> UpdateMovieAsync(int id, MovieWithGenreIdUpdateDto movieWithGenreIdUpdateDto);

	/// <summary>
	/// Gets a PatchDto representing a movie with Genre and movie details. 
	/// </summary>
	/// <param name="movieId">The ID of the movie to get.</param>
	/// <returns>True, if updating operation was successful.</returns>
	Task<MovieWithMovieDetailsPatchDto> GetMovieWithMovieDetailsPatchDtoAsync(int movieId);

	/// <summary>
	/// Applies a patch to a movie and its associated details using the provided patch DTO.
	/// </summary>
	/// <param name="movieId">The ID of the movie to update.</param>
	/// <param name="patchDto">The patch data containing the updated fields.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	Task ApplyMovieWithDetailsPatchAsync(int movieId, MovieWithMovieDetailsPatchDto patchDto);

	/// <summary>
	/// Removes a movie by its ID.
	/// </summary>
	/// <param name="id">The ID of the movie to delete.</param>
	/// <returns><c>true</c> if the movie was successfully deleted.</returns>
	Task<bool> RemoveMovieAsync(int id);
}
