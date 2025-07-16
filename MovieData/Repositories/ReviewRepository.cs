using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.DTOs.ReviewDTOs;
using MovieCore.Models.Entities;
using MovieData.Data;

namespace MovieData.Repositories;

/// <summary>
/// Repository implementation for accessing and managing <see cref="Review"/> entities.
/// Inherits common CRUD and query functionality from <see cref="RepositoryBase{T}"/>.
/// </summary>
public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
{
	public ReviewRepository(MovieApiContext context) : base(context)
	{
	}

	public async Task<List<ReviewDto>> GetAllReviewsForMovieAsync(int movieId, bool changeTracker = false) =>
		await GetByCondition(r => r.MovieId.Equals(movieId))
			.Select(r => new ReviewDto
			{
				Id = r.Id,
				ReviewerName = r.ReviewerName,
				Comment = r.Comment,
				Rating = r.Rating
			}).ToListAsync();
}
