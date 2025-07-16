namespace MovieCore.Models.Entities;

/// <summary>
/// Represents detailed information about a movie.
/// This entity has a one-to-one relationship with the <see cref="Movie"/> entity.
/// </summary>
public class MovieDetails : EntityBase
{
	public int Id { get; set; }
	public int MovieId { get; set; }
	public string? Synopsis { get; set; } = null!;
	public string? Language { get; set; } = null!;
	public int Budget { get; set; }
	public VideoMovie Movie { get; set; } = null!;

}
