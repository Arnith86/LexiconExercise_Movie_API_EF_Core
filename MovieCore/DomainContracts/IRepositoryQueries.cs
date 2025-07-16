using MovieCore.Models.Entities;
using System.Linq.Expressions;

namespace MovieCore.DomainContracts;

/// <summary>
/// Defines query operations for retrieving entities from the database.
/// </summary>
/// <typeparam name="T">The entity type, constrained to inherit from <see cref="EntityBase"/>.</typeparam>
public interface IRepositoryQueries<T> where T : EntityBase
{
	/// <summary>
	/// Asynchronously determines whether any entities match the given condition.
	/// </summary>
	/// <param name="expression">A LINQ expression used to test against the entities.</param>
	/// <returns>
	/// A task that represents the asynchronous operation. The task result contains <c>true</c> 
	/// if any entities match the condition; otherwise, <c>false</c>.
	/// </returns>
	Task<bool> FindAnyAsync(Expression<Func<T, bool>> expression);

	/// <summary>
	/// Retrieves all entities from the database as an <see cref="IQueryable{T}"/>.
	/// </summary>
	/// <param name="trackChanges">Indicates whether to enable change tracking.</param>
	/// <returns>An <see cref="IQueryable{T}"/> containing all entities.</returns>
	IQueryable<T> GetAll(bool trackChanges = false);
	
	/// <summary>
	/// Retrieves entities that match the given condition.
	/// </summary>
	/// <param name="expression">A LINQ expression used to filter the entities.</param>
	/// <param name="trackChanges">Indicates whether to enable change tracking.</param>
	/// <returns>An <see cref="IQueryable{T}"/> containing the matching entities.</returns>
	IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
}