namespace MovieCore.Models.Exceptions;

public class InvalidArgumentException : Exception
{
	public string Title { get; }

	public InvalidArgumentException(string message, string title = "Invalid argument!") 
		: base(message)
	{
		Title = title;
	}
}
