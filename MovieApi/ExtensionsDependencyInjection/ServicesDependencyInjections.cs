using Movie.Services;
using Movie.Services.Services;
using MovieServices.Services;
using Services.Contracts;
using Services.Contracts.Contracts;
using ServicesContracts.Contracts;

namespace MovieApi.ExtensionsDependencyInjection;

/// <summary>
/// Provides extension methods for registering the service layer dependencies used in the VideoMovie API.
/// </summary>
public static class ServicesDependencyInjections
{
	/// <summary>
	/// Registers all service-layer interfaces and their corresponding implementations into the 
	/// dependency injection container. Includes lazy initialization for each service. 
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to which services are added.</param>
	public static void AddServiceLayer(this IServiceCollection services)
	{
		services.AddScoped<IServiceManager, ServiceManager>();
		
		services.AddScoped<IMoviesServices, MoviesServices>();
		services.AddScoped(provider =>
			new Lazy<IMoviesServices>(() => provider.GetRequiredService<IMoviesServices>())
		);
		
		services.AddScoped<IReviewServices, ReviewServices>();
		services.AddScoped(provider =>
			new Lazy<IReviewServices>(() => provider.GetRequiredService<IReviewServices>())
		);
		
		services.AddScoped<IActorServices, ActorServices>();
		services.AddScoped(provider =>
			new Lazy<IActorServices>(() => provider.GetRequiredService<IActorServices>())
		);
	}
}
