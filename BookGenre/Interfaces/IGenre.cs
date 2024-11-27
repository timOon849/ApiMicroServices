using BookGenre.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookGenre.Interfaces
{
    public interface IGenre
    {
        Task<IActionResult> GetallZhanrs();
        Task<IActionResult> CreateNewZhanr([FromBody] Genre newZhanr);
        Task<IActionResult> UpdateBook(int ID_Zhanr, Genre zhanr);
        Task<IActionResult> DeleteZhanr(int ID_Zhanr);
    }
}
