using Microsoft.AspNetCore.Mvc;
using ReadersRent.Interfaces;
using ReadersRent.Model;

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
        public async Task<IActionResult> AddNewRent(Rent newRent)
        {
            return await _historyService.AddNewRent(newRent);
        }
        //o Возврат арендованной книги.
        [HttpPut]
        [Route("ReturnBook/{ID_Rent}")]
        public async Task<IActionResult> ReturnBook(int ID_Rent)
        {
            return await _historyService.ReturnBook(ID_Rent);
        }

        //o   Получение истории аренды книг для конкретного читателя.
        [HttpGet]//должно работать
        [Route("RentHistoryForReader/{Id_Reader}")]
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
        [Route("GetRentHistoryForBook/{idbook}")]
        public async Task<IActionResult> GetRentHistoryForBook(int idbook)
        {
            return await _historyService.GetRentHistoryForBook(idbook);
        }
    }
}
