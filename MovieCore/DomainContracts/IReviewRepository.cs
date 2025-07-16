using MovieCore.Models.DTOs.ReviewDTOs;
using MovieCore.Models.Entities;

namespace MovieCore.DomainContracts;

/// <summary>
/// Defines data access operations specific to <see cref="Review"/> entities.
/// Inherits basic query capabilities from <see cref="IRepositoryQueries{Review}"/>.
/// </summary>
public interface IReviewRepository : IRepositoryQueries<Review>
{
	Task<List<ReviewDto>> GetAllReviewsForMovieAsync(int movieId, bool changeTracker = false);
}
