using MovieCore.DomainContracts.RequestParameters;
using MovieCore.Models.Entities;

namespace MovieCore.DomainContracts.RepositoryInterfaces;

/// <summary>
/// Defines data access operations specific to <see cref="Actor"/> entities.
/// Inherits basic query capabilities from <see cref="IRepositoryQueries{Actor}"/>.
/// </summary>
public interface IActorRepository : IRepositoryQueries<Actor>, IRepositoryActions<Actor>
{
	Task<bool> AnyAsync(int id);
	Task<Actor?> GetActorAsync(int id, bool changeTracker = false);
	Task<IPageList<Actor>> GetAllActorsAsync(
		ActorRequestParameters requestParameters,
		bool changeTracker = false);
	Task<bool> IsActorAssignedToMovie(int actorId, int movieId);
}