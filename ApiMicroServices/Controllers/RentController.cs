using ApiMicroServices.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace ApiMicroServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentController : Controller
    {
        private readonly HttpClient _httpClient;
        public RentController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        [HttpPost]
        [Route("AddNewRent")]
        public async Task<IActionResult> AddNewRent([FromBody] Rent newRent)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5101/api/Rent/AddNewRent", newRent);
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
        [HttpGet]
        [Route("GetCurrentRentals")]
        public async Task<IActionResult> GetCurrentRentals()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5101/api/Rent/GetCurrentRentals");
                response.EnsureSuccessStatusCode();
                var apiResponse = await response.Content.ReadFromJsonAsync<Rent[]>();
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

        [HttpGet]
        [Route("RentHistoryForReader")]
        public async Task<IActionResult> RentHistoryForReader(int Id_Reader)
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://localhost:5101/api/Rent/RentHistoryForReader/{Id_Reader}");
                response.EnsureSuccessStatusCode();
                var apiResponse = await response.Content.ReadFromJsonAsync<Rent[]>();
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
        [Route("ReturnBook")]
        public async Task<IActionResult> ReturnBook(int ID_Rent)
        {
            try
            {
                var response = await _httpClient.PutAsync($"http://localhost:5101/api/Rent/ReturnBook/{ID_Rent}", null);
                response.EnsureSuccessStatusCode();

                return Ok();
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
        [Route("GetRentHistoryForBook")]
        public async Task<IActionResult> GetRentHistoryForBook(int bookId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://localhost:5101/api/Rent/GetRentHistoryForBook/{bookId}");
                response.EnsureSuccessStatusCode();
                var apiResponse = await response.Content.ReadFromJsonAsync<Rent[]>();
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
