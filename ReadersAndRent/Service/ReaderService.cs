using Microsoft.AspNetCore.Mvc;
using ReadersAndRent.Interfaces;
using ReadersAndRent.Model;
using ReadersAndRent.DB;
using Microsoft.EntityFrameworkCore;

namespace ReadersAndRent.Service
{
    public class ReaderService : IReaderService
    {
        private readonly DBCon _context;
        public ReaderService(DBCon context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddReader(Readers newReader)
        {
            if (newReader.Birthday == default)
            {
                newReader.Birthday = DateTime.Today.AddYears(-6);
            }

            var reader = new Readers()
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
            var rentHistory = await _context.Rent.Where(r => r.ID_Reader == ID_Reader && r.Date_End == null).ToListAsync();
            var bookNames = new List<string>();
            foreach (var rent in rentHistory)
            {
                var book = await _context.Books.FindAsync(rent.ID_Book); // Получение книги по ее ID
                if (book != null)
                {
                    bookNames.Add(book.Name); // Добавление названия книги в список
                }
            }
            if (rentHistory == null)
            {
                return new BadRequestObjectResult("Книги не найдены для данного читателя."); // Сообщение, если нет арендованных книг
            }
            else
            {
                return new OkObjectResult(bookNames);
            }
        }

        public async Task<IActionResult> DeleteReader(int Id_Reader)
        {
            var reader = await _context.Readers.FirstOrDefaultAsync(a => a.Id_Reader == Id_Reader);
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
            var readers = await _context.Readers.ToListAsync();
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
            var readers = await _context.Readers.FirstOrDefaultAsync(a => a.Id_Reader == Id_Reader);
            if (readers is null)
            {
                return new BadRequestObjectResult("Читатель с таким ID не найден");
            }
            return new OkObjectResult(readers);
        }

        public async Task<IActionResult> UpdateReader(int Id, Readers updateReader)
        {
            var reader = await _context.Readers.FirstOrDefaultAsync(a => a.Id_Reader == Id);

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
