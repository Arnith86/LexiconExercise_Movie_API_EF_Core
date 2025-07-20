namespace MovieCore.DomainContracts.RequestParameters;

/// <summary>
/// Represents metadata information about a paginated result set.
/// </summary>
public interface IPaginationMetaData
{
	int CurrentPage { get; set; }
	int PageSize { get; set; }
	int TotalItemCount { get; set; }
	int TotalPages { get; set; }
	bool hasPrevious { get; }
	bool hasNext { get; }
}