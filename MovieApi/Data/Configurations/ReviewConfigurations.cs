using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models.Entities;

namespace MovieApi.Data.Configurations
{
	internal class ReviewConfigurations : IEntityTypeConfiguration<Review>
	{
		public void Configure(EntityTypeBuilder<Review> builder)
		{
			builder.HasKey(r => r.Id);

			builder.Property(r => r.ReviewerName)
				.HasMaxLength(50)
				.IsRequired();

			builder.Property(r => r.Comment)
				.HasMaxLength(200);

			builder.Property(r => r.Rating)
				.IsRequired();

			// Sets up a 1:N relationship between Movies and Review. Can not be null.
			builder.HasOne(m => m.Movie)
				.WithMany(r => r.Reviews)
				.HasForeignKey(r => r.MovieId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.ToTable("Review");
		}
	}
}