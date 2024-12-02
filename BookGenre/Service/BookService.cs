using BookGenre.Model;
using Microsoft.AspNetCore.Mvc;
using BookGenre.Interfaces;
using BookGenre.DB;
using Microsoft.EntityFrameworkCore;
using BookGenre.Migrations;
using System.Net.Http.Headers;

namespace BookGenre.Service
{
    public class BookService : IBook
    {
        private readonly DBCon _context;
        private readonly HttpClient _httpClient;
        public BookService(DBCon context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<IActionResult> CreateNewBook(Books newBook)
        {
            var zhanr = await _context.Genre.FindAsync(newBook.ID_Genre);
            if (zhanr == null)
            {
                return new BadRequestObjectResult("Жанр с указанным ID не найден");
            }

            var book = new Books()
            {
                Name = newBook.Name,
                Description = newBook.Description,
                Author = newBook.Author,
                YearOfIzd = newBook.YearOfIzd,
                ID_Genre = zhanr.ID_Genre,
            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return new OkObjectResult(book);
        }

        public async Task<IActionResult> DeleteBook(int ID_Book)
        {
            var tecBook = await _context.Books.FindAsync(ID_Book);
            if (tecBook != null)
            {
                _context.Remove(tecBook);
                await _context.SaveChangesAsync();
                return new OkObjectResult("Книга удалена");
            }
            else
            {
                return new BadRequestObjectResult("Книга с данным ID не найдена или уже удалена");
            }
        }

        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _context.Books.Include(e => e.Genre).ToListAsync();
            if (books != null)
            {
                return new OkObjectResult(books);
            }
            else
            {
                return new BadRequestObjectResult("Книги не найдены");
            }
        }

        public async Task<IActionResult> GetBookNameAuthor(string? Authorbook, string? Namebook)
        {
            var books = await _context.Books.Where(a => a.Name.ToLower().Contains(Namebook) || a.Author.ToLower().Contains(Authorbook)).ToListAsync();

            if (books is null)
            {
                return new BadRequestObjectResult("Такой книги не найдено");
            }
            return new OkObjectResult(books);
        }

        public async Task<IActionResult> GetBooksByZhanr(string Namezhanr)
        {
            var TecZhanr = await _context.Genre.FirstOrDefaultAsync(a => a.Name_Genre.ToLower().Contains(Namezhanr));

            if (TecZhanr != null)
            {
                var books = await _context.Books.Where(b => b.ID_Genre == TecZhanr.ID_Genre).ToListAsync();
                if (books != null)
                {
                    return new OkObjectResult(books);
                }
                else
                {
                    return new BadRequestObjectResult("Книги с таким жанром не обнаружены");
                }
            }
            else
            {
                return new BadRequestObjectResult("Такого жанра не существует");
            }
        }

        public async Task<IActionResult> GetInfoByID(int ID_Book)
        {
            var tecBook = await _context.Books.Where(b => b.ID_Book == ID_Book).Include(e => e.Genre).FirstOrDefaultAsync();
            if (tecBook != null)
            {
                return new OkObjectResult(tecBook);
            }
            else
            {
                return new BadRequestObjectResult("Книга с данным ID не найдена");
            }
        }

        public async Task<IActionResult> UpdateBook(int ID_Book, Books book)
        {
            var tecBook = await _context.Books.FindAsync(ID_Book);

            if (tecBook != null)
            {
                var zhanr = await _context.Genre.FindAsync(tecBook.ID_Genre);
                if (zhanr != null)
                {
                    tecBook.Name = book.Name;
                    tecBook.Description = book.Description;
                    tecBook.Author = book.Author;
                    tecBook.YearOfIzd = book.YearOfIzd;
                    tecBook.ID_Genre = zhanr.ID_Genre;
                    await _context.SaveChangesAsync();
                    return new OkObjectResult(tecBook);
                }
                else
                {
                    return new BadRequestObjectResult("Не удается онаружить жанр с данным ID");
                }
            }
            else
            {
                return new BadRequestObjectResult("Книга с данным ID не найдена");
            }
        }
        public async Task<IActionResult> UpdateBookImage(int ID_Book, IFormFile image)
        {
            var tecBook = await _context.Books.FindAsync(ID_Book);

            if (tecBook != null)
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
                tecBook.ImageUrl = imageUrl;
                await _context.SaveChangesAsync();
                return new OkObjectResult(tecBook);
            }
            else
            {
                return new BadRequestObjectResult("Книга с данным ID не найдена");
            }
        }
        public async Task<IActionResult> BooksPagination(int page, int pageSize)
        {
            var totalBooks = await _context.Books.CountAsync();
            var totalPages = (int)Math.Ceiling(totalBooks / (double)pageSize);

            // Получаем книги с учетом пагинации
            var books = await _context.Books.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            if (books.Count == 0)
            {
                return new NotFoundObjectResult("Книги не найдены");
            }

            return new OkObjectResult(new
            {
                TotalBooks = totalBooks,
                TotalPages = totalPages,
                CurrentPage = page,
                Books = books
            });
        }
        public async Task<IActionResult> SearchOrFilter(string author, int? genreId, int? year)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(author) || genreId != null || year != null)
            {
                if (!string.IsNullOrEmpty(author))
                {
                    query = query.Where(b => b.Author.Contains(author));
                }

                if (genreId.HasValue)
                {
                    query = query.Where(b => b.ID_Genre == genreId.Value);
                }

                if (year.HasValue)
                {
                    query = query.Where(b => b.YearOfIzd.Year == year.Value);
                }

                var result = await query.ToListAsync();

                if (result.Any())
                {
                    return new OkObjectResult(result);
                }
                else
                {
                    return new NotFoundObjectResult("Книги не найдены");
                }
            }
            else
            {
                return new NotFoundObjectResult("Не указаны параметры для поиска");
            }
        }

    }
}
