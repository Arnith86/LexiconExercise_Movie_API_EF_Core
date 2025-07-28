namespace MovieCore.Models.Exceptions.BusinessRuleViolationExceptions;

public class DuplicateMovieDetailsAssignmentException : BusinessRuleViolationException
{
	public DuplicateMovieDetailsAssignmentException(int movieId)
		: base($"The movie with Id {movieId} has movie details assigned already.")
	{
	}
}
