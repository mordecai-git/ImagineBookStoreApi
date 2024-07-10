using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Model.Input;
using ImagineBookStore.Model.Utilities;
using ImagineBookStore.Model.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImagineBookStore.Api.Controllers;

/// <summary>
/// API controller for managing books in the application.
/// </summary>
[ApiController]
[Route("api/v1/books")]
[Authorize]
public class BooksController : BaseController
{
    private readonly IBookService _bookService;

    /// <summary>
    /// Initializes a new instance of the <see cref="BooksController"/> class.
    /// </summary>
    /// <param name="bookService">The book service to be injected.</param>
    public BooksController(IBookService bookService)
    {
        _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
    }

    /// <summary>
    /// Adds a new book to the application.
    /// </summary>
    /// <remarks>
    /// This endpoint adds a new book to the database, the title must be unique to the author. <br/>
    /// Requires an Admin role to access this.
    /// </remarks>
    /// <param name="model">This represents the book object to be added.</param>
    /// <response code="201">Returns the added book object.</response>
    /// <response code="400">Returns an error object indicating what went wrong.</response>
    [HttpPost]
    [ProducesResponseType(typeof(SuccessResult<BookView>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddBook(BookModel model)
    {
        var res = await _bookService.AddBook(model);
        return ProcessResponse(res);
    }

    /// <summary>
    /// Delete a book
    /// </summary>
    /// <remarks>
    ///  Deletes a book with the specified identifier. <br/>
    /// Requires an Admin role to access this.
    /// </remarks>
    /// <param name="bookId">The integer identifier of the book to be deleted.</param>
    /// <response code="200">Returns a successful message if deleted successfully.</response>
    /// <response code="404">Returns Not Found if book does not exist.</response>
    [HttpDelete("{bookId}")]
    [ProducesResponseType(typeof(SuccessResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook(int bookId)
    {
        var res = await _bookService.DeleteBook(bookId);
        return ProcessResponse(res);
    }

    /// <summary>
    /// Get a Book
    /// </summary>
    /// <remarks>
    /// Retrieves details of a book with the specified identifier.
    /// </remarks>
    /// <param name="bookId">The integer identifier of the book to be retrieved.</param>
    /// <response code="200">Returns the book object.</response>
    /// <response code="404">Returns Not Found if book does not exist.</response>
    [HttpGet("{bookId}")]
    [ProducesResponseType(typeof(SuccessResult<BookView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBook(int bookId)
    {
        var res = await _bookService.GetBook(bookId);
        return ProcessResponse(res);
    }

    /// <summary>
    /// List Books
    /// </summary>
    /// <remarks>
    /// Lists books based on the provided paging options.
    /// </remarks>
    /// <param name="request">The <see cref="PagingOptionModel"/> containing the paging options.</param>
    /// <response code="200">Returns the list of books in a paginated format.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedSuccessResult<BookView>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListBooks([FromQuery] PagingOptionModel request)
    {
        var res = await _bookService.ListBooks(request);
        return ProcessResponse(res);
    }

    /// <summary>
    /// Update a Book details
    /// </summary>
    /// <remarks>
    /// Updates details of a book with the specified identifier. <br/>
    /// Requires an Admin role to access this.
    /// </remarks>
    /// <param name="bookId">The identifier of the book to be updated.</param>
    /// <param name="model">The object representing the updated details of the book.</param>
    /// <response code="200">Returns the updated book object.</response>
    /// <response code="404">Returns not found if book does not exist.</response>
    [HttpPut("{bookId}")]
    [ProducesResponseType(typeof(SuccessResult<BookView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundErrorResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBook(int bookId, BookModel model)
    {
        var res = await _bookService.UpdateBook(bookId, model);
        return ProcessResponse(res);
    }
}