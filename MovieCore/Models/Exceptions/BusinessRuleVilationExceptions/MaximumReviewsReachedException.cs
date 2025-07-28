namespace MovieCore.Models.Exceptions.BusinessRuleVilationExceptions;

public class MaximumReviewsReachedException : BusinessRuleViolationException
{
	public MaximumReviewsReachedException(int movieId) 
		: base($"Movie with ID {movieId} already has the maximum allowed number of reviews (10).")
	{
	}
}
