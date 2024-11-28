using ReadersAndRent.DB;
using Microsoft.AspNetCore.Mvc;
using ReadersAndRent.Interfaces;
using ReadersAndRent.Model;
using Microsoft.EntityFrameworkCore;

namespace ReadersAndRent.Service
{
    public class HistoryService : IHistoryService
    {
        private readonly DBCon _context;
        public HistoryService(DBCon context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddNewRent(int srok, int Id_Book, int IdReader)
        {
            var rent = new Rent()
            {
                Date_Start = DateTime.Now,
                Date_End = null,
                Srok = srok,
                ID_Book = Id_Book,
                ID_Reader = IdReader,

            };
            await _context.AddAsync(rent);
            await _context.SaveChangesAsync();
            return new OkObjectResult(rent);
        }

        public async Task<IActionResult> GetCurrentRentals()
        {
            var currentRentals = await _context.Rent.Where(r => r.Date_End == null).ToListAsync();
            return new OkObjectResult(currentRentals);
        }

        //public async Task<IActionResult> GetRentHistoryForBook(int bookId)
        //{
        //    var rentHistory = await _context.Rent.Where(r => r.ID_Book == bookId).ToListAsync();
        //    var book = await _context.Books.FindAsync(bookId);
        //    if (rentHistory != null && book != null)
        //    {
        //        return new OkObjectResult(rentHistory);
        //    }
        //    else if (book == null)
        //    {
        //        return new BadRequestObjectResult("Книги с таким ID не существует");
        //    }
        //    else
        //    {
        //        return new BadRequestObjectResult("История аренды для книги с таким ID не найдена");
        //    }
        //}

        public async Task<IActionResult> RentHistoryForReader(int Id_Reader)
        {
            var readerHistory = await _context.Rent.Where(r => r.ID_Reader == Id_Reader).ToListAsync();
            if (readerHistory != null)
            {
                return new OkObjectResult(readerHistory);
            }
            else
            {
                return new BadRequestObjectResult("Пользователь с таким ID не обнаружен");
            }
        }

        public async Task<IActionResult> ReturnBook(int ID_History)
        {
            var rentHistory = await _context.Rent.FindAsync(ID_History);

            if (rentHistory == null || rentHistory.Date_End != null)
            {
                return new BadRequestObjectResult("Информация об аренде не найдена");
            }

            rentHistory.Date_End = DateTime.Now;
            _context.Rent.Update(rentHistory);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

    }
}
