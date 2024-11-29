using Microsoft.AspNetCore.Mvc;
using ReadersRent.Model;

namespace ReadersRent.Interfaces
{
    public interface IReader
    {
        Task<IActionResult> AddReader(Reader newReader);
        Task<IActionResult> GetAllReaders();
        Task<IActionResult> GetReaderInfo(int Id_Reader);
        Task<IActionResult> UpdateReader(int Id, Reader updateReader);
        Task<IActionResult> DeleteReader(int Id_Reader);
        Task<IActionResult> BooksRentedByReader(int ID_Reader);
    }
}
