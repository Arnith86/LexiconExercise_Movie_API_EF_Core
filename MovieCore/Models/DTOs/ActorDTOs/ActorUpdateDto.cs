// Ignore Spelling: Dto

using System.ComponentModel.DataAnnotations;

namespace MovieCore.Models.DTOs.ActorDTOs;

public class ActorUpdateDto
{
	[MaxLength(50)]
	[Required]
	public string Name { get; set; } = null!;
	public int? BirthYear { get; set; }
}
