using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReadersRent.Context;
using ReadersRent.Interfaces;
using ReadersRent.Model;

namespace ReadersRent.Service
{
    public class ReaderService : IReader
    {
        private readonly DBCon _context;
        private readonly BookService _bookService;
        public ReaderService(DBCon context, BookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }

        public async Task<IActionResult> AddReader(Reader newReader)
        {
            if (newReader.Birthday == default)
            {
                newReader.Birthday = DateTime.Today.AddYears(-6);
            }

            var reader = new Reader()
            {
                Name = newReader.Name,
                FName = newReader.FName,
                Birthday = newReader.Birthday,
                Email = newReader.Email,
            };

            await _context.AddAsync(reader);
            await _context.SaveChangesAsync();
            return new OkObjectResult(reader);
        }

        public async Task<IActionResult> BooksRentedByReader(int ID_Reader)
        {
            // Получаем все активные аренды для этого читателя
            var rentHistory = await _context.Rent
                .Where(r => r.ID_Reader == ID_Reader && r.Date_End == null)
                .ToListAsync();

            var bookNames = new List<string>();

            foreach (var rent in rentHistory)
            {
                // Используем BookService для получения данных о книге по ID
                var book = await _bookService.GetBookByIdAsync(rent.ID_Book);

                //if (book != null)
                //{
                //    bookNames.Add(book.Name); // Добавляем название книги в список
                //}
            }

            if (!bookNames.Any()) // Если список пуст, возвращаем ошибку
            {
                return new BadRequestObjectResult("Книги не найдены для данного читателя.");
            }

            // Возвращаем список названий книг
            return new OkObjectResult(bookNames);
        }

        public async Task<IActionResult> DeleteReader(int Id_Reader)
        {
            var reader = await _context.Reader.FirstOrDefaultAsync(a => a.Id_Reader == Id_Reader);
            if (reader == null)
            {
                return new BadRequestObjectResult("Такой читатель не найден");
            }
            else
            {
                _context.Remove(reader);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
        }

        public async Task<IActionResult> GetAllReaders()
        {
            var readers = await _context.Reader.ToListAsync();
            if (readers != null)
            {
                return new OkObjectResult(readers);
            }
            else
            {
                return new BadRequestObjectResult("Читателей не обнаружено");
            }
        }

        public async Task<IActionResult> GetReaderInfo(int Id_Reader)
        {
            var readers = await _context.Reader.FirstOrDefaultAsync(a => a.Id_Reader == Id_Reader);
            if (readers is null)
            {
                return new BadRequestObjectResult("Читатель с таким ID не найден");
            }
            return new OkObjectResult(readers);
        }

        public async Task<IActionResult> UpdateReader(int Id, Reader updateReader)
        {
            var reader = await _context.Reader.FirstOrDefaultAsync(a => a.Id_Reader == Id);

            if (reader is null)
            {
                return new BadRequestObjectResult("Читатель с таким ID не найден");
            }
            else
            {
                reader.Name = updateReader.Name;
                reader.FName = updateReader.FName;
                reader.Birthday = updateReader.Birthday;
                reader.Email = updateReader.Email;

                await _context.SaveChangesAsync();
                return new OkObjectResult(reader);
            }
        }
    }
}
