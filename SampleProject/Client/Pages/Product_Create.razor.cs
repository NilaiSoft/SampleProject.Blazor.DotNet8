using SampleProjects.Shared.ViewModels.Product;
using System.Net.Http.Json;

namespace SampleProject.Client.Pages
{
    public partial class Product_Create
    {
        private readonly ProductVM productVM = new();

        public async Task Create()
        {
            //productVM.GuidRecord = Guid.NewGuid();

            var responce = await _httpClient.PostAsJsonAsync("api/Product/Create", productVM);
            if (responce.IsSuccessStatusCode)
                _navigationManager.NavigateTo("Product_Index");
        }
    }
}