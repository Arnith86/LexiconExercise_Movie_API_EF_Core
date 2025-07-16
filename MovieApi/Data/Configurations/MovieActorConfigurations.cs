using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models.Entities;

namespace MovieApi.Data.Configurations
{
	internal class MovieActorConfigurations : IEntityTypeConfiguration<MovieActor>
	{
		public void Configure(EntityTypeBuilder<MovieActor> builder)
		{
			// MovieActor has N:1 relation to both Movie and Actor
			builder.HasKey(ma => new { ma.MovieId, ma.ActorId });


			builder.HasOne<Movie>(m => m.Movie)
				.WithMany(m => m.MovieActors)
				.HasForeignKey(ma => ma.MovieId)
				.IsRequired();

			builder.HasOne<Actor>(a => a.Actor)
				.WithMany(a => a.MovieActors)
				.HasForeignKey(ma => ma.ActorId)
				.IsRequired();
			
			builder.Property(ma => ma.Role)
				.HasMaxLength(30);


			builder.ToTable("MovieActor");
		}
	}
}