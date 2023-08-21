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
    public partial class Edit
    {
        private ProductVM? productVM = new();
        [Parameter]
        public int Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            productVM = await _httpClient.GetFromJsonAsync<ProductVM>($"api/Product/EditAsync/{Id}");
        }

        public async Task EditData()
        {
            var responce = await _httpClient.PostAsJsonAsync("api/Product/EditAsync", productVM);
            if (responce.IsSuccessStatusCode)
                _navigationManager.NavigateTo("Product/Index");
        }

        private void BackToList()
        {
            _navigationManager.NavigateTo("Product/Index");
        }
    }
}