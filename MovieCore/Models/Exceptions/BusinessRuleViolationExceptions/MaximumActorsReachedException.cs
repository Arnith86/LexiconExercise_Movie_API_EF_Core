namespace MovieCore.Models.Exceptions.BusinessRuleViolationExceptions;

public class MaximumActorsReachedException : BusinessRuleViolationException
{
	public MaximumActorsReachedException(int movieId) : base($"Movie with Id {movieId} is a Documentary, and cannot have more then 10 actors.")
	{
	}
}
