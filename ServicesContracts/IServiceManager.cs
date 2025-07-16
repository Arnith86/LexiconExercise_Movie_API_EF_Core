using Services.Contracts.Contracts;
using ServicesContracts.Contracts;

namespace Services.Contracts;

/// <summary>
/// Defines a centralized access point for all service-layer operations, encapsulating movie, 
/// review, and actor services. Acts as a service aggregator (facade) to simplify dependency 
/// injection and promote separation of concerns.
/// </summary>
public interface IServiceManager
{
	IMoviesServices MovieServices { get; }
	IReviewServices ReviewServices { get; }
	IActorServices ActorServices { get; }
}
