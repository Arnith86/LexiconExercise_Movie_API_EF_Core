using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCore.Models.Entities;

/// <summary>
/// Represents a movie entity with core properties such as title, year, genre, and duration.
/// Includes an optional one-to-one relationship with <see cref="MovieDetails"/> for extended information,
/// and a one-to-many relationship with <see cref="Review"/> representing user reviews.
/// </summary>
public class VideoMovie : EntityBase
{
	public int Id { get; set; }
	public string Title { get; set; } = null!;
	public int Year { get; set; }
	public int Duration { get; set; }
	public MovieDetails? MoviesDetails { get; set; } = null!;
	public ICollection<Review> Reviews { get; set; } = new List<Review>();
	public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
	public int MovieGenreId { get; set; }

	[ForeignKey(nameof(MovieGenreId))]
	public MovieGenre? MoviesGenre { get; set; }
}
