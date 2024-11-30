using System.Text.Json;

namespace ReadersRent.Service
{
    public class BookService
    {
        private readonly HttpClient _httpClient;

        public BookService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            
        }


        // Метод для получения книги по ID
        public async Task<BookService> GetBookByIdAsync(int bookId)
        {
            var response = await _httpClient.GetAsync($"books/{bookId}"); // Строим URL для запроса

            if (response.IsSuccessStatusCode)
            {
                // Десериализуем содержимое ответа с помощью JsonSerializer
                var content = await response.Content.ReadAsStringAsync();
                var book = JsonSerializer.Deserialize<BookService>(content);

                return book;
            }

            return null; // Возвращаем null, если запрос не успешен
        }
    }
}
