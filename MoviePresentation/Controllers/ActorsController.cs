// Ignore Spelling: Dto

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCore.Models.DTOs.ActorDTOs;
using MovieCore.Models.DTOs.MovieActorDto;
using Services.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace MovieApi.Controllers
{
	[Route("api/actors")]
	[ApiController]
	public class ActorsController : ControllerBase
	{
		private readonly IServiceManager _serviceManager;

		public ActorsController(IServiceManager serviceManager)
		{
			_serviceManager = serviceManager;
		}

		// GET /api/actors?pageSize=20&page=3
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors(
			[FromQuery]int pageSize,
			[FromQuery]int page)
		{
			var (actorDtos, paginationMetaData) =
				await _serviceManager.ActorServices.GetAllActorsAsync(pageSize, page);

			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

			return Ok(actorDtos);
		}

		// GET /api/actors/5
		/// <summary>Retrieves data associated with a single actor linked by id. Data includes Id, Name and BirthYear.</summary>
		/// <param name="id">Id of the actor to retrieve data for.</param>
		/// <returns>
		/// A <see cref="ActorDto"/> containing information about the specified actor, or a 
		/// <see cref="ProblemDetails"/> object if the actor is not found.
		/// </returns>
		/// <response code="200">Returned the actor data requested.</response>
		/// <response code="400">Invalid actor Id was provided.</response>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ActorDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[SwaggerOperation(
			Summary = "Gets data related to a single actor.",
			Description = "Retrieves data linked to an actor. Includes actor name, birth year and id"
		)]
		public async Task<ActionResult<ActorDto>> GetActor(int id) =>	
			Ok(await _serviceManager.ActorServices.GetActorAsync(id));
		

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
		[HttpPost("/api/movie/{movieId}/actors")]
		public async Task<IActionResult> PostLinkMovieAndActor(
			[FromBody] MovieActorCreateDto movieActorCreateDto,
			[FromRoute] int movieId)
		{
			await _serviceManager.ActorServices.LinkMovieAndActorAsync(movieActorCreateDto, movieId);

			return NoContent();
		}
	}
}
