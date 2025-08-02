namespace MovieCore.Models.Exceptions;

public class MovieDetailsNotLinkedException : Exception
{
	public string Title { get; set; } = "No movie details found";
	public MovieDetailsNotLinkedException(int movieId) 
		: base($"No movie details linked to movie with Id {movieId}.")
	{
	}
}
