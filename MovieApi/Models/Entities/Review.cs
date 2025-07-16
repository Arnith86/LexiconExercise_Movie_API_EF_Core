using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models.Entities;

/// <summary>
/// Represents a review for a movie, including reviewer information, comment, and rating.
/// </summary>
public class Review
{
	public int Id { get; set; }
	public int MovieId { get; set; }
	public string ReviewerName { get; set; } = null!;
	public string Comment { get; set; } = null!;
	public int Rating { get; set; }
	public Movie Movie { get; set; } = null!;
}
