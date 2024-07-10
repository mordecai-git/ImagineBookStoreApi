namespace ImagineBookStore.Model.Utilities;

/// <summary>
/// Represents a model containing pagination information.
/// </summary>
public class Paging
{
    /// <summary>
    /// Index of the current page.
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// Size of each page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total number of pages.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Total number of items.
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Indicates whether there is a previous page.
    /// </summary>
    public bool HasPreviousPage { get; set; }

    /// <summary>
    /// Indicates whether there is a next page.
    /// </summary>
    public bool HasNextPage { get; set; }
}