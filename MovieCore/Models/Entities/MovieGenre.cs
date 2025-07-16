namespace MovieCore.Models.Entities;


/// <summary>
/// Represents the link between a movie and one of its genres.
/// Each combination of MovieId and Genre is unique.
/// </summary>
public class MovieGenre : EntityBase
{
	public int Id { get; set; }
	public string Genre { get; set; } = null!;
	public ICollection<VideoMovie> Movies { get; set; } = new List<VideoMovie>();
}
