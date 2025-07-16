namespace MovieCore.Models.DTOs.MovieDtos;

public class MovieWithGenreDetailsDto : MovieWithGenreDto
{
	public string? Synopsis { get; set; } = null!;
	public string? Language { get; set; } = null!;
	public int? Budget { get; set; }
}
