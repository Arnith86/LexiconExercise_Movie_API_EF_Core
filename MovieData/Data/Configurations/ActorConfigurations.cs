using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCore.Models.Entities;

namespace MovieData.Data.Configurations
{
	internal class ActorConfigurations : IEntityTypeConfiguration<Actor>
	{
		public void Configure(EntityTypeBuilder<Actor> builder)
		{
			// Has a M:N relation to Movie
			builder.HasKey(a => a.Id);

			builder.Property(a => a.Name) // Add <type>
				.HasMaxLength(50)
				.IsRequired();


			builder.ToTable("Actor",
				a => a.HasCheckConstraint("CK_Actor_BirthYear_MinValue", "[BirthYear] >= 1850"));
		}
	}
}