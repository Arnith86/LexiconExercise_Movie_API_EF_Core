// Ignore Spelling: Dto

namespace MovieServices.Services
{
	public class EntityServicesBase
	{
		private const int _pageNumberDefault = 1;
		private const int _pageSizeDefault = 10;
		private const int _pageSizeMax = 100;

		protected (int setPageSize, int setPage) SetPageVariables(
			int pageSize = _pageSizeDefault,
			int page = _pageNumberDefault)
		{
			pageSize = pageSize < 1 ? _pageSizeDefault : pageSize;
			pageSize = pageSize > 100 ? _pageSizeMax : pageSize;
			page = page < 1 ? _pageNumberDefault : page;

			return (pageSize, page);
		}
	}
}