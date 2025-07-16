using System.ComponentModel.DataAnnotations;

namespace MovieCore.Models.DTOs.MovieActorDto;

public class MovieActorCreateDto
{
	[Required(ErrorMessage = "Movie ID is required.")]
	public int MovieId { get; set; }

	[Required(ErrorMessage = "Actor ID is required.")]
	public int ActorId { get; set; }

	[MaxLength(30)]
	public string? Role { get; set; }
}


