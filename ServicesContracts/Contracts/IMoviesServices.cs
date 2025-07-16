using MovieCore.Models.DTOs.MovieDtos;

namespace Services.Contracts.Contracts;

/// <summary>
/// Provides service-level operations related to movie entities, acting as an intermediary between the controller and data access layers.
/// </summary>
public interface IMoviesServices
{
	/// <summary>
	/// Retrieves a collection of all movies with their associated genre information.
	/// </summary>
	/// <returns>A collection of <see cref="MovieWithGenreDto"/>.</returns>
	Task<IEnumerable<MovieWithGenreDto>> GetAllMoviesAsync();

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
	/// Updates an existing movie with new title, year, duration, and genre.
	/// </summary>
	/// <param name="id">The ID of the movie to update.</param>
	/// <param name="movieWithGenreIdUpdateDto">The updated movie data.</param>
	/// <returns><c>true</c> if the update was successful.</returns>
	Task<bool> UpdateMovieAsync(int id, MovieWithGenreIdUpdateDto movieWithGenreIdUpdateDto);

	/// <summary>
	/// Removes a movie by its ID.
	/// </summary>
	/// <param name="id">The ID of the movie to delete.</param>
	/// <returns><c>true</c> if the movie was successfully deleted.</returns>
	Task<bool> RemoveMovieAsync(int id);
}
