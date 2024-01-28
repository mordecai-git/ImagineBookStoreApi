using ImagineBookStore.Core.Models.Input;
using ImagineBookStore.Core.Models.Utilities;
using ImagineBookStore.Core.Models.View;

namespace ImagineBookStore.Core.Interfaces;

/// <summary>
/// Interface for book-related services.
/// </summary>
public interface IBookService
{
    /// <summary>
    /// Adds a new book with the provided details.
    /// </summary>
    /// <param name="model">The book model containing book details.</param>
    /// <returns>A task with the result of the book addition: <see cref="Result{T}"/> where T is <see cref="BookView"/>.</returns>
    Task<Result> AddBook(BookModel model);

    /// <summary>
    /// Deletes the book with the specified ID.
    /// </summary>
    /// <param name="bookId">The ID of the book to delete.</param>
    /// <returns>A task with the result of the book deletion: <see cref="Result"/>.</returns>
    Task<Result> DeleteBook(int bookId);

    /// <summary>
    /// Retrieves details of the book with the specified ID.
    /// </summary>
    /// <param name="bookId">The ID of the book to retrieve.</param>
    /// <returns>A task with the result of retrieving the book details: <see cref="Result{T}"/> where T is <see cref="BookView"/>.</returns>
    Task<Result> GetBook(int bookId);

    /// <summary>
    /// Lists books based on the provided paging options.
    /// </summary>
    /// <param name="request">The paging options for listing books.</param>
    /// <returns>A task with the result of listing books: <see cref="Result{T}"/> where T is a list of <see cref="BookView"/>.</returns>
    Task<Result> ListBooks(PagingOptionModel request);

    /// <summary>
    /// Updates the book with the specified ID using the provided details.
    /// </summary>
    /// <param name="bookId">The ID of the book to update.</param>
    /// <param name="model">The book model containing updated details.</param>
    /// <returns>A task with the result of updating the book: <see cref="Result{T}"/> where T is <see cref="BookView"/>.</returns>
    Task<Result> UpdateBook(int bookId, BookModel model);
}