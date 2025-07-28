namespace MovieCore.Models.Exceptions.BusinessRuleViolationExceptions;

public class DuplicateMovieTitleArgumentException : BusinessRuleViolationException
{
	public DuplicateMovieTitleArgumentException(string movieTitle) : base($"There already exist a movie with the title {movieTitle}.")
	{
	}
}
