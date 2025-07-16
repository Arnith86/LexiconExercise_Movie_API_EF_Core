using MovieCore.DomainContracts;
using MovieData.Data;

namespace MovieData.Repositories
{
	/// <summary>
	/// Implements the Unit of Work pattern to coordinate changes across multiple repositories
	/// using a shared <see cref="MovieApiContext"/> instance.
	/// </summary>
	public class UnitOfWork : IUnitOfWork
	{
		private readonly MovieApiContext _context;
		private readonly Lazy<IMovieRepository> _movieRepository;
		private readonly Lazy<IMovieGenreRepository> _movieGenreRepository;
		private readonly Lazy<IReviewRepository> _reviewRepository;
		private readonly Lazy<IActorRepository> _actorRepository;

		/// <inheritdoc/>
		public IMovieRepository Movies => _movieRepository.Value;
		/// <inheritdoc/>
		public IMovieGenreRepository MovieGenres => _movieGenreRepository.Value;
		/// <inheritdoc/>
		public IReviewRepository Reviews => _reviewRepository.Value;
		/// <inheritdoc/>
		public IActorRepository Actors => _actorRepository.Value;

		public UnitOfWork(MovieApiContext context)
		{
			_context = context;
			_movieRepository = new Lazy<IMovieRepository>(() => new MovieRepository(_context));
			_movieGenreRepository = new Lazy<IMovieGenreRepository>(() => new MovieGenreRepository(_context));
			_reviewRepository = new Lazy<IReviewRepository>(() => new ReviewRepository(_context));
			_actorRepository = new Lazy<IActorRepository>(() => new ActorRepository(_context));
		}

		/// <inheritdoc/>
		public async Task CompleteAsync() => await _context.SaveChangesAsync();
	}
}
