using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using todowpf.Models;

namespace todowpf.Services
{
    public class AuthService : IAuthService
    {
        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("Api");
        }

        private HttpClient _client { get; set; }
        public async Task<AuthResponse?> Login(AuthRequest request)
        {
            var response = await _client.PostAsJsonAsync("/user/login", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AuthResponse>();
            }
            return null;
        }
    }
}
