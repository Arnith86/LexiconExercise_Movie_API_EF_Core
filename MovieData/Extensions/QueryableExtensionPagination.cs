
using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts.RequestParameters;
using MovieCore.Models.Exceptions;

namespace MovieData.Extensions;

/// <summary>
/// Provides extension methods for applying pagination logic to <see cref="IQueryable{T}"/> sources.
/// </summary>
public static class QueryableExtensionPagination
{
	/// <summary>
	/// Asynchronously creates a paginated list from the specified <see cref="IQueryable{T}"/> source.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the source query.</typeparam>
	/// <param name="source">The queryable data source to paginate.</param>
	/// <param name="pageNumber">The current page number (1-based index).</param>
	/// <param name="pageSize">The number of items per page.</param>
	/// <returns>
	/// A task representing the asynchronous operation, containing an <see cref="IPageList{T}"/> 
	/// with the requested page of data and pagination metadata.
	/// </returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown when <paramref name="pageNumber"/> or <paramref name="pageSize"/> is less than or equal to zero.
	/// </exception>
	public static async Task<IPageList<T>> ToPageListAsync<T>(
		this IQueryable<T> source,
		int pageNumber,
		int pageSize)
	{
		if (pageNumber < 1)
			throw new PaginationArgumentOutOfRangeException(nameof(pageNumber), "Page number must be higher then 0");
		if (pageSize < 1)
			throw new PaginationArgumentOutOfRangeException(nameof(pageSize), "Page size must be higher then 0");

		int count = await source.CountAsync();

		var items = await source.Skip(pageSize * (pageNumber - 1))
							.Take(pageSize)
							.ToListAsync();

		return new PageList<T>(items, count, pageNumber, pageSize);
	}
}
