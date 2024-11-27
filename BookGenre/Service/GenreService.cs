using BookGenre.DB;
using BookGenre.Model;
using Microsoft.AspNetCore.Mvc;
using BookGenre.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookGenre.Service
{
    public class GenreService : IGenre
    {
        private readonly DBCon _context;
        public GenreService(DBCon context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateNewZhanr(Genre newZhanr)
        {
            var zhanr = new Genre()
            {
                Name_Genre = newZhanr.Name_Genre
            };

            await _context.Genre.AddAsync(zhanr);
            await _context.SaveChangesAsync();
            return new OkObjectResult(zhanr);
        }

        public async Task<IActionResult> DeleteZhanr(int ID_Zhanr)
        {
            var tecZhanr = await _context.Books.FindAsync(ID_Zhanr);
            if (tecZhanr != null)
            {
                _context.Remove(tecZhanr);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            else
            {
                return new BadRequestObjectResult("Жанр с данным ID не найден или уже удалёен");
            }
        }

        public async Task<IActionResult> GetallZhanrs()
        {
            var zhanrs = await _context.Genre.ToListAsync();
            if (zhanrs != null)
            {
                return new OkObjectResult(zhanrs);
            }
            else
            {
                return new NotFoundObjectResult("Жанров не обнаружено");
            }
        }

        public async Task<IActionResult> UpdateGenre(int ID_Zhanr, Genre zhanr)
        {
            var tecZhanr = await _context.Genre.FindAsync(ID_Zhanr);

            if (tecZhanr != null)
            {
                tecZhanr.Name_Genre = zhanr.Name_Genre;
                await _context.SaveChangesAsync();
                return new OkObjectResult(tecZhanr);
            }
            else
            {
                return new BadRequestObjectResult("Жанр с данным ID не найден");
            }
        }
    }
}
