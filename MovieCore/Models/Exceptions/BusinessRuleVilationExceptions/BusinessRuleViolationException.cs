namespace MovieCore.Models.Exceptions.BusinessRuleVilationExceptions;

public class BusinessRuleViolationException : Exception
{
	public string Title { get; }

	public BusinessRuleViolationException(string message, string title = "Business Rule Violation")
		: base(message)
	{
		Title = title;
	}
}
