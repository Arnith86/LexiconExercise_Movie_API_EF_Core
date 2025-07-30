namespace MovieCore.Models.Exceptions.BusinessRuleViolationExceptions;

public class MovieDetailsBusinessRuleException : BusinessRuleViolationException
{
	public MovieDetailsBusinessRuleException(int movieId) : base($"Movie with ID {movieId} exceeds budget limit of 1 million set for Documentary's.")
	{
	}
}
