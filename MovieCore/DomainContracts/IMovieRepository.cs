using MovieCore.Models.DTOs.MovieDtos;
using MovieCore.Models.Entities;


namespace MovieCore.DomainContracts;

/// <summary>
/// Defines data access operations specific to <see cref="VideoMovie"/> entities,
/// including CRUD functionality and custom query methods.
/// Inherits generic query and modification capabilities from 
/// <see cref="IRepositoryQueries{Movie}"/> and <see cref="IRepositoryActions{Movie}"/>.
/// </summary>
public interface IMovieRepository : IRepositoryQueries<VideoMovie>, IRepositoryActions<VideoMovie>
{
	Task<List<VideoMovie>> GetAllMoviesAsync(bool changeTracker = false);
	Task<VideoMovie?> GetMovieAsync(int id, bool changeTracker = false);
	Task<bool> AnyAsync(int id);
	Task<VideoMovie?> GetMovieDetailsAsync(int id, bool changeTracker = false);
	Task<MovieDetailDto?> GetMovieFullDetailsAsync(int id, bool changeTracker = false);
}
