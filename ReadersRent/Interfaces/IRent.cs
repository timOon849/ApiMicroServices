using Microsoft.AspNetCore.Mvc;

namespace ReadersRent.Interfaces
{
    public interface IRent
    {
        Task<IActionResult> AddNewRent(int srok, int Id_Book, int IdReader);
        Task<IActionResult> ReturnBook(int ID_History);
        Task<IActionResult> RentHistoryForReader(int Id_Reader);
        Task<IActionResult> GetCurrentRentals();
        Task<IActionResult> GetRentHistoryForBook(int idbook);
    }
}
