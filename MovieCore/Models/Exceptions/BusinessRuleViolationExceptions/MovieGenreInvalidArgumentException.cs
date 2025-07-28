namespace MovieCore.Models.Exceptions.BusinessRuleViolationExceptions;

public class MovieGenreInvalidArgumentException : InvalidArgumentException
{
	public MovieGenreInvalidArgumentException() : base($"A movie genre must be assigned to a movie an creation.")
	{
	}
}
