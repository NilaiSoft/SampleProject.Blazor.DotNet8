using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SampleProject.Client.AuthProviders;
using SampleProject.Shared.Dtos.Authentication;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SampleProject.Client.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        [Inject]
        public AuthenticationStateProvider _authStateProvider { get; set; }

        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication)
        {
            var content = JsonSerializer.Serialize(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var authRequest = await _client.PostAsync("api/accounts/Login", bodyContent);
            var authContent = await authRequest.Content.ReadAsStringAsync();
            var authResult = JsonSerializer.Deserialize<AuthResponseDto>(authContent, _options);

            if (!authRequest.IsSuccessStatusCode)
                return authResult;

            await _localStorage.SetItemAsync("authToken", authResult.Token);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(userForAuthentication.Email);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authResult.Token);

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
