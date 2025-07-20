namespace MovieCore.DomainContracts.RequestParameters;

/// <summary>
/// Provides base pagination parameters for requests that support paging.
/// </summary>
public abstract class RequestBaseParameters
{
	private const int _c_maxPageSize = 100;
	private int _pageSize = 10;

	/// <summary>
	/// The current page number of the result set. Defaults to 1.
	/// </summary>
	public int PageNumber { get; set; } = 1;

	/// <summary>
	/// The number of items to return per page. 
	/// If the value exceeds the maximum allowed size, it will be capped at 100.
	/// </summary>
	public int PageSize
	{
		get => _pageSize;
		set => _pageSize = value > _c_maxPageSize ? _c_maxPageSize : value;
	}
}
