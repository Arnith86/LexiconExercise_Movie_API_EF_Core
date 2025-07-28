namespace MovieCore.Models.Exceptions;

public class MovieGenreInvalidArgumentException : InvalidArgumentException
{
	public MovieGenreInvalidArgumentException() : base($"A movie genre must be assigned to a movie an creation.")
	{
	}
}
