using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCore.Models.Entities;

namespace MovieData.Data.Configurations
{
	internal class MovieGenreConfigurations : IEntityTypeConfiguration<MovieGenre>
	{
		public void Configure(EntityTypeBuilder<MovieGenre> builder)
		{
			builder.HasKey(mg => mg.Id);

			builder.Property(mg => mg.Genre)
				.HasMaxLength(50)
				.IsRequired();


			// Sets up a N:1 relationship between Movies and MoviesGenre. Can be null.
			builder.HasMany(m => m.Movies)
				.WithOne(mg => mg.MoviesGenre)
				.HasForeignKey(m => m.MovieGenreId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.ToTable("MoviesGenre");
		}
	}
}