using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using MudBlazor;
using SampleProject.Shared.Dtos;
using SampleProjects.Shared.ViewModels.Product;

namespace SampleProject.Client.Pages.Product
{
    public partial class Create
    {
        private readonly ProductVM productVM = new();
        public async Task CreateData()
        {
            var response = await _httpClient.PostAsJsonAsync("api/Product/Create", productVM);
            var insertedId = await response.Content.ReadAsStringAsync();
            if (insertedId == "0")
                return;
            if (response.IsSuccessStatusCode)
                _navigationManager.NavigateTo($"Product/Edit/{insertedId}");
        }

        private void BackToList()
        {
            _navigationManager.NavigateTo("Product/Index");
        }
    }
}