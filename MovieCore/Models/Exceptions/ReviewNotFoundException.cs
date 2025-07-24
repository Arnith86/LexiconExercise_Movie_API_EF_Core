namespace MovieCore.Models.Exceptions;

public class ReviewNotFoundException : NotFoundException
{
	public ReviewNotFoundException(int id) : base($"No Review with id:{id} was found.")
	{
	}
}
