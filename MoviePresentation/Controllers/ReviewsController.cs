// Ignore Spelling: Dto

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCore.Models.DTOs.ReviewDTOs;
using Services.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace MovieApi.Controllers
{
	[Route("api/reviews")]
	[ApiController]
	public class ReviewsController : ControllerBase
	{
		private readonly IServiceManager _serviceManager;

		public ReviewsController(IServiceManager serviceManager)
		{
			_serviceManager = serviceManager;
		}

		// GET: api/movie/5/reviews
		/// <summary>
		/// Retrieves all reviews associated with a specific movie.
		/// </summary>
		/// <param name="movieId">The ID of the movie.</param>
		/// <returns>A list of reviews for the specified movie.</returns>
		/// <response code="200">Returns the list of review DTOs.</response>
		/// <response code="404">If the movie with the specified ID does not exist.</response>
		[HttpGet("/api/movie/{movieId}/reviews")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDto>))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
		[SwaggerOperation(
			Summary = "Get all reviews for a movie",
			Description = "Retrieves a list of reviews associated with a given movie ID."
		)]
		public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int movieId) => 
			Ok(await _serviceManager.ReviewServices.GetAllReviewsAsync(movieId));

		/// <summary>
		/// Adds a new review for a movie.
		/// </summary>
		/// <param name="reviewCreateDto">
		/// The data required to create a new review, including reviewer name, rating, 
		/// comment, and associated movie ID.</param>
		/// <returns>
		/// A <see cref="ReviewDto"/> representing the newly created review.
		/// </returns>
		/// <response code="201">The review was successfully created.</response>
		/// <response code="400">The input data was invalid, the movie was not found or max review numbers exceed max allowed.</response>
		[HttpPost]
		[ProducesResponseType(typeof(ReviewDto), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[SwaggerOperation(
			Summary = "Add a new movie review.",
			Description = "Creates a new review for an existing movie, including reviewer name, rating, and comment."
		)]
		public async Task<ActionResult<ReviewDto>> PostReview(ReviewCreateDto reviewCreateDto)
		{
			ReviewDto actionResult = await _serviceManager.ReviewServices.AddReview(reviewCreateDto);
			return Created();
		}

		/// <summary>
		/// Deletes a review identified by its unique ID.
		/// </summary>
		/// <param name="reviewId">The ID of the review to delete.</param>
		/// <returns>
		/// A <see cref="NoContentResult"/> (HTTP 204) if the deletion is successful.
		/// Returns <see cref="NotFoundResult"/> (HTTP 404) if the review does not exist.
		/// </returns>
		/// <response code="204">The review was successfully deleted.</response>
		/// <response code="404">No review with the specified ID was found.</response>
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
		[SwaggerOperation(
			Summary = "Delete a review by ID",
			Description =	"Deletes an existing review based on its unique identifier. Returns 204 No " +
							"Content if successful, or 404 Not Found if the review does not exist."
		)]
		public async Task<IActionResult> DeleteReview(int reviewId)
		{
			await _serviceManager.ReviewServices.RemoveReviewAsync(reviewId);
			return NoContent();
		}
	}
}
