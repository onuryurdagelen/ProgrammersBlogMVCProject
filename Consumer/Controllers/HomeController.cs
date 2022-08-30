using Consumer.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Consumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory; 
        }

        [HttpGet("bad")]
        public Task<string> Bad()
        {
            var client = new HttpClient { BaseAddress = new Uri("http://localhost:5045") };

            return client.GetStringAsync($"/homes/{Guid.NewGuid()}");
        }
        [HttpGet("simple")]
        public Task<string> Simple()
        {
            var client = _httpClientFactory.CreateClient("simple");
        
            //var client = new HttpClient { BaseAddress = new Uri("http://localhost:5045") };

            return client.GetStringAsync($"/homes/{Guid.NewGuid()}");
        }

        [HttpGet("typed")]
        public Task<string> Typed([FromServices] ApiClient client)
        {
            return client.GetHome();
        }
    }
}
