using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using SampleProject.NewClient;
using SampleProject.NewClient.AuthProviders;
using SampleProject.NewClient.Services;

// Supply HttpClient instances that include access tokens when making requests to the server project



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IDialogService, DialogService>();
builder.Services.AddMudServices();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Logging.SetMinimumLevel(LogLevel.Warning);
await builder.Build().RunAsync();

