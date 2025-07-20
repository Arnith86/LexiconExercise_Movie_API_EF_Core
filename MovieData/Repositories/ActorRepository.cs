using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts.RepositoryInterfaces;
using MovieCore.DomainContracts.RequestParameters;
using MovieCore.Models.Entities;
using MovieData.Data;
using MovieData.Extensions;


namespace MovieData.Repositories;

/// <summary>
/// Repository implementation for accessing and managing <see cref="Actor"/> entities.
/// Inherits common CRUD and query functionality from <see cref="RepositoryBase{T}"/>.
/// </summary>
public class ActorRepository : RepositoryBase<Actor>, IActorRepository
{
	public ActorRepository(MovieApiContext context) : base(context)
	{
	}

	public async Task<bool> AnyAsync(int id) => await FindAnyAsync(a => a.Id.Equals(id));

	public async Task<Actor?> GetActorAsync(int id, bool changeTracker = false)
		=> await GetByCondition(a => a.Id.Equals(id), changeTracker).FirstOrDefaultAsync();

	public async Task<IPageList<Actor>> GetAllActorsAsync(
		MovieRequestParameters requestParameters, 
		bool changeTracker = false)
	{
		return await GetAll(changeTracker)
			.ToPageListAsync(requestParameters.PageNumber, requestParameters.PageSize);
	}
}
