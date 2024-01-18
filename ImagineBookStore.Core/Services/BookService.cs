﻿using ImagineBookStore.Core.Extensions;
using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Core.Models.App;
using ImagineBookStore.Core.Models.Input;
using ImagineBookStore.Core.Models.Utilities;
using ImagineBookStore.Core.Models.View;
using ImagineBookStore.Core.Utilities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ImagineBookStore.Core.Services;

public class BookService : IBookService
{
    private readonly BookStoreContext _context;
    private readonly UserSession _userSession;

    public BookService(BookStoreContext context, UserSession userSession)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
    }

    public async Task<Result> AddBook(BookModel model)
    {
        // validate if book exist
        bool bookExist = await _context.Books
            .AnyAsync(b => b.Title.ToLower().Trim() == model.Title.ToLower().Trim()
                && b.Author.ToLower().Trim() == model.Author.ToLower().Trim());

        if (bookExist)
            return new ErrorResult($"Book titled {model.Title} and written by {model.Author} already exist");

        Book mappedBook = model.Adapt<Book>();

        await _context.AddAsync(mappedBook);

        int saved = await _context.SaveChangesAsync();

        return saved > 0
            ? new SuccessResult(StatusCodes.Status201Created, mappedBook.Adapt<BookView>())
            : new ErrorResult(StaticErrorMessages.UnableToSaveChanges);
    }

    public async Task<Result> DeleteBook(int bookId)
    {
        Book book = await _context.Books.FindAsync(bookId);

        if (book == null) return new ErrorResult("Book does not exist");

        if (book.IsDeleted) return new SuccessResult();

        book.IsDeleted = true;
        book.DeletedById = _userSession.UserId;
        book.DeletedAt = DateTime.UtcNow;

        int saved = await _context.SaveChangesAsync();

        return saved > 0
            ? new SuccessResult("Book deleted successfully")
            : new ErrorResult(StaticErrorMessages.UnableToSaveChanges);
    }

    public async Task<Result> GetBook(int bookId)
    {
        Book book = await _context.Books
            .Where(b => b.Id == bookId && !b.IsDeleted)
            .FirstOrDefaultAsync();

        if (book == null) return new ErrorResult("Book does not exist");

        BookView mappedBook = book.Adapt<BookView>();

        return new SuccessResult(mappedBook);
    }

    public async Task<Result> ListBooks(PagingOptionModel request)
    {
        var allBooks = await _context.Books
            .Where(b => !b.IsDeleted)
            .ProjectToType<BookView>()
            .ToPaginatedListAsync(request.PageIndex, request.PageSize);

        return new SuccessResult(allBooks);
    }

    public async Task<Result> UpdateBook(int bookId, BookModel model)
    {
        Book book = await _context.Books
            .Where(b => b.Id == bookId && !b.IsDeleted)
            .FirstOrDefaultAsync();

        if (book == null) return new ErrorResult("Book does not exist");

        // update the record with the model
        model.Adapt(book);

        int saved = await _context.SaveChangesAsync();

        return saved > 0
            ? new SuccessResult("Book updated successfully", book.Adapt<BookView>())
            : new ErrorResult(StaticErrorMessages.UnableToSaveChanges);
    }
}
