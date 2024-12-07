using BookGenre.Model;
using BookGenre.Service;
using Microsoft.AspNetCore.Mvc;
using BookGenre.Interfaces;

namespace BookGenre.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : Controller
    {
        private readonly IGenre _zhanrService;

        public GenreController(IGenre zhanrService)
        {
            _zhanrService = zhanrService;
        }

        //o Получение списка всех жанров.
        [HttpGet]
        [Route("GetallZhanrs")]
        public async Task<IActionResult> GetallZhanrs()
        {
            return await _zhanrService.GetallZhanrs();
        }

        //o Добавление нового жанра.
        [HttpPost]
        [Route("CreateNewZhanr")]
        public async Task<IActionResult> CreateNewZhanr(Genre newZhanr)
        {
            return await _zhanrService.CreateNewZhanr(newZhanr);
        }

        //o   Редактирование жанра.
        [HttpPut]
        [Route("UpdateGenre/{ID_Zhanr}")]
        public async Task<IActionResult> UpdateGenre(int ID_Zhanr, Genre zhanr)
        {
            return await _zhanrService.UpdateGenre(ID_Zhanr, zhanr);
        }
        //o Удаление жанра.
        [HttpDelete]
        [Route("DeleteZhanr/{ID_Zhanr}")]
        public async Task<IActionResult> DeleteZhanr(int ID_Zhanr)
        {
            return await _zhanrService.DeleteZhanr(ID_Zhanr);
        }
    }
}
