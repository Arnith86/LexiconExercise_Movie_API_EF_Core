using MovieApi.Data;
using System.Diagnostics;

namespace MovieApi.Extensions;

/// <summary>
/// Provides an extension method for <see cref="IApplicationBuilder"/> to seed initial data into the database.
/// This method creates a scoped service provider to obtain the application's <see cref="MovieApiContext"/>,
/// and then calls the asynchronous seeding logic defined in <see cref="DataSeeder.InitAsync(MovieApiContext)"/>.
/// Exceptions during seeding are caught and logged via <see cref="Debug"/>, then rethrown.
/// </summary>
public static class SeedDataExtension
{
	public static async Task SeedData(this IApplicationBuilder app)
	{
		// Create a new scope for dependency injection.
		// This scope controls the lifetime of scoped services like DbContext.
		using (var scope = app.ApplicationServices.CreateScope())
		{
			var serviceProvider = scope.ServiceProvider;
			var context = serviceProvider.GetRequiredService<MovieApiContext>();

			try
			{
				await DataSeeder.InitAsync(context);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				throw;
			}
		}
	}
}
