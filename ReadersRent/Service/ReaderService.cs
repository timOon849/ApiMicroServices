using BookGenre.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReadersRent.Context;
using ReadersRent.Interfaces;
using ReadersRent.Model;
using System.Net.Http.Headers;

namespace ReadersRent.Service
{
    public class ReaderService : IReader
    {
        private readonly DBCon _context;
        private readonly HttpClient _httpClient;
        public ReaderService(DBCon context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
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
            var rentHistory = await _context.ReaderBook.Where(e => e.ID_Reader == ID_Reader).ToListAsync();
            var bookIds = rentHistory.Select(e => e.ID_Book).ToList();

            var response = await _httpClient.GetAsync("http://localhost:5205/api/Book/GetAllBooks");
            response.EnsureSuccessStatusCode();
            var apiResponse = await response.Content.ReadFromJsonAsync<Book[]>();
            var result = apiResponse.Where(b => bookIds.Contains(b.ID_Book)).ToArray();

            // Возвращаем список названий книг
            return new OkObjectResult(result);
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

        public async Task<IActionResult> UpdateReaderImage(int ID_Reader, IFormFile image)
        {
            var tecReader = await _context.Reader.FindAsync(ID_Reader);

            if (tecReader != null)
            {
                using var ms = new MemoryStream();
                await image.CopyToAsync(ms);
                var bytes = ms.ToArray();
                var content = new MultipartFormDataContent();
                var byteContent = new ByteArrayContent(bytes);
                byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse(image.ContentType);
                content.Add(byteContent, "file", image.FileName);
                var response = await _httpClient.PostAsync("http://localhost:5195/api/Image/CreateImage", content);
                response.EnsureSuccessStatusCode();
                var responseData = await response.Content.ReadFromJsonAsync<Image>();
                var imageUrl = "http://localhost:5195/api/Image/GetImage/" + responseData!.ImageID;
                tecReader.ImageUrl = imageUrl;
                await _context.SaveChangesAsync();
                return new OkObjectResult(tecReader);
            }
            else
            {
                return new BadRequestObjectResult("Книга с данным ID не найдена");
            }
        }
    }
}
