using MovieCore.Models.DTOs.ReviewDTOs;

namespace ServicesContracts.Contracts;

/// <summary>
/// Provides service-level operations related to Review entities, acting as an intermediary between the controller and data access layers.
/// </summary>
public interface IReviewServices
{
	/// <summary>
	/// Retrieves all reviews associated with a specific movie.
	/// </summary>
	/// <param name="movieId">The ID of the movie whose reviews are to be fetched.</param>
	/// <returns>
	/// A task that represents the asynchronous operation. The task result contains a collection of review DTOs.
	/// </returns>
	Task<IEnumerable<ReviewDto>> GetAllReviewsAsync(int movieId);

	/// <summary>
	/// Adds a new review for a movie.
	/// </summary>
	/// <param name="reviewCreateDto">The data required to create a new review, including rating, comment, and associated movie ID.</param>
	/// <returns>A <see cref="ReviewDto"/> representing the newly created review, including its details.</returns>
	Task<ReviewDto> AddReview(ReviewCreateDto reviewCreateDto);

	/// <summary>
	/// Removes a review associated with a movie.
	/// </summary>
	/// <param name="reviewId">The Id of the given review to remove.</param>
	/// <returns><c>true<c>, if the review was removed.</returns>
	Task<bool> RemoveReviewAsync(int reviewId);
}
