using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using MudBlazor;
using SampleProject.Shared.Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SampleProject.NewClient.Services;

namespace SampleProject.NewClient.Shared
{
    public partial class LoginDisplay
    {
        [Inject]
        public IAuthenticationService _authenticationService { get; set; }

        private async void BeginLogOut()
        {
            await _authenticationService.Logout();
            Navigation.NavigateToLogout("authentication/logout");
        }
    }
}