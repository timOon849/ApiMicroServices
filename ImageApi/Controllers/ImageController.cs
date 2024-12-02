using ImageApi.DB;
using ImageApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace ImageApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly DBCon _context;
        public ImageController(DBCon dBCon) 
        { 
            _context = dBCon;
        }
        [HttpPost]
        [Route("CreateImage")]
        public async Task<IActionResult> CreateImage(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var bytes = ms.ToArray();
            var image = new Image { Create_date = DateTime.Now, File = bytes, ContentType = file.ContentType };
            await _context.Image.AddAsync(image);
            await _context.SaveChangesAsync();
            return Ok(new { ImageID = image.Id_image });
        }

        [HttpGet]
        [Route("GetImage/{idImage}")]
        public async Task<IActionResult> GetImage(int idImage)
        {
            var image = await _context.Image.FindAsync(idImage);
            return File(image!.File, image!.ContentType!);
        }
    }
}
