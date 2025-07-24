// Ignore Spelling: Dto

using System.ComponentModel.DataAnnotations;

namespace MovieCore.Models.DTOs.ReviewDTOs;

public class ReviewCreateDto
{
	[Required(ErrorMessage = "An movie Id must be provided.")]
	public int MovieId { get; set; }

	[MaxLength(50)]
	[Required(ErrorMessage = "A reviewer name must be provided.")]
	public string ReviewerName { get; set; } = null!;

	[MaxLength(500)]
	public string Comment { get; set; } = null!;

	[Range(1, 5)]
	public int Rating { get; set; }
}
