using global::Microsoft.AspNetCore.Components;
using MudBlazor;
using SampleProject.Shared.Dtos.Product;
using System.Net.Http.Json;

namespace SampleProject.Client.Pages.Product
{
    public partial class _AddRelatedProduct
    {
        private List<ProductDto> products = new();
        [Parameter]
        public int ProductId1 { get; set; }

        private async Task<GridData<ProductDto>> LoadServerData(GridState<ProductDto> state)
        {
            var _productDtos = await _httpClient.GetFromJsonAsync<Tuple<IList<ProductDto>, int>>($"api/Product/Index/{state.Page}/{state.PageSize}");
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

        private void SelectedItemsChanged(HashSet<ProductDto> items)
        {
            products = items.Select(x => x).ToList();
        }

        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; } = default !;

        private async Task btnSave()
        {
            var rp = new List<RelatedProductDtoForCreateOrUpdate>();
            foreach (var item in products)
            {
                rp.Add(new RelatedProductDtoForCreateOrUpdate { ProductId1 = ProductId1, ProductId2 = item.Id, DisplayOrder = 0 });
            }

            var responce = await _httpClient.PostAsJsonAsync("api/Product/RelatedCreate", rp);
            MudDialog.Close(DialogResult.Ok(responce));
        }
    }
}