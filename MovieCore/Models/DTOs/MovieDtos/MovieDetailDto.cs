// Ignore Spelling: Dto

using MovieCore.Models.DTOs.ActorDTOs;
using MovieCore.Models.DTOs.ReviewDTOs;

namespace MovieCore.Models.DTOs.MovieDtos;

public class MovieDetailDto
{
	public int Id { get; set; }
	public string Genre { get; set; } = null!;
	public string Title { get; set; } = null!;
	public int Year { get; set; }
	public int Duration { get; set; }
	public string? Synopsis { get; set; } = null!;
	public string? Language { get; set; } = null!;
	public int? Budget { get; set; }
	public IEnumerable<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
	public IEnumerable<ActorDto> Actors { get; set; } = new List<ActorDto>();


}
