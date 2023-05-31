using SampleProjects.Shared.Dtos;
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
            productDtos = await _httpClient.GetFromJsonAsync<IList<ProductDto>>("api/Product/Index");
        }

        public async Task Delete(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Product/Delete/{id}");
            if (result.IsSuccessStatusCode)
            {
                productDtos = await _httpClient.GetFromJsonAsync<IList<ProductDto>>("api/Product/Index");
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

        void CommittedItemChanges(ProductDto item)
        {
            _events.Insert(0, $"Event = CommittedItemChanges, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
        }
    }
}