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

	public async Task<Actor?> GetActorAsync(int id, bool changeTracker = false) => 
		await GetByCondition(a => a.Id.Equals(id), changeTracker).FirstOrDefaultAsync();

	public async Task<IPageList<Actor>> GetAllActorsAsync(
		ActorRequestParameters requestParameters, 
		bool changeTracker = false)
	{

		if (!requestParameters.WithMovies)
		{ 
			return await GetAll(changeTracker)
				.ToPageListAsync(requestParameters.PageNumber, requestParameters.PageSize);
		}	

		return await GetAll(changeTracker)
			.Include(a => a.MovieActors)
			.ThenInclude(m => m.Movie)
			.ToPageListAsync(requestParameters.PageNumber, requestParameters.PageSize);
	}

	public async Task<bool> IsActorAssignedToMovie(int actorId, int movieId) =>
		await DbSet.AnyAsync(
			a => a.Id.Equals(actorId) &&
			a.MovieActors.Any(
				ma => ma.MovieId.Equals(movieId)
			)
		);
}
