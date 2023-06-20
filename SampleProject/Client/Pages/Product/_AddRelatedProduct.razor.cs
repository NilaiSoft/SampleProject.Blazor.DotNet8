using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
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
using SampleProjects.Shared.Dtos;

namespace SampleProject.Client.Pages.Product
{
    public partial class _AddRelatedProduct
    {
        private async Task<GridData<ProductDto>> LoadServerData(GridState<ProductDto> state)
        {
            var _productDtos = await _httpClient
                .GetFromJsonAsync<Tuple<IList<ProductDto>, int>>($"api/Product/Index/{state.Page}/{state.PageSize}");

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
    }
}