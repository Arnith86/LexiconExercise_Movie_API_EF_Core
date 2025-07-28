namespace MovieCore.Models.Exceptions.BusinessRuleViolationExceptions;

public class DuplicateActorAssignmentException : BusinessRuleViolationException
{
	public DuplicateActorAssignmentException(int actorId, int movieId) 
		: base($"The actor with Id {actorId} is already assigned to movie with Id {movieId}.")
	{
	}
}
