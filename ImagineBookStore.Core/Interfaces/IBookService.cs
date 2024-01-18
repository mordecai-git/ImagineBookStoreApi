using ImagineBookStore.Core.Models.Input;
using ImagineBookStore.Core.Models.Utilities;

namespace ImagineBookStore.Core.Interfaces;

public interface IBookService
{
    Task<Result> AddBook(BookModel model);

    Task<Result> DeleteBook(int bookId);

    Task<Result> GetBook(int bookId);

    Task<Result> ListBooks(PagingOptionModel request);

    Task<Result> UpdateBook(int bookId, BookModel model);
}