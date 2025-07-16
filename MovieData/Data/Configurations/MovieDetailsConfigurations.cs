using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCore.Models.Entities;

namespace MovieData.Data.Configurations
{
	internal class MovieDetailsConfigurations : IEntityTypeConfiguration<MovieDetails>
	{
		public void Configure(EntityTypeBuilder<MovieDetails> builder)
		{
			// Has a 1:1 relationship between Movies and MoviesDetails. 
			builder.HasKey(md => md.Id);

			builder.Property(md => md.Synopsis)
				.HasMaxLength(400);

			builder.Property(md => md.Language)
				.HasMaxLength(50);



			builder.ToTable("MoviesDetail");
		}
	}
}