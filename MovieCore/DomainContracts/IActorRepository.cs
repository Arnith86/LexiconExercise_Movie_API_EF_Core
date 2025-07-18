using MovieCore.Models.DTOs.ActorDTOs;
using MovieCore.Models.Entities;

namespace MovieCore.DomainContracts;

/// <summary>
/// Defines data access operations specific to <see cref="Actor"/> entities.
/// Inherits basic query capabilities from <see cref="IRepositoryQueries{Actor}"/>.
/// </summary>
public interface IActorRepository : IRepositoryQueries<Actor>
{
	Task<bool> AnyAsync(int id);
	Task<Actor?> GetActorAsync(int id, bool changeTracker = false);
	Task<IEnumerable<Actor>> GetAllActorsAsync(int pageSize, int page, bool changeTracker = false);
}