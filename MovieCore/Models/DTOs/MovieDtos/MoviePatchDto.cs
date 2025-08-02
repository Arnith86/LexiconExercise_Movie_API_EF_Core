// Ignore Spelling: Dto

namespace MovieCore.Models.DTOs.MovieDtos;

public class MoviePatchDto
{
	public int MovieId { get; set; }
	public string Title { get; set; } = null!;
	public int Year { get; set; }
	public int Duration { get; set; }
	public int MovieGenreId { get; set; }
}
