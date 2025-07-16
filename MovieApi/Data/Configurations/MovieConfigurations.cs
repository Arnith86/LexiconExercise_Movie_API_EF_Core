using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models.Entities;

namespace MovieApi.Data.Configurations
{
	internal class MovieConfigurations : IEntityTypeConfiguration<Movie>
	{
		public void Configure(EntityTypeBuilder<Movie> builder)
		{
			builder.HasKey(m => m.Id);

			builder.Property(m => m.Title)
				.HasMaxLength(100)
				.IsRequired();

			// Sets up a 1:1 relationship between Movies and MoviesDetails. 
			builder.HasOne(m => m.MoviesDetails)
				.WithOne(md => md.Movie)
				.HasForeignKey<MovieDetails>(md => md.MovieId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}