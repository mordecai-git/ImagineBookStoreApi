using ImagineBookStore.Model.Interfaces;

namespace ImagineBookStore.Model.Utilities;

/// <summary>
/// Represents a paged list containing a subset of items of type TDestination.
/// Implements IPagedList for pagination information.
/// </summary>
/// <typeparam name="TDestination">Type of items in the paged list.</typeparam>
public class PagedList<TDestination> : List<TDestination>, IPagedList
{
    /// <summary>
    /// Gets or sets the index of the current page.
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// Gets the total number of pages in the paged list.
    /// </summary>
    public int TotalPages { get; private set; }

    /// <summary>
    /// Gets the total number of items in the paged list.
    /// </summary>
    public int TotalItems { get; private set; }

    /// <summary>
    /// Gets the size of each page in the paged list.
    /// </summary>
    public int PageSize { get; private set; }

    /// <summary>
    /// Gets a value indicating whether there is a previous page.
    /// </summary>
    public bool HasPreviousPage
    {
        get
        {
            return PageIndex > 1;
        }
    }

    /// <summary>
    /// Gets a value indicating whether there is a next page.
    /// </summary>
    public bool HasNextPage
    {
        get
        {
            return PageIndex < TotalPages;
        }
    }

    /// <summary>
    /// Initializes a new instance of the PagedList class with a subset of items, total count, page index, and page size.
    /// </summary>
    /// <param name="items">Subset of items for the current page.</param>
    /// <param name="count">Total count of items in the entire collection.</param>
    /// <param name="pageIndex">Index of the current page.</param>
    /// <param name="pageSize">Size of each page.</param>
    public PagedList(List<TDestination> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalItems = count;
        PageSize = pageSize;

        AddRange(items);
    }
}