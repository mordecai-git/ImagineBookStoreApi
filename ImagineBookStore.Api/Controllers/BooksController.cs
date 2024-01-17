using ImagineBookStore.Core.Interfaces;
using ImagineBookStore.Core.Models.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImagineBookStore.Api.Controllers
{
    [ApiController]
    [Route("api/v1/books")]
    [Authorize]
    public class BooksController : BaseController
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookModel model)
        {
            var res = await _bookService.AddBook(model);
            return ProcessResponse(res);
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var res = await _bookService.DeleteBook(bookId);
            return ProcessResponse(res);
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBook(int bookId)
        {
            var res = await _bookService.GetBook(bookId);
            return ProcessResponse(res);
        }

        [HttpGet]
        public async Task<IActionResult> ListBooks([FromQuery] PagingOptionModel request)
        {
            var res = await _bookService.ListBooks(request);
            return ProcessResponse(res);
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook(int bookId, BookModel model)
        {
            var res = await _bookService.UpdateBook(bookId, model);
            return ProcessResponse(res);
        }
    }
}