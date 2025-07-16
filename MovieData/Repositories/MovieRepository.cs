using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.DTOs.ActorDTOs;
using MovieCore.Models.DTOs.MovieDtos;
using MovieCore.Models.DTOs.ReviewDTOs;
using MovieCore.Models.Entities;
using MovieData.Data;

namespace MovieData.Repositories;

/// <summary>
/// Repository implementation for accessing and managing <see cref="VideoMovie"/> entities.
/// Inherits common CRUD and query functionality from <see cref="RepositoryBase{T}"/>.
/// </summary>
public class MovieRepository : RepositoryBase<VideoMovie>, IMovieRepository
{

	public MovieRepository(MovieApiContext context) : base(context)
	{
	}

	public async Task<bool> AnyAsync(int id) => await FindAnyAsync(m => m.Id.Equals(id));

	public async Task<List<VideoMovie>> GetAllMoviesAsync(bool changeTracker = false) => 
		await GetAll(changeTracker).Include(m => m.MoviesGenre).ToListAsync();

	public async Task<VideoMovie?> GetMovieAsync(int id, bool changeTracker = false) =>	
		await GetByCondition(m => m.Id.Equals(id), changeTracker)
				.Include(m => m.MoviesGenre)
				.FirstOrDefaultAsync();

	public async Task<VideoMovie?> GetMovieDetailsAsync(int id, bool changeTracker = false) =>
		await GetByCondition(gmd => gmd.Id.Equals(id))
				.Include(md => md.MoviesDetails)
				.Include(mg => mg.MoviesGenre)
				.FirstOrDefaultAsync();

	public async Task<MovieDetailDto?> GetMovieFullDetailsAsync(int id, bool changeTracker = false) => 
		await GetByCondition(mfd => mfd.Id.Equals(id))
			.Include(r => r.Reviews)
			.Include(md => md.MoviesDetails)
			.Include(mg => mg.MoviesGenre)
			.Include(a => a.MovieActors)
			.Select(mfd => new MovieDetailDto
			{
				Id = mfd.Id,
				Genre = mfd.MoviesGenre!.Genre,
				Title = mfd.Title,
				Year = mfd.Year,
				Duration = mfd.Duration,
				Synopsis = mfd.MoviesDetails!.Synopsis,
				Language = mfd.MoviesDetails!.Language,
				Budget = mfd.MoviesDetails!.Budget,
				Reviews = mfd.Reviews.Where(r => r.MovieId == id)
					.Select(r => new ReviewDto
					{
						Id = r.Id,
						ReviewerName = r.ReviewerName,
						Comment = r.Comment,
						Rating = r.Rating
					}).ToList(),
				Actors = mfd.MovieActors
					.Select(a => new ActorDto
					{
						Id = a.Actor.Id,
						Name = a.Actor.Name,
						BirthYear = a.Actor.BirthYear
					}).ToList()

			}).FirstOrDefaultAsync();


}
