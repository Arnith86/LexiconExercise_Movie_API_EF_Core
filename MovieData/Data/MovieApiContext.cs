using Microsoft.EntityFrameworkCore;
using MovieCore.Models.Entities;
using MovieData.Data.Configurations;

namespace MovieData.Data;

public class MovieApiContext : DbContext
{
	public MovieApiContext(DbContextOptions<MovieApiContext> options)
		: base(options)
	{
	}

	public DbSet<VideoMovie> Movies { get; set; } = default!;
	public DbSet<MovieDetails> MovieDetails { get; set; } = default!;
	public DbSet<MovieGenre> MovieGenres { get; set; } = default!;
	public DbSet<Review> Reviews { get; set; } = default!;
	public DbSet<Actor> Actors { get; set; } = default!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfiguration(new MovieDetailsConfigurations());
		modelBuilder.ApplyConfiguration(new ReviewConfigurations());
		modelBuilder.ApplyConfiguration(new MovieGenreConfigurations());
		modelBuilder.ApplyConfiguration(new ActorConfigurations());
		modelBuilder.ApplyConfiguration(new MovieConfigurations());
		modelBuilder.ApplyConfiguration(new MovieActorConfigurations());

	}
}
