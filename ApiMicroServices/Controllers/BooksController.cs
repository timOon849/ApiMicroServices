using ApiMicroServices.DB;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ApiMicroServices.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient _httpClient;

        public BookController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        [HttpGet]
        [Route("getAllBooks")]
        public async Task<IActionResult> GetAllBooks() //РАБОТАЕТ
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5205/api/Book/GetAllBooks");
                response.EnsureSuccessStatusCode();

                // Десериализация в объект ApiResponse
                var apiResponse = await response.Content.ReadFromJsonAsync<Books[]>();

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

        [HttpDelete]
        [Route("DeleteBook/{ID_Book}")] //РАБОТАЕТ
        public async Task<IActionResult> DeleteBook(int ID_Book)
        {
            try
            {
                // Отправка DELETE-запроса
                var response = await _httpClient.DeleteAsync($"http://localhost:5205/api/Book/DeleteBook/{ID_Book}");
                response.EnsureSuccessStatusCode();

                // Проверка успешного удаления
                var result = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(result))
                {
                    return StatusCode(500, "Ответ от сервиса книг пустой.");
                }

                return Ok($"Книга с ID {ID_Book} успешно удалена.");
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
        [Route("GetBookNameAuthor")] //РАБОТАЕТ
        public async Task<IActionResult> GetBookNameAuthor(string Authorbook, string Namebook)
        {
            try
            {
                // Выполняем запрос с передачей параметров Author и Name через маршрут
                var response = await _httpClient.GetAsync($"http://localhost:5205/api/Book/GetBookNameAuthor");
                response.EnsureSuccessStatusCode();

                // Десериализация ответа в объект с одним свойством books
                var bookResponse = await response.Content.ReadFromJsonAsync<Books[]>();

                // Проверяем, получили ли данные
                if (bookResponse == null)
                {
                    return NotFound("Книга с указанными автором и названием не найдена.");
                }

                return Ok(bookResponse);
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
        [Route("GetInfoByID/{ID_Book}")]
        public async Task<IActionResult> GetInfoByID(int ID_Book) //РАБОТАЕТ
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://localhost:5205/api/Book/GetInfoByID/{ID_Book}");
                response.EnsureSuccessStatusCode();

                // Десериализация в объект ApiResponse
                var bookResponse = await response.Content.ReadFromJsonAsync<Books>();

                // Проверяем, получили ли данные
                if (bookResponse == null)
                {
                    return NotFound("Книга с указанным айди не найдена.");
                }

                return Ok(bookResponse);
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
        [Route("GetBooksByZhanr/{Namezhanr}")]
        public async Task<IActionResult> GetBooksByZhanr(string Namezhanr) //РАБОТАЕТ
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://localhost:5205/api/Books/GetBooksByZhanr/{Namezhanr}");
                response.EnsureSuccessStatusCode();

                // Десериализация в объект ApiResponse
                var bookResponse = await response.Content.ReadFromJsonAsync<Books[]>();

                // Проверяем, получили ли данные
                if (bookResponse == null)
                {
                    return NotFound("Книга с указанными автором и названием не найдена.");
                }

                return Ok(bookResponse);
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
        [Route("CreateNewBook")]
        public async Task<IActionResult> CreateNewBook([FromBody] Books newBook) //РАБОТАЕТ
        {
            try
            {
                // Отправка POST-запроса с JSON-данными
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5205/api/Book/CreateNewBook", newBook);
                response.EnsureSuccessStatusCode();

                // Проверка успешного добавления
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
        [Route("UpdateBook")]
        public async Task<IActionResult> UpdateBook([FromQuery] int ID_Book, [FromBody] Books UpdateBook) //РАБОТАЕТ
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("http://localhost:5205/api/Book/UpdateBook", UpdateBook);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return Ok($"Книга с ID {UpdateBook.ID_Book} успешно обновлена.");
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
        [Route("SearchOrFilter")]
        public async Task<IActionResult> SearchOrFilter([FromQuery] string? Author, [FromQuery] int? zhanr, [FromQuery] DateTime? year)
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5205/api/Book/SearchOrFilter");
                response.EnsureSuccessStatusCode();
                var bookResponse = await response.Content.ReadFromJsonAsync<Books[]>();
                if (bookResponse == null)
                {
                    return NotFound("Книга с указанными автором и названием не найдена.");
                }
                return Ok(bookResponse);
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
        [Route("BooksPagination")]
        public async Task<IActionResult> BooksPagination([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5205/api/Book/BooksPagination"); 
                response.EnsureSuccessStatusCode();

                // Десериализация в объект ApiResponse
                var apiResponse = await response.Content.ReadFromJsonAsync<Books[]>();

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
        [HttpPut]
        [Route("UpdateBookImage/{ID_Book}")]
        public async Task<IActionResult> UpdateReaderImage(int ID_Book, IFormFile image)
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
