using BookGenre.Model;
using BookGenre.Service;
using Microsoft.AspNetCore.Mvc;
using BookGenre.Interfaces;

namespace BookGenre.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBook _bookService;

        public BookController(IBook bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("getAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            return await _bookService.GetAllBooks();
        }


        [HttpGet]
        [Route("GetInfoByID/{ID_Book}")]
        public async Task<IActionResult> GetInfoByID(int ID_Book)
        {
            return await _bookService.GetInfoByID(ID_Book);
        }

        [HttpPost]
        [Route("CreateNewBook")]
        public async Task<IActionResult> CreateNewBook(Books newBook)
        {
            return await _bookService.CreateNewBook(newBook);
        }

        [HttpPut]
        [Route("UpdateBook")]
        public async Task<IActionResult> UpdateBook(int ID_Book, Books book)
        {
            return await _bookService.UpdateBook(ID_Book, book);
        }

        [HttpPut]
        [Route("UpdateBookImage")]
        public async Task<IActionResult> UpdateBookImage(int ID_Book, IFormFile image)
        {
            return await _bookService.UpdateBookImage(ID_Book, image);
        }

        [HttpDelete]
        [Route("DeleteBook")]
        public async Task<IActionResult> DeleteBook(int ID_Book)
        {
            return await _bookService.DeleteBook(ID_Book);
        }

        [HttpGet]
        [Route("GetBooksByZhanr")]
        public async Task<IActionResult> GetBooksByZhanr(string Namezhanr)
        {
            return await _bookService.GetBooksByZhanr(Namezhanr);
        }

        [HttpGet]
        [Route("GetBookNameAuthor")]
        public async Task<IActionResult> GetBookNameAuthor(string? Authorbook, string? Namebook)
        {
            return await _bookService.GetBookNameAuthor(Authorbook, Namebook);
        }

        [HttpGet]
        [Route("BooksPagination")]
        public async Task<IActionResult> BooksPagination(int page, int pageSize)
        {
            return await _bookService.BooksPagination(page, pageSize);
        }

        [HttpGet]
        [Route("SearchOrFilter")]
        public async Task<IActionResult> SearchOrFilter(string author, int? genreId, int? year)
        {
            return await _bookService.SearchOrFilter(author, genreId, year);
        }
    }
}
