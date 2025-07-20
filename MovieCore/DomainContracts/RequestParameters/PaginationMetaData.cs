namespace MovieCore.DomainContracts.RequestParameters;

public class PaginationMetaData : IPaginationMetaData
{
	public int CurrentPage { get; set; }
	public int TotalPages { get; set; }
	public int PageSize { get; set; }
	public int TotalItemCount { get; set; }
	public bool hasPrevious => CurrentPage > 1;
	public bool hasNext => CurrentPage < TotalPages;

	public PaginationMetaData(int currentPage, int totalPages, int pageSize, int totalItemCount)
	{
		CurrentPage = currentPage;
		TotalPages = totalPages;
		PageSize = pageSize;
		TotalItemCount = totalItemCount;
	}
}
