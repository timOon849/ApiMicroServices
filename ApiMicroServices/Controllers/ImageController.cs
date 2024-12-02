namespace ApiMicroServices.Controllers
{
    public class ImageController
    {
        private readonly HttpClient _httpClient;
        public ImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }


    }
}
