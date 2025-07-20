namespace MovieCore.Models.Exceptions;

public class PaginationArgumentOutOfRangeException : Exception
{
	public string Title { get; }

	public PaginationArgumentOutOfRangeException(string message, string title = "Pagination out of range.") 
		: base(message)
	{
		Title = title;
	}
}
