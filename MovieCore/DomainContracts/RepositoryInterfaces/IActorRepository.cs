using MovieCore.DomainContracts.RequestParameters;
using MovieCore.Models.Entities;

namespace MovieCore.DomainContracts.RepositoryInterfaces;

/// <summary>
/// Defines data access operations specific to <see cref="Actor"/> entities.
/// Inherits basic query capabilities from <see cref="IRepositoryQueries{Actor}"/>.
/// </summary>
public interface IActorRepository : IRepositoryQueries<Actor>
{
	Task<bool> AnyAsync(int id);
	Task<Actor?> GetActorAsync(int id, bool changeTracker = false);
	Task<IPageList<Actor>> GetAllActorsAsync(
		MovieRequestParameters requestParameters,
		bool changeTracker = false);
}