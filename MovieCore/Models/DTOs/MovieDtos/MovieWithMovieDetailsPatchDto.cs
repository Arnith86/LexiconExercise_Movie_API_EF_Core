// Ignore Spelling: Dto

namespace MovieCore.Models.DTOs.MovieDtos;

public class MovieWithMovieDetailsPatchDto : MoviePatchDto
{
	public int MovieDetailsId { get; set; }
	public string? Synopsis { get; set; } = null!;
	public string? Language { get; set; } = null!;
	public int Budget { get; set; }
}
