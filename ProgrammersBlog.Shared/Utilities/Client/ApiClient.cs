using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Consumer.Client
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private string _guidy;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
            //base configuration
            _guidy = Guid.NewGuid().ToString();
        }

        public Task<string> GetHome()
        {
            return _httpClient.GetStringAsync($"/homes/{_guidy}");
        }
    }
}
