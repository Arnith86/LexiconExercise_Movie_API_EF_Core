using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Data;
using System.Linq.Expressions;

namespace MovieData.Repositories;

/// <summary>
/// Provides a generic base implementation for common data access operations using Entity Framework Core.
/// This class supports basic CRUD operations and query capabilities for entities derived from <see cref="EntityBase"/>.
/// </summary>
/// <typeparam name="T">The entity type, which must inherit from <see cref="EntityBase"/>.</typeparam>

public class RepositoryBase<T> : IRepositoryActions<T>, IRepositoryQueries<T> where T : EntityBase
{
	/// <summary>
	/// The Entity Framework Core <see cref="DbSet{TEntity}"/> used to interact with the entity set.
	/// </summary>
	protected DbSet<T> DbSet { get; }

	public RepositoryBase(MovieApiContext context)
	{
		DbSet = context.Set<T>();
	}

	/// <inheritdoc/>
	public async Task<bool> FindAnyAsync(Expression<Func<T, bool>> expression)
		=> await DbSet.AnyAsync(expression);

	/// <inheritdoc/>
	public IQueryable<T> GetAll(bool trackChanges = false)
		=> !trackChanges ?	DbSet.AsNoTracking() :
							DbSet;

	/// <inheritdoc/>
	public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
		=> !trackChanges ? DbSet.Where(expression).AsNoTracking() :
						   DbSet.Where(expression);

	public void Add(T entity) => DbSet.Add(entity);
	public void Remove(T entity) => DbSet.Remove(entity);
	public void Update(T entity) => DbSet.Update(entity);
}
