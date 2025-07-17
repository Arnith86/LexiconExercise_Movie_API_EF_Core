namespace MovieCore.Models.Exceptions;

public class ActorNotFoundException : NotFoundException
{
	public ActorNotFoundException(int id) : base($"No actor with id:{id} was found.")
	{
	}
}
