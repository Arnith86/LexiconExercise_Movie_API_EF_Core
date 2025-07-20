using MovieCore.Models.DTOs.MovieDtos;

namespace MovieCore.Models.DTOs.ActorDTOs
{
	public class ActorDto
	{
		public required int Id { get; set; }
		public required string Name { get; set; } = null!;
		public int BirthYear { get; set; }
		public ICollection<MovieBaseDto> VideoMovies { get; set; } = new List<MovieBaseDto>();
	}
}
