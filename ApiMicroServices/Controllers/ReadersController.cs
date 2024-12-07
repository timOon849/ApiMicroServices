using ApiMicroServices.DB;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ApiMicroServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReaderController : Controller
    {
        private readonly HttpClient _httpClient;
        public ReaderController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpDelete]
        [Route("DeleteReader/{Id}")]

        public async Task<IActionResult> DeleteReader(int Id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"http://localhost:5101/api/Reader/DeleteReader/{Id}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(result))
                {
                    return StatusCode(500, "Ответ пустой.");
                }

                return Ok($"Читатель с ID {Id} успешно удален.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Ошибка при выполнении запроса: {ex.Message}");
            }
            catch (JsonException ex)
            {
                return StatusCode(500, $"Ошибка десериализации JSON: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetAllReaders")]
        public async Task<IActionResult> GetAllReaders()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5101/api/Reader/GetAllReaders");
                response.EnsureSuccessStatusCode();
                var apiResponse = await response.Content.ReadFromJsonAsync<Reader[]>();
                if (apiResponse == null)
                {
                    return StatusCode(500, "Ответ от сервиса книг не содержит данных.");
                }

                return Ok(apiResponse);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Ошибка при выполнении запроса: {ex.Message}");
            }
            catch (JsonException ex)
            {
                return StatusCode(500, $"Ошибка десериализации JSON: {ex.Message}");
            }
        }

        //[HttpGet]
        //[Route("Readers/Pagination")]
        //public async Task<IActionResult> GetReaderPagination([FromQuery] string? Name, [FromQuery] string? FName, [FromQuery] string? Contact, [FromQuery] DateTime? Birth_Day, [FromQuery] int page = 1, [FromQuery] int pageSize = 2)
        //{
        //    try
        //    {
        //        var response = await _httpClient.GetAsync("http://localhost:5101/api/Reader/Readers/Pagination");
        //        response.EnsureSuccessStatusCode();

        //        // Десериализация в объект ApiResponse
        //        var apiResponse = await response.Content.ReadFromJsonAsync<Reader[]>();

        //        if (apiResponse == null)
        //        {
        //            return StatusCode(500, "Ответ от сервиса читателей не содержит данных.");
        //        }

        //        return Ok(apiResponse);
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        return StatusCode(500, $"Ошибка при выполнении запроса: {ex.Message}");
        //    }
        //    catch (JsonException ex)
        //    {
        //        return StatusCode(500, $"Ошибка десериализации JSON: {ex.Message}");
        //    }
        //}

        [HttpGet]
        [Route("GetReaderInfo/{Id}")]
        public async Task<IActionResult> GetReaderInfo(int Id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://localhost:5101/api/Reader/GetReaderInfo/{Id}");
                response.EnsureSuccessStatusCode();

                // Десериализация в объект ApiResponse
                var ReaderResponse = await response.Content.ReadFromJsonAsync<Reader>();

                // Проверяем, получили ли данные
                if (ReaderResponse == null)
                {
                    return NotFound("Читатель в базе не найден.");
                }

                return Ok(ReaderResponse);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Ошибка при выполнении запроса: {ex.Message}");
            }
            catch (JsonException ex)
            {
                return StatusCode(500, $"Ошибка десериализации JSON: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("AddReader")]
        public async Task<IActionResult> AddReader([FromBody] Reader newReader)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5101/api/Reader/AddReader", newReader);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Ошибка при выполнении запроса: {ex.Message}");
            }
            catch (JsonException ex)
            {
                return StatusCode(500, $"Ошибка десериализации JSON: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("UpdateReader")]
        public async Task<IActionResult> UpdateReader([FromBody] Reader UpdateReader)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"http://localhost:5101/api/Reader/UpdateReader/{UpdateReader.Id_Reader}", UpdateReader);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return Ok($"Читатель с ID {UpdateReader.Id_Reader} успешно обновлен.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Ошибка при выполнении запроса: {ex.Message}");
            }
            catch (JsonException ex)
            {
                return StatusCode(500, $"Ошибка десериализации JSON: {ex.Message}");
            }
        }

        //[HttpGet]
        //[Route("/Readers/Filter")]
        //public async Task<IActionResult> GetReadersFilter([FromQuery] DateTime? DateRegist)
        //{
        //    try
        //    {
        //        var query = string.Empty;
        //        if (DateRegist.HasValue)
        //        {
        //            query = $"?DateRegist={DateRegist.Value.ToString("yyyy-MM-dd")}";
        //        }

        //        var response = await _httpClient.GetAsync($"http://localhost:5101/api/Reader/Readers/Filter");
        //        response.EnsureSuccessStatusCode();
        //        var readers = await response.Content.ReadFromJsonAsync<List<Reader>>();

        //        return Ok(readers);
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        return StatusCode(500, $"Ошибка при выполнении запроса: {ex.Message}");
        //    }
        //    catch (JsonException ex)
        //    {
        //        return StatusCode(500, $"Ошибка десериализации JSON: {ex.Message}");
        //    }
        //}
        [HttpPut]
        [Route("UpdateReaderImage/{ID_Reader}")]
        public async Task<IActionResult> UpdateReaderImage(int ID_Reader, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("Изображение не было предоставлено.");
            }

            // Здесь вы можете добавить логику для поиска читателя по ID_Reader,
            // если это необходимо, но без доступа к базе данных.

            // Пример, как можно использовать ID_Reader
            // var reader = FindReaderById(ID_Reader);
            // if (reader == null)
            // {
            //     return NotFound("Читатель с данным ID не найден.");
            // }

            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            var bytes = ms.ToArray();
            var content = new MultipartFormDataContent();
            var byteContent = new ByteArrayContent(bytes);
            byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse(image.ContentType);
            content.Add(byteContent, "file", image.FileName);

            // Вызов API другого проекта
            var response = await _httpClient.PostAsync("http://localhost:5195/api/Image/CreateImage", content);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Ошибка при загрузке изображения.");
            }

            var responseData = await response.Content.ReadFromJsonAsync<Image>();
            var imageUrl = "http://localhost:5195/api/Image/GetImage/" + responseData!.ImageID;

            // Здесь вы можете сохранить imageUrl в памяти, в кэше или другом месте,
            // если это необходимо, но без базы данных.

            return Ok(new { ImageUrl = imageUrl });
        }
    }
}
