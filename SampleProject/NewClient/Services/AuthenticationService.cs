using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SampleProject.NewClient.AuthProviders;
using SampleProject.Shared.Dtos.Authentication;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SampleProject.NewClient.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        [Inject]
        public AuthenticationStateProvider _authStateProvider { get; set; }

        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _localStorage = localStorage;
        }

        public async Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication)
        {
            var content = JsonSerializer.Serialize(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var authResult = await _client.PostAsync("api/accounts/Login", bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthResponseDto>(authContent, _options);

            if (!authResult.IsSuccessStatusCode)
                return result;

            await _localStorage.SetItemAsync("authToken", result.Token);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(userForAuthentication.Email);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

            return new AuthResponseDto { IsAuthSuccessful = true };
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            throw new NotImplementedException();
        }
    }
}
