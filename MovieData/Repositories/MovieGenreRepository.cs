using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Data;

namespace MovieData.Repositories;

/// <summary>
/// Repository implementation for accessing and managing <see cref="MovieGenre"/> entities.
/// Inherits common CRUD and query functionality from <see cref="RepositoryBase{T}"/>.
/// </summary>
public class MovieGenreRepository : RepositoryBase<MovieGenre>, IMovieGenreRepository
{
	public MovieGenreRepository(MovieApiContext context) : base(context)
	{
	}

	public async Task<bool> AnyAsync(int id) => await FindAnyAsync(mg => mg.Id.Equals(id));
}
