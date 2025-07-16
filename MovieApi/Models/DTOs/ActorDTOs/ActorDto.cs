using MovieApi.Models.DTOs.MovieDtos;
using MovieApi.Models.Entities;

namespace MovieApi.Models.DTOs.ActorDTOs
{
	public class ActorDto
	{
		public required int Id { get; set; }
		public required string Name { get; set; } = null!;
		public int BirthYear { get; set; }
	}
}
