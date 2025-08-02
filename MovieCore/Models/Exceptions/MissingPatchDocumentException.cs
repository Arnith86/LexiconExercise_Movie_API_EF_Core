namespace MovieCore.Models.Exceptions;

public class MissingPatchDocumentException : ArgumentNullException
{
	public string Title { get; } = "Argument Null";

	public MissingPatchDocumentException() 
		: base("No patch document was included in request.")
	{
	}
}
