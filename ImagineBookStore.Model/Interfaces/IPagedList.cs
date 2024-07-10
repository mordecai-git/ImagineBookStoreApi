namespace ImagineBookStore.Model.Interfaces;

/// <summary>
/// Represents information about paging in a collection of items.
/// </summary>
public interface IPagedList
{
    /// <summary>
    /// Gets the index of the current page.
    /// </summary>
    int PageIndex { get; }

    /// <summary>
    /// Gets the size of each page.
    /// </summary>
    int PageSize { get; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    int TotalPages { get; }

    /// <summary>
    /// Gets the total number of items in the entire collection.
    /// </summary>
    int TotalItems { get; }

    /// <summary>
    /// Gets a value indicating whether there is a previous page.
    /// </summary>
    bool HasPreviousPage { get; }

    /// <summary>
    /// Gets a value indicating whether there is a next page.
    /// </summary>
    bool HasNextPage { get; }
}