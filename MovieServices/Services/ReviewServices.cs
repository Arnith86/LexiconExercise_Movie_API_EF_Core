using AutoMapper;
using MovieCore.DomainContracts;
using MovieCore.Models.DTOs.ReviewDTOs;
using ServicesContracts.Contracts;

namespace MovieServices.Services;

/// <summary>
/// Provides review-related operations for movies, such as retrieving all reviews associated with 
/// a specific movie. Implements the <see cref="IReviewServices"/> interface.
/// </summary>
public class ReviewServices : IReviewServices
{
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;

	public ReviewServices(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<ReviewDto>> GetAllReviews(int movieId)
	{
		var movieExists = await _unitOfWork.Movies.AnyAsync(movieId);

		if (movieExists == false)
		{
			//return Problem(
			//	statusCode: StatusCodes.Status404NotFound,
			//	title: "Invalid movie ID",
			//	detail: $"No movie with ID {movieId} was found.",
			//	instance: HttpContext.Request.Path
			//);

			// ToDo : Create a custom exception and handle this exception in program.cs 
			throw new ArgumentNullException($"No movie with ID {movieId} was found.");
		}

		return await _unitOfWork.Reviews.GetAllReviewsForMovieAsync(movieId, changeTracker: false);
	}
}
