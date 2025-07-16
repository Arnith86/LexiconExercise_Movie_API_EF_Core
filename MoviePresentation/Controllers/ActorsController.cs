// Ignore Spelling: Dto

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCore.Models.DTOs.MovieActorDto;
using Services.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace MovieApi.Controllers
{
	[Route("api/movie/{movieId}/actors")]
	[ApiController]
	public class ActorsController : ControllerBase
	{
		private readonly IServiceManager _serviceManager;

		public ActorsController(IServiceManager serviceManager)
		{
			_serviceManager = serviceManager;
		}


		// POST /api/movies/5/actors
		/// <summary>
		/// Associates an existing actor with an existing movie, specifying their role.
		/// </summary>
		/// <param name="movieId">The ID of the movie to which the actor should be added.</param>
		/// <param name="movieActorCreateDto">The actor ID and their role in the movie.</param>
		/// <returns>No content on success; BadRequest if movie or actor ID is invalid.</returns>
		/// <response code="204">The actor was successfully associated with the movie.</response>
		/// <response code="400">Invalid movie or actor ID was provided.</response>
		[SwaggerOperation(
			Summary = "Add an actor to a movie.",
			Description = "Associates an existing actor with an existing movie by specifying their role. " +
						  "Requires a valid movie ID and actor ID."
		)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
		[HttpPost]
		public async Task<IActionResult> PostLinkMovieAndActor(
			[FromBody] MovieActorCreateDto movieActorCreateDto,
			[FromRoute] int movieId)
		{
			await _serviceManager.ActorServices.LinkMovieAndActorAsync(movieActorCreateDto, movieId);

			return NoContent();
		}
	}
}
