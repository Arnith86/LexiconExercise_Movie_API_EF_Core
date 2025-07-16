using Services.Contracts;
using Services.Contracts.Contracts;
using ServicesContracts.Contracts;

namespace Movie.Services;

/// <summary>
/// Aggregates and lazily initializes access to application service layer components (acts as a facade for services), 
/// such as movie, review, and actor services. This class acts as a centralized entry point for accessing 
/// domain services, improving dependency management and promoting a clean architecture.
/// </summary>
public class ServiceManager : IServiceManager
{
	private readonly Lazy<IMoviesServices> _movieServices;
	private readonly Lazy<IReviewServices> _reviewServices;
	private readonly Lazy<IActorServices> _actorServices;

	public IMoviesServices MovieServices => _movieServices.Value;
	public IReviewServices ReviewServices => _reviewServices.Value;
	public IActorServices ActorServices => _actorServices.Value;

	public ServiceManager(
		Lazy<IMoviesServices> movieServices,
		Lazy<IReviewServices> reviewServices,
		Lazy<IActorServices> actorServices)
	{
		_movieServices = movieServices;
		_reviewServices = reviewServices;
		_actorServices = actorServices;
	}
}
