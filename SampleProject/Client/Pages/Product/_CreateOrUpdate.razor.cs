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
using SampleProjects.Shared.ViewModels.Product;

namespace SampleProject.Client.Pages.Product
{
    public partial class _CreateOrUpdate
    {
        [Parameter]
        public ProductVM? productVM { get; set; }

        [Parameter]
        public int productId { get; set; }

        MudDataGrid<RelatedProductDto> grdRelatedProducts;
        private Tuple<IList<RelatedProductDto>, int>? _relatedProductDtos;
        private async Task<GridData<RelatedProductDto>> LoadRelatedProducts(GridState<RelatedProductDto> state)
        {
            if (productId == 0)
                return new GridData<RelatedProductDto>();
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

        private async void ShowProductList()
        {
            var closeOnEscapeKey = new DialogOptions()
            {
                CloseOnEscapeKey = false,
                CloseButton = true,
                DisableBackdropClick = false,
                ClassBackground = "my-custom-class"
            };
            var prm = new DialogParameters();
            prm.Add("ProductId1", productId);
            var result = await (await _dialogService.ShowAsync<_AddRelatedProduct>("Select Product For Related", prm, closeOnEscapeKey)).GetReturnValueAsync<HttpResponseMessage>();
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await grdRelatedProducts.ReloadServerData();
            }
        }
    }
}