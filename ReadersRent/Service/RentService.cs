using BookGenre.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReadersRent.Context;
using ReadersRent.Interfaces;
using ReadersRent.Model;

namespace ReadersRent.Service
{
    public class RentService : IRent
    {
        private readonly DBCon _context;
        private readonly HttpClient _httpClient;
        public Book book;
        public RentService(DBCon context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();

        }

        public async Task<IActionResult> AddNewRent(Rent newRent)
        {
            var rent = new Rent()
            {
                Date_Start = DateTime.Now,
                Date_End = null,
                Srok = newRent.Srok,
                ID_Book = newRent.ID_Book,
                ID_Reader = newRent.ID_Reader,

            };
            await _context.AddAsync(rent);
            await _context.SaveChangesAsync();
            return new OkObjectResult(rent);
        }

        public async Task<IActionResult> GetCurrentRentals()
        {
            var rents = await _context.Rent.ToListAsync();
            if (rents != null)
            {
                return new OkObjectResult(rents);
            }
            else
            {
                return new BadRequestObjectResult("Аренд не обнаружено");
            }
        }

        public async Task<IActionResult> GetRentHistoryForBook(int bookId)
        {
            // Получаем информацию о книге через внешний API (BookService)
            var book = await _context.ReaderBook.Where(e => e.ID_Book == bookId).ToListAsync();

            // Проверяем, если книга не найдена в внешнем API
            if (book == null)
            {
                return new BadRequestObjectResult("Книги с таким ID не существует");
            }

            // Получаем историю аренды книги из базы данных аренды
            var rentHistory = await _context.Rent
                .Where(r => r.ID_Book == bookId)
                .ToListAsync();

            // Если история аренды найдена, возвращаем ее
            if (rentHistory != null && rentHistory.Any())
            {
                return new OkObjectResult(rentHistory);
            }

            // Если аренда для книги не найдена
            return new BadRequestObjectResult("История аренды для книги с таким ID не найдена");
        }


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

        public async Task<IActionResult> ReturnBook(int ID_Rent)
        {
            var rentHistory = await _context.Rent.FindAsync(ID_Rent);

            if (rentHistory == null)
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
