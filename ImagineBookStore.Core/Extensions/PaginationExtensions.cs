using ImagineBookStore.Core.Models.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ImagineBookStore.Core.Extensions;

/// <summary>
/// Provides extension methods for IQueryable and IEnumerable to handle pagination.
/// </summary>
public static class PaginationExtensions
{
    /// <summary>
    /// Returns a paginated list.
    /// </summary>
    /// <typeparam name="T">Type of items in the list.</typeparam>
    /// <param name="source">An IQueryable instance to apply pagination to.</param>
    /// <param name="pageIndex">Page number that starts with zero.</param>
    /// <param name="pageSize">Size of each page.</param>
    /// <returns>Returns a paginated list.</returns>
    public static async Task<PagedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize)
    {
        return await CreateAsync(source, pageIndex, pageSize);
    }

    /// <summary>
    /// Creates a paginated list asynchronously.
    /// </summary>
    /// <typeparam name="T">Type of items in the list.</typeparam>
    /// <param name="source">An IQueryable instance to apply pagination to.</param>
    /// <param name="pageIndex">Page number that starts with zero.</param>
    /// <param name="pageSize">Size of each page.</param>
    /// <returns>A paginated list.</returns>
    private static async Task<PagedList<T>> CreateAsync<T>(IQueryable<T> source, int pageIndex, int pageSize)
    {
        try
        {
            var count = await source.CountAsync();
            var items = pageSize == 0
                ? await source.ToListAsync()
                : await source.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageIndex, pageSize);
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Returns a paginated list from an IEnumerable.
    /// </summary>
    /// <typeparam name="T">Type of items in the list.</typeparam>
    /// <param name="source">An IEnumerable instance to apply pagination to.</param>
    /// <param name="pageIndex">Page number that starts with zero.</param>
    /// <param name="pageSize">Size of each page.</param>
    /// <returns>Returns a paginated list.</returns>
    public static PagedList<T> ToPaginatedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
    {
        return Create(source, pageIndex, pageSize);
    }

    /// <summary>
    /// Returns a paginated list asynchronously with a fixed page size of 15.
    /// </summary>
    /// <typeparam name="T">Type of items in the list.</typeparam>
    /// <param name="source">An IQueryable instance to apply pagination to.</param>
    /// <param name="pageIndex">Page number that starts with zero.</param>
    /// <returns>Returns a paginated list.</returns>
    /// <remarks>This functionality may not work in SQL Compact 3.5</remarks>
    public static async Task<PagedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageIndex)
    {
        return await CreateAsync(source, pageIndex, 15);
    }

    /// <summary>
    /// Creates a paginated list.
    /// </summary>
    /// <typeparam name="T">Type of items in the list.</typeparam>
    /// <param name="source">An IEnumerable instance to apply pagination to.</param>
    /// <param name="pageIndex">Page number that starts with zero.</param>
    /// <param name="pageSize">Size of each page.</param>
    /// <returns>Returns a paginated list.</returns>
    private static PagedList<T> Create<T>(IEnumerable<T> source, int pageIndex, int pageSize)
    {
        try
        {
            var count = source.Count();
            var items = pageSize == 0
                ? source.ToList()
                : source.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageIndex, pageSize);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
