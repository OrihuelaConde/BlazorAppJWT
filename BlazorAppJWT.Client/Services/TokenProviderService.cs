using System.Net.Http.Headers;
using System.Net.Http.Json;
using BlazorAppJWT.Client.DTOs;

namespace BlazorAppJWT.Client.Services
{
    public class TokenProviderService
    {
        private readonly HttpClient _httpClient;

        public TokenProviderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private string? _token;

        public async Task<string?> GetToken(string? username, string? password)
        {
            if (_token is null)
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", new UserLoginDto()
                {
                    Username = username,
                    Password = password
                });

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResultDto>();
                    _token = result!.Token;                    
                }
                else                
                    _token = null;                
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            return _token;
        }
    }
}
