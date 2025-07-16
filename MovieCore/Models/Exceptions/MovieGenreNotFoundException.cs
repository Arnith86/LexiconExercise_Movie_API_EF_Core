namespace MovieCore.Models.Exceptions;

public class MovieGenreNotFoundException : NotFoundException
{
	public MovieGenreNotFoundException(int id) : base($"No movie genre with id:{id} was found.")
	{
	}
}
