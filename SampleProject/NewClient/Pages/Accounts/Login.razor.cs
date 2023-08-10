using global::Microsoft.AspNetCore.Components;
using SampleProject.NewClient.Services;
using SampleProject.Shared.Dtos.Authentication;

namespace SampleProject.NewClient.Pages.Accounts
{
    public partial class Login
    {
        public UserForAuthenticationDto _userForAuthentication = new UserForAuthenticationDto();
        
        [Inject]
        public IAuthenticationService _authenticationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowAuthError { get; set; }
        public string Error { get; set; }

        public async Task ExecuteLogin()
        {
            ShowAuthError = false;
            var result = await _authenticationService.Login(_userForAuthentication);
            if (!result.IsAuthSuccessful)
            {
                Error = result.ErrorMessage;
                ShowAuthError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}