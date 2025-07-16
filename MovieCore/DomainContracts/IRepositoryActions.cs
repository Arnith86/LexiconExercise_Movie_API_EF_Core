using MovieCore.Models.Entities;

namespace MovieCore.DomainContracts;

/// <summary>
/// Defines basic repository actions for handling entities in a data store.
/// </summary>
/// <typeparam name="T">
/// The type of entity the repository will manage. Must inherit from <see cref="EntityBase"/>.
/// </typeparam>
public interface IRepositoryActions<T> where T : EntityBase
{
	void Add(T entity);
	void Update(T entity);
	void Remove(T entity);
}