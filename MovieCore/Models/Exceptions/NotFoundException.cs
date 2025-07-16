namespace MovieCore.Models.Exceptions;

public class NotFoundException: Exception
{
	public string Title { get; }

	public NotFoundException(string message, string title = "Invalid Id") : base(message)
	{
		Title = title;
	}
}
