namespace ImagineBookStore.Model.Input;

/// <summary>
/// Represents the paging options for querying data.
/// </summary>
public class PagingOptionModel
{
    /// <summary>
    /// The page index/number to query. Defaults to 1.
    /// </summary>
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// The page size to return or how many items to return. Defaults to 15.
    /// </summary>
    public int PageSize { get; set; } = 15;
}