using MovieCore.Models.Entities;

namespace MovieCore.DomainContracts;

/// <summary>
/// Defines data access operations specific to <see cref="MovieGenre"/> entities.
/// Inherits basic query capabilities from <see cref="IRepositoryQueries{MovieGenre}"/>.
/// </summary>
public interface IMovieGenreRepository : IRepositoryQueries<MovieGenre>
{
	Task<bool> AnyAsync(int id);
}
