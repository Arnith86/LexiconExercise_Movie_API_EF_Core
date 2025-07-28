// Ignore Spelling: Dto

using System.ComponentModel.DataAnnotations;

namespace MovieCore.Models.DTOs.MovieDtos;

public class MovieDetailsCreateDto
{
	[Required]
	public int MovieId { get; set; }

	[MaxLength(250)]
	public string? Synopsis { get; set; } = null!;

	[MaxLength(50)]
	public string? Language { get; set; } = null!;

	[Range(0, int.MaxValue)]
	public int Budget { get; set; }
}
