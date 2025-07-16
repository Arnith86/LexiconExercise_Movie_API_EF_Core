using MovieApi.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models.DTOs.MovieDtos
{
	public class MovieBaseDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = null!;
		public int Year { get; set; }
		public int Duration { get; set; }
	}
}
