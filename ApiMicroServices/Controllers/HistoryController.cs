using ApiMicroServices.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
