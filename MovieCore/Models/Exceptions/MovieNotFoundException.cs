namespace MovieCore.Models.Exceptions;

public class MovieNotFoundException : NotFoundException
{
	public MovieNotFoundException(int id) : base($"No movie with the id:{id} was found.")
	{
	}
}
