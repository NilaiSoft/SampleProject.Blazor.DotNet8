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
using SampleProject.Shared.Dtos.Product;

namespace SampleProject.Client.Pages.Product
{
    public partial class Index
    {
        private Tuple<IList<RelatedProductDto>, int>? _relatedProductDtos;
        public int productId { get; set; }

        private MudDataGrid<ProductDto> grdProducts;
        private Tuple<IList<ProductDto>, int>? _productDtos;
        private bool _hidePosition;
        private bool _loading;
        private bool _readOnly;
        private bool _isCellEditMode;
        private List<string> _events = new();
        private bool _editTriggerRowClick;
        private string searchString1;
        public async Task DeleteAsync(int id)
        {
            bool? dialogResult = await _dialogService.ShowMessageBox("Delete Confirmation", "Deleting can not be undone!", yesText: "Delete!", noText: "Cancel", cancelText: "", new DialogOptions { FullWidth = true });
            if (dialogResult ?? false)
            {
                var result = await _httpClient.DeleteAsync($"api/Product/DeleteAsync/{id}");
                if (result.IsSuccessStatusCode)
                {
                    await grdProducts.ReloadServerData();
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
            var responce = await _httpClient.PostAsJsonAsync("api/Product/EditAsync", item);
            if (responce.IsSuccessStatusCode)
                _navigationManager.NavigateTo("Product/Index");
        //_events.Insert(0, $"Event = CommittedItemChanges, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
        }

        private async Task<GridData<ProductDto>> LoadServerData(GridState<ProductDto> state)
        {
            _productDtos = await _httpClient.GetFromJsonAsync<Tuple<IList<ProductDto>, int>>($"api/Product/Index/{state.Page}/{state.PageSize}");
#region Cache Method
            //    if (memoryCache.TryGetValue($"product-productList-{state.Page}-{state.PageSize}"
            //, out _productDtos))
            //    {
            //        // Data successfully read from cache
            //        // use myValue
            //    }
            //    else
            //    {
            //        // There was no data in the cache
            //        // Perform calculations and calculate the new value
            //        // Then insert the new value into the cache using memoryCache.Set
            //        memoryCache.Set($"product-productList-{state.Page}-{state.PageSize}", await _httpClient
            //            .GetFromJsonAsync<Tuple<IList<ProductDto>, int>>($"api/Product/Index/{state.Page}/{state.PageSize}")
            //        , TimeSpan.FromMinutes(10));
            //        memoryCache.TryGetValue($"product-productList-{state.Page}-{state.PageSize}"
            //            , out _productDtos);
            //    }
#endregion
            if (_productDtos is not null)
            {
                var data = _productDtos.Item1;
                data = data.OrderBySortDefinitions(state).ToList();
                GridData<ProductDto> model = new()
                {
                    Items = data,
                    TotalItems = _productDtos.Item2
                };
                return model;
            }

            return new GridData<ProductDto>();
        }

        private Func<ProductDto, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(searchString1))
                return true;
            if (x.Name.Contains(searchString1, StringComparison.OrdinalIgnoreCase))
                return true;
            if (x.ShortDescription.Contains(searchString1, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        };

        private async Task<IEnumerable<string>> SearchProduct(string value, CancellationToken token)
        {
            var model = await _httpClient.GetFromJsonAsync<IList<ProductDto>>($"api/Product/SearchProduct/{value}");
            return model.Select(x => x.Name).ToList<string>();
        }

        private void Edit(int Id)
        {
            _navigationManager.NavigateTo($"Product/Edit/{Id}");
        }

        public EventCallback RowClick(int id)
        {
            productId = id;
            return new EventCallback();
        }

        private async Task<GridData<RelatedProductDto>> LoadRelatedProducts(GridState<RelatedProductDto> state)
        {
            _relatedProductDtos = await _httpClient.GetFromJsonAsync<Tuple<IList<RelatedProductDto>, int>>($"api/Product/RelatedProducts/{productId}/{state.Page}/{state.PageSize}");
            if (_relatedProductDtos is not null)
            {
                var data = _relatedProductDtos.Item1;
                data = data.OrderBySortDefinitions(state).ToList();
                GridData<RelatedProductDto> model = new()
                {
                    Items = data,
                    TotalItems = _relatedProductDtos.Item2
                };
                return model;
            }

            return new GridData<RelatedProductDto>();
        }
    }
}