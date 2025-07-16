using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCore.Models.Entities;

namespace MovieData.Data.Configurations
{
	internal class MovieConfigurations : IEntityTypeConfiguration<VideoMovie>
	{
		public void Configure(EntityTypeBuilder<VideoMovie> builder)
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