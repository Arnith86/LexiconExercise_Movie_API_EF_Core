namespace MovieServices
{
	public interface IPaginationMetaData
	{
		int CurrentPage { get; set; }
		int PageSize { get; set; }
		int TotalItemCount { get; set; }
		int TotalPageCount { get; set; }
	}
}