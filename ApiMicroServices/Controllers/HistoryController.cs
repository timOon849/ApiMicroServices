using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiMicroServices.Controllers
{
    
    public class HistoryController : Controller
    {
        private readonly HttpClient _httpClient;
        public HistoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
    }
}
