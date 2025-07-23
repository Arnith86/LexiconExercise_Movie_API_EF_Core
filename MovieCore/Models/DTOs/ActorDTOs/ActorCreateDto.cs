// Ignore Spelling: Dto

using System.ComponentModel.DataAnnotations;

namespace MovieCore.Models.DTOs.ActorDTOs;

public class ActorCreateDto 
{
	[MaxLength(50)]
	[Required]
	public string Name { get; set; } = null!;
	[Range(1900, 3000)]
	public int? BirthYear { get; set; }
}
