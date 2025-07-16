// Ignore Spelling: Dto

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCore.Models.DTOs.MovieDtos;
using Services.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace MovieApi.Controllers;

[Route("api/movie")]
[ApiController]
public class MoviesController : ControllerBase
{
	private readonly IServiceManager _serviceManager;

	public MoviesController(IServiceManager serviceManager)
	{
		_serviceManager = serviceManager;
	}


	// GET: api/Movies
	/// <summary>
	/// Retrieves all registered movies with basic details and their associated genre.
	/// </summary>
	/// <remarks>
	/// This endpoint returns a simplified list of movies. Each movie includes:
	/// Id, Title, Genre (as a string), Duration, Release Year
	/// </remarks>
	/// <returns>A list of movies with basic information and genre.</returns>
	/// <response code="200">Returns the list of movies successfully.</response>
	[HttpGet]
	[SwaggerOperation(
		Summary = "Retrieve all movies",
		Description = "Returns a simplified list of all registered movies including basic details and genre.")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MovieWithGenreDto>))]
	public async Task<ActionResult<IEnumerable<MovieWithGenreDto>>> GetMovies() =>
			Ok(await _serviceManager.MovieServices.GetAllMoviesAsync());


	// GET: api/Movies/5
	/// <summary>
	/// Retrieves a simplified representation of a specific movie by its ID.
	/// </summary>
	/// <param name="id">The unique identifier of the movie.</param>
	/// <remarks>
	/// Returns basic movie details including:
	/// Id, Title, Genre (as a string), Duration, Release Year
	/// </remarks>
	/// <returns>The requested movie if found; otherwise, a 404 Not Found response.</returns>
	/// <response code="200">Returns the requested movie.</response>
	/// <response code="404">If no movie with the specified ID exists.</response>
	[HttpGet("{id}")]
	[SwaggerOperation(
		Summary = "Get a specific movie by ID",
		Description = "Returns simplified movie details for the movie with the given ID, including genre info.")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieWithGenreDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<MovieWithGenreDto>> GetMovie(int id) =>
		Ok(await _serviceManager.MovieServices.GetMovieAsync(id));


	// GET: api/Movies/5/details
	/// <summary>
	/// Retrieves a simplified representation of a specific movie by its ID, with more detailed information.
	/// </summary>
	/// <param name="id">The unique identifier of the movie.</param>
	/// <remarks>
	/// Returns movie details including:
	/// - Id, Title, Genre (as a string), Duration, Release Year
	/// - Synopsis, Language and Budget
	/// </remarks>
	/// <returns>The requested movie if found; otherwise, a 404 Not Found response.</returns>
	/// <response code="200">Returns the requested movie.</response>
	/// <response code="404">If no movie with the specified ID exists.</response>
	[HttpGet("{id}/descriptive")]
	[SwaggerOperation(
		Summary = "Get a specific movie by ID",
		Description = "Returns movie details for the movie with the given ID, including genre info and details.")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieWithGenreDetailsDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<MovieWithGenreDetailsDto>> GetMovieDetails(int id) =>
		Ok(await _serviceManager.MovieServices.GetMovieDetailsAsync(id));



	// GET: api/Movies/5/details
	/// <summary>
	/// Retrieves detailed information about a specific movie, including its genre, synopsis, budget, language, reviews, and associated actors.
	/// </summary>
	/// <param name="id">The ID of the movie to retrieve details for.</param>
	/// <returns>
	/// Returns a <see cref="MovieDetailDto"/> containing detailed information about the specified movie,
	/// or a <see cref="ProblemDetails"/> object if the movie is not found.
	/// </returns>
	/// <response code="200">Returns the full movie details.</response>
	/// <response code="404">No movie with the specified ID was found.</response>
	[HttpGet("{id}/details")]
	[ProducesResponseType(typeof(MovieDetailDto), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[SwaggerOperation(
		Summary = "Get full movie details",
		Description = "Retrieves a movie by ID, including its genre, synopsis, budget, language, reviews, and actors."
	)]
	public async Task<ActionResult<MovieDetailDto>> GetMovieFullDetails(int id)
		=> Ok(await _serviceManager.MovieServices.GetMovieFullDetailsAsync(id));



	// POST: api/Movies
	/// <summary>
	/// Creates a new movie with basic information such as title, year, duration, and genre.
	/// </summary>
	/// <param name="genreId">The ID of the genre to associate with the new movie. Must refer to an existing genre.</param>
	/// <param name="movieCreateDto">The data used to create the movie.</param>
	/// <returns>The created movie's basic information including the associated genre ID.</returns>
	/// <response code="201">Returns the created movie with genre ID.</response>
	/// <response code="400">Returned if the specified genre does not exist or the request is invalid.</response>
	[HttpPost]
	[SwaggerOperation(
		Summary = "Create a new movie.",
		Description =	"Adds a new movie to the database using basic details like title, year, " +
						"and duration. Requires a valid genre ID to associate the movie with."
	)]
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieWithGenreIdDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
	public async Task<ActionResult<MovieWithGenreIdDto>> PostMovie(MovieCreateDto movieCreateDto)
	{
		var (movieWithGenreIdDto, movieId) =  await _serviceManager.MovieServices.AddMovieAsync(movieCreateDto);

		return CreatedAtAction("GetMovie", new { id = movieId}, movieWithGenreIdDto);
	}


	// PUT: api/Movies/5
	/// <summary>
	/// Updates an existing movie with new title, year, duration, and genre.
	/// </summary>
	/// <param name="id">The ID of the movie to update.</param>
	/// <param name="movieWithGenreIdUpdateDto">The updated movie data.</param>
	/// <returns>No content on success; NotFound if the movie is not found; error if concurrency conflict occurs.</returns>
	/// <response code="204">The movie was successfully updated.</response>
	/// <response code="404">No movie with the specified ID was found.</response>
	/// <response code="400">No genre with the specified MovieGenreIdwas found.</response>
	[HttpPut("{id}")]
	[SwaggerOperation(
	Summary = "Update an existing movie.",
	Description = "Updates an existing movie's title, year, duration, and associated genre. Requires the movie ID and the updated data."
)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
	public async Task<IActionResult> PutMovie(int id, MovieWithGenreIdUpdateDto movieWithGenreIdUpdateDto)
	{
		await _serviceManager.MovieServices.UpdateMovieAsync(id, movieWithGenreIdUpdateDto);
		return NoContent();
	}


	// DELETE: api/Movies/5
	/// <summary>
	/// Deletes a specific movie by its ID.
	/// </summary>
	/// <param name="id">The ID of the movie to delete.</param>
	/// <returns>No content if deletion is successful; otherwise, a 404 error if the movie is not found.</returns>
	/// <response code="204">Movie was successfully deleted.</response>
	/// <response code="404">Movie with the specified ID was not found.</response>
	[HttpDelete("{id}")]
	[SwaggerOperation(
	Summary = "Delete a movie by ID.",
	Description = "Removes a movie from the database using the provided ID. " +
				  "Returns a 404 Not Found response if the movie does not exist."
	)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
	public async Task<IActionResult> DeleteMovie(int id)
	{
		await _serviceManager.MovieServices.RemoveMovieAsync(id);
		return NoContent();
	}
}
