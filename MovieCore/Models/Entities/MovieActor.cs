namespace MovieCore.Models.Entities;

/// <summary>
/// Represents the many-to-many relationship between movies and actors,
/// including the role played by the actor in the specific movie.
/// </summary>
public class MovieActor 
{
	public int MovieId { get; set; }
	public VideoMovie Movie { get; set; } = null!;

	public int ActorId { get; set; }
	public Actor Actor { get; set; } = null!;

	public string? Role { get; set; }
}
