namespace MovieCore.DomainContracts.RequestParameters;

public class PageList<T> : IPageList<T>
{
	public IReadOnlyList<T> Items { get; }
	public IPaginationMetaData MetaData { get; set; }

	public PageList(List<T> items, int count, int pageNumber, int pageSize)
	{
		MetaData = new PaginationMetaData(
			currentPage: pageNumber,
			totalPages: (int)Math.Ceiling(count / (double)pageSize),
			pageSize: pageSize,
			totalItemCount: count
		);

		Items = items;
	}
}
