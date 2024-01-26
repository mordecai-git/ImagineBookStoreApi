namespace ImagineBookStore.Core.Models.View;

/// <summary>
/// Represents a view model for displaying book information, including ID, title, author, genre, and total stock.
/// </summary>
public class BookView
{
    /// <summary>
    /// Identifier for the book.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Title of the book.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Author of the book.
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// Genre of the book.
    /// </summary>
    public string Genre { get; set; }

    /// <summary>
    /// Total stock quantity of the book.
    /// </summary>
    public int TotalStock { get; set; }
}