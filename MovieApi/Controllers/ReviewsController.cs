using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.DTOs.ReviewDTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace MovieApi.Controllers
{
	[Route("api/review")]
	[ApiController]
	public class ReviewsController : ControllerBase
	{
		private readonly MovieApiContext _context;

		public ReviewsController(MovieApiContext context)
		{
			_context = context;
		}

		// GET: api/movie/5/reviews
		/// <summary>
		/// Retrieves all reviews associated with a specific movie.
		/// </summary>
		/// <param name="movieId">The ID of the movie.</param>
		/// <returns>A list of reviews for the specified movie.</returns>
		/// <response code="200">Returns the list of review DTOs.</response>
		/// <response code="404">If the movie with the specified ID does not exist.</response>
		[Route("api/movie/{movieId}/reviews")]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDto>))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
		[SwaggerOperation(
			Summary = "Get all reviews for a movie",
			Description = "Retrieves a list of reviews associated with a given movie ID."
		)]
		public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int movieId)
		{
			var movieExists = await _context.Movies.AnyAsync(m => m.Id == movieId);

			if (movieExists == false)
			{
				return Problem(
					statusCode: StatusCodes.Status404NotFound,
					title: "Invalid movie ID",
					detail: $"No movie with ID {movieId} was found.",
					instance: HttpContext.Request.Path
				);
			}

			// ToDo: use automapper
			IEnumerable<ReviewDto> movieReview = await _context.Reviews
				.Where(r => r.MovieId == movieId)
				.Select(r => new ReviewDto
				{
					Id = r.Id,
					ReviewerName = r.ReviewerName,
					Comment = r.Comment,
					Rating = r.Rating
				}).ToListAsync();

			return Ok(movieReview);
		}

		//	// GET: api/Reviews/5
		//	[HttpGet("{id}")]
		//	public async Task<ActionResult<Review>> GetReview(int id)
		//	{
		//		var review = await _context.Reviews.FindAsync(id);

		//		if (review == null)
		//		{
		//			return NotFound();
		//		}

		//		return review;
		//	}

		//	// PUT: api/Reviews/5
		//	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		//	[HttpPut("{id}")]
		//	public async Task<IActionResult> PutReview(int id, Review review)
		//	{
		//		if (id != review.Id)
		//		{
		//			return BadRequest();
		//		}

		//		_context.Entry(review).State = EntityState.Modified;

		//		try
		//		{
		//			await _context.SaveChangesAsync();
		//		}
		//		catch (DbUpdateConcurrencyException)
		//		{
		//			if (!ReviewExists(id))
		//			{
		//				return NotFound();
		//			}
		//			else
		//			{
		//				throw;
		//			}
		//		}

		//		return NoContent();
		//	}

		//	// POST: api/Reviews
		//	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		//	[HttpPost]
		//	public async Task<ActionResult<Review>> PostReview(Review review)
		//	{
		//		_context.Reviews.Add(review);
		//		await _context.SaveChangesAsync();

		//		return CreatedAtAction("GetReview", new { id = review.Id }, review);
		//	}

		//	// DELETE: api/Reviews/5
		//	[HttpDelete("{id}")]
		//	public async Task<IActionResult> DeleteReview(int id)
		//	{
		//		var review = await _context.Reviews.FindAsync(id);
		//		if (review == null)
		//		{
		//			return NotFound();
		//		}

		//		_context.Reviews.Remove(review);
		//		await _context.SaveChangesAsync();

		//		return NoContent();
		//	}

		//	private bool ReviewExists(int id)
		//	{
		//		return _context.Reviews.Any(e => e.Id == id);
		//	}
	}
}
