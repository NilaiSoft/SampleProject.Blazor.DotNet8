using SampleProject.Shared.Dtos;
using SampleProjects.Shared.Dtos;

namespace SampleProject.Client.Pages.Product
{
    public partial class _CreateOrUpdate
    {
        [Parameter]
        public ProductVM? productVM { get; set; }

        private Tuple<IList<RelatedProductDto>, int>? _relatedProductDtos;

        private async Task<GridData<RelatedProductDto>> LoadRelatedProducts(GridState<RelatedProductDto> state)
        {
            _relatedProductDtos = await _httpClient
                .GetFromJsonAsync<Tuple<IList<RelatedProductDto>, int>>($"api/Product/RelatedProducts/{state.Page}/{state.PageSize}");

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