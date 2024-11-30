using ApiMicroServices.DB;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace ApiMicroServices.Controllers
{
    public class ReadersController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReadersController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
    }
}
