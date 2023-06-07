using MudBlazor;
using SampleProjects.Shared.Dtos;
using SampleProjects.Shared.ViewModels.Product;
using System.Net.Http.Json;

namespace SampleProject.Client.Pages
{
    public partial class Product_Index
    {
        private IList<ProductDto>? productDtos;
        private bool _hidePosition;
        private bool _loading;

        private bool _readOnly;
        private bool _isCellEditMode;
        private List<string> _events = new();
        private bool _editTriggerRowClick;

        protected override async Task OnInitializedAsync()
        {
            //productDtos = await _httpClient.GetFromJsonAsync<IList<ProductDto>>("api/Product/Index/1/10");
            productDtos = new List<ProductDto>();
        }

        public async Task DeleteAsync(int id)
        {
            bool? dialogResult = await _dialogService.ShowMessageBox(
                "Delete Confirmation",
                "Deleting can not be undone!",
                yesText: "Delete!", noText: "Cancel", cancelText: "",
                new DialogOptions { FullWidth = true });

            if (dialogResult ?? false)
            {
                var result = await _httpClient.DeleteAsync($"api/Product/Delete/{id}");
                if (result.IsSuccessStatusCode)
                {
                    productDtos = await _httpClient.GetFromJsonAsync<IList<ProductDto>>("api/Product/Index");
                }
            }
        }

        void StartedEditingItem(ProductDto item)
        {
            _events.Insert(0, $"Event = StartedEditingItem, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
        }

        void CanceledEditingItem(ProductDto item)
        {
            _events.Insert(0, $"Event = CanceledEditingItem, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
        }

        async void CommittedItemChanges(ProductDto item)
        {
            var responce = await _httpClient.PostAsJsonAsync("api/Product/Edit", item);
            if (responce.IsSuccessStatusCode)
                _navigationManager.NavigateTo("Product_Index");
            //_events.Insert(0, $"Event = CommittedItemChanges, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
        }

        private async Task<GridData<ProductDto>> LoadServerData(GridState<ProductDto> state)
        {
            var pageSize = state.PageSize;
            var pageIndex = state.Page + 1;

            productDtos = await _httpClient
                .GetFromJsonAsync<IList<ProductDto>>($"api/Product/Index/{pageIndex}/{pageSize}");

            GridData<ProductDto> data = new()
            {
                Items = productDtos,
                TotalItems = 12
            };

            return data;
        }
    }
}