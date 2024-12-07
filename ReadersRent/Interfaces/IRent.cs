using Microsoft.AspNetCore.Mvc;
using ReadersRent.Model;

namespace ReadersRent.Interfaces
{
    public interface IRent
    {
        Task<IActionResult> AddNewRent(Rent newRent);
        Task<IActionResult> ReturnBook(int ID_Rent);
        Task<IActionResult> RentHistoryForReader(int Id_Reader);
        Task<IActionResult> GetCurrentRentals();
        Task<IActionResult> GetRentHistoryForBook(int idbook);
    }
}
