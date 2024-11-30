using Microsoft.AspNetCore.Mvc;
using ReadersRent.Interfaces;

namespace ReadersRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentController : Controller
    {
        private readonly IRent _historyService;

        public RentController(IRent historyService)
        {
            _historyService = historyService;
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
        [HttpGet]
        [Route("GetRentHistoryForBook")]
        public async Task<IActionResult> GetRentHistoryForBook(int idbook)
        {
            return await _historyService.GetRentHistoryForBook(idbook);
        }
    }
}
