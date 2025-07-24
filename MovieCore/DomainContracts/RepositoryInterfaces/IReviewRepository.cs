using MovieCore.Models.DTOs.ReviewDTOs;
using MovieCore.Models.Entities;

namespace MovieCore.DomainContracts.RepositoryInterfaces;

/// <summary>
/// Defines data access operations specific to <see cref="Review"/> entities.
/// Inherits basic query capabilities from <see cref="IRepositoryQueries{Review}"/>.
/// </summary>
public interface IReviewRepository : IRepositoryQueries<Review>, IRepositoryActions<Review>
{
	Task<List<ReviewDto>> GetAllReviewsForMovieAsync(int movieId, bool changeTracker = false); 
}
