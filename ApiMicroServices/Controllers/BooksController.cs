using ApiMicroServices.DB;
using Microsoft.AspNetCore.Mvc;
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
        [Route("GetBookNameAuthor/{Authorbook}/{Name}")] //РАБОТАЕТ
        public async Task<IActionResult> GetBookNameAuthor(string Authorbook, string Namebook)
        {
            try
            {
                // Выполняем запрос с передачей параметров Author и Name через маршрут
                var response = await _httpClient.GetAsync($"http://localhost:5205/api/Book/GetBookNameAuthor/{Authorbook}/{Namebook}");
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
        [Route("api/books")]
        public async Task<IActionResult> GetBooks([FromQuery] string? Name, [FromQuery] string? Author, [FromQuery] int? zhanr, [FromQuery] DateTime? year)
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5205/api/Book/api/books");
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
        [Route("api/Books/BooksPagination")]
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
    }
}
