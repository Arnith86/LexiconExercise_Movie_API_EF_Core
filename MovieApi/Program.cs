using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieApi.ExtensionsDependencyInjection;
using MovieCore.DomainContracts;
using MovieCore.Models.Exceptions;
using MovieData.Data;
using MovieData.Data.Configurations;
using MovieData.Extensions;
using MovieData.Repositories;
using MoviePresentation;


namespace MovieApi
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddDbContext<MovieApiContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("MovieApiContext")
					?? throw new InvalidOperationException("Connection string 'MovieApiContext' not found.")));

			// Add services to the container.

			// Needed to allow swagger api documentation.
			builder.Services.AddSwaggerGen(opt =>
			{
				opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Title = "VideoMovie API",
					Version = "v1"
				});
				opt.EnableAnnotations();
			});

			builder.Services.AddAutoMapper(config =>
				config.AddProfile<MapperProfile>()
			);

			builder.Services.AddControllers()
				// Makes ASP.NET Core look for controllers in another project (MoviePresentation in this case).
				.AddApplicationPart(typeof(AssemblyReference).Assembly);

			// "AddScoped" is chosen, because context is scoped. As such the lifetime of the service needs
			// to match. 
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


			builder.Services.AddServiceLayer();


			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			//builder.Services.AddOpenApi();

			var app = builder.Build();

			// Todo: Extract to extension method.
			app.UseExceptionHandler(builder =>
			{
				builder.Run(async context =>
				{
					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

					if (contextFeature != null)
					{
						var problemDetailsFactory = app.Services.GetRequiredService<ProblemDetailsFactory>();

						ProblemDetails problemDetails;
						int statusCode;

						switch (contextFeature.Error)
						{
							case MovieNotFoundException movieNotFoundException: // Movie Not Found
								statusCode = StatusCodes.Status404NotFound;
								problemDetails = problemDetailsFactory.CreateProblemDetails(
									context,
									statusCode,
									title: movieNotFoundException.Title,
									detail: movieNotFoundException.Message,
									instance: context.Request.Path
								);
								break;
							case MovieGenreNotFoundException movieGenreNotFoundException: // Movie Genre Not Found
								statusCode = StatusCodes.Status404NotFound;
								problemDetails = problemDetailsFactory.CreateProblemDetails(
									context,
									statusCode,
									title: movieGenreNotFoundException.Title,
									detail: movieGenreNotFoundException.Message,
									instance: context.Request.Path
								);
								break;
							case ActorNotFoundException actorNotFoundException: // Actor Not Found
								statusCode = StatusCodes.Status404NotFound;
								problemDetails = problemDetailsFactory.CreateProblemDetails(
									context,
									statusCode,
									title: actorNotFoundException.Title,
									detail: actorNotFoundException.Message,
									instance: context.Request.Path
								);
								break;
							default:
								statusCode = StatusCodes.Status500InternalServerError;  // General server error
								problemDetails = problemDetailsFactory.CreateProblemDetails(
										context,
										statusCode,
										title: "Internal Server Error",
										detail: contextFeature.Error.Message,
										instance: context.Request.Path);
								break;
						}

						context.Response.StatusCode = statusCode;
						await context.Response.WriteAsJsonAsync(problemDetails);
					}
				});
			});

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				//app.MapOpenApi();
				app.UseSwagger();
				app.UseSwaggerUI(options =>
				{
					// Tell it exactly where the JSON file is
					options.SwaggerEndpoint("/swagger/v1/swagger.json", "VideoMovie API V1");
				});
				await app.SeedData();
			}


			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
