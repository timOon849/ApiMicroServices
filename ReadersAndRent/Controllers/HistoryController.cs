using Microsoft.AspNetCore.Mvc;
using ReadersAndRent.Interfaces;
using System.Net.Http;

namespace ReadersAndRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService, IHttpClientFactory httpClientFactory)
        {
            _historyService = historyService;
            _httpClient = httpClientFactory.CreateClient();
        }
        //o Аренда книги читателем(с указанием срока аренды).
        [HttpPost]
        [Route("AddNewRent")]
        public async Task<IActionResult> AddNewRent(int srok, int Id_Book, int IdReader)
        {
            return await _historyService.AddNewRent(srok, Id_Book, IdReader);
        }
        //o Возврат арендованной книги.
        [HttpPost]
        [Route("ReturnBook")]
        public async Task<IActionResult> ReturnBook(int ID_History)
        {
            return await _historyService.ReturnBook(ID_History);
        }

        //o   Получение истории аренды книг для конкретного читателя.
        [HttpGet]//должно работать
        [Route("RentHistoryForReader")]
        public async Task<IActionResult> RentHistoryForReader(int Id_Reader)
        {
            return await _historyService.RentHistoryForReader(Id_Reader);
        }
        //o Получение информации о текущих арендах (кто арендовал, на какой срок).
        [HttpGet]
        [Route("GetCurrentRentals")]
        public async Task<IActionResult> GetCurrentRentals()
        {
            return await _historyService.GetCurrentRentals();
        }

        ////o Получение истории аренды для конкретной книги.
        //[HttpGet]
        //[Route("GetRentHistoryForBook")]
        //public async Task<IActionResult> GetRentHistoryForBook(int bookId)
        //{
        //    var rentHistory = await _context.Rent.Where(r => r.ID_Book == bookId).ToListAsync();
        //    var book = await _context.Books.FindAsync(bookId);
        //    if (rentHistory != null && book != null)
        //    {
        //        return Ok(rentHistory);
        //    }
        //    else if (book == null)
        //    {
        //        return BadRequest("Книги с таким ID не существует");
        //    }
        //    else
        //    {
        //        return BadRequest("История аренды для книги с таким ID не найдена");
        //    }
        //}
    }
}
