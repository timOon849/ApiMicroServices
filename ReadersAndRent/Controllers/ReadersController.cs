using Microsoft.AspNetCore.Mvc;
using ReadersAndRent.Interfaces;
using ReadersAndRent.Model;

namespace ReadersAndRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadersController : Controller
    {
        private readonly IReaderService _readersService;

        public ReadersController(IReaderService readersService)
        {
            _readersService = readersService;
        }

        [HttpPost]
        [Route("AddReader")]
        public async Task<IActionResult> AddReader(Readers newReader)
        {
            return await _readersService.AddReader(newReader);
        }

        [HttpGet]
        [Route("getAllReaders")]
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
        public async Task<IActionResult> UpdateReader(int Id, Readers updateReader)
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
    }
}
