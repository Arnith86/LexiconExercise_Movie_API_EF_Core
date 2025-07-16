using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieApi.Data;
using MovieApi.Data.Configurations;
using MovieApi.Extensions;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
					Title = "Movie API",
					Version = "v1"
				});
				opt.EnableAnnotations();
			});

			builder.Services.AddAutoMapper(config =>
				config.AddProfile<MapperProfile>()
			);

			builder.Services.AddControllers();


			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			//builder.Services.AddOpenApi();


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				//app.MapOpenApi();
				app.UseSwagger();
				app.UseSwaggerUI(options =>
				{
					// Tell it exactly where the JSON file is
					options.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API V1");
					//options.RoutePrefix = string.Empty; // So it shows at https://localhost:7120/
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
