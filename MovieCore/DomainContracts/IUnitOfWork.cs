namespace MovieCore.DomainContracts;

/// <summary>
/// Defines a unit of work that encapsulates access to multiple repositories
/// and coordinates changes across them to ensure transactional consistency.
/// </summary>
public interface IUnitOfWork
{
	/// <summary>
	/// Gets the repository for handling <see cref="Entities.Movie"/> entities.
	/// </summary>
	IMovieRepository Movies { get; }

	/// <summary>
	/// Gets the repository for handling <see cref="Entities.Movie"/> entities.
	/// </summary>
	IMovieGenreRepository MovieGenres { get; }

	/// <summary>
	/// Gets the repository for handling <see cref="Entities.Review"/> entities.
	/// </summary>
	IReviewRepository Reviews { get; }

	/// <summary>
	/// Gets the repository for handling <see cref="Entities.Actor"/> entities.
	/// </summary>
	IActorRepository Actors { get; }

	/// <summary>
	/// Persists all changes made through the repositories in a single transaction.
	/// </summary>
	/// <returns>A task representing the asynchronous save operation.</returns>
	Task CompleteAsync();
}
