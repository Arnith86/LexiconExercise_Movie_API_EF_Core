using MovieCore.DomainContracts.RequestParameters;
using MovieCore.Models.DTOs.MovieDtos;
using MovieCore.Models.Entities;


namespace MovieCore.DomainContracts.RepositoryInterfaces;

/// <summary>
/// Defines data access operations specific to <see cref="VideoMovie"/> entities,
/// including CRUD functionality and custom query methods.
/// Inherits generic query and modification capabilities from 
/// <see cref="IRepositoryQueries{Movie}"/> and <see cref="IRepositoryActions{Movie}"/>.
/// </summary>
public interface IMovieRepository : IRepositoryQueries<VideoMovie>, IRepositoryActions<VideoMovie>
{
	Task<bool> AnyAsync(int id);
	Task<bool> AnyAsync(string title);
	Task<bool> AlreadyHasMovieDetailsAsync(int movieId);
	Task<IPageList<VideoMovie>> GetAllMoviesAsync(
		MovieRequestParameters requestParameters, 
		bool changeTracker = false);
	Task<VideoMovie?> GetMovieAsync(int id, bool changeTracker = false);
	Task<VideoMovie?> GetMovieDetailsAsync(int id, bool changeTracker = false);
	Task<MovieDetailDto?> GetMovieFullDetailsAsync(int id, bool changeTracker = false);
}
