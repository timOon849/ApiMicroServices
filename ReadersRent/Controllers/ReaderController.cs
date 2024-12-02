using Microsoft.AspNetCore.Mvc;
using ReadersRent.Interfaces;
using ReadersRent.Model;
using ReadersRent.Service;

namespace ReadersRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReaderController : Controller
    {
        private readonly IReader _readersService;

        public ReaderController(IReader readersService)
        {
            _readersService = readersService;
        }

        [HttpPost]
        [Route("AddReader")]
        public async Task<IActionResult> AddReader(Reader newReader)
        {
            return await _readersService.AddReader(newReader);
        }

        [HttpGet]
        [Route("GetAllReaders")]
        public async Task<IActionResult> GetAllReaders()
        {
            return await _readersService.GetAllReaders();
        }

        [HttpGet]
        [Route("GetReaderInfo")]
        public async Task<IActionResult> GetReaderInfo(int Id_Reader)
        {
            return await _readersService.GetReaderInfo(Id_Reader);
        }

        [HttpPut]
        [Route("UpdateReader")]
        public async Task<IActionResult> UpdateReader(int Id, Reader updateReader)
        {
            return await _readersService.UpdateReader(Id, updateReader);
        }

        [HttpDelete]
        [Route("DeleteReader")]
        public async Task<IActionResult> DeleteReader(int Id_Reader)
        {
            return await _readersService.DeleteReader(Id_Reader);
        }

        [HttpGet]
        [Route("BooksRentedByReader")]
        public async Task<IActionResult> BooksRentedByReader(int ID_Reader)
        {
            return await _readersService.BooksRentedByReader(ID_Reader);
        }

        [HttpPut]
        [Route("UpdateReaderImage")]
        public async Task<IActionResult> UpdateReaderImage(int ID_Reader, IFormFile file)
        {
            return await _readersService.UpdateReaderImage(ID_Reader, file);
        }
    }
}
