namespace SampleProject.Client.Pages;
public partial class Product_Index
{
    private Tuple<IList<ProductDto>, int>? _productDtos;
    private bool _hidePosition;
    private bool _loading;

    private bool _readOnly;
    private bool _isCellEditMode;
    private List<string> _events = new();
    private bool _editTriggerRowClick;
    private string searchString1;
    IList<string> test;

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
                //productDtos = await _httpClient.GetFromJsonAsync<IList<ProductDto>>("api/Product/Index");
                _navigationManager.NavigateTo("Product_Index");
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
        _productDtos = await _httpClient
            .GetFromJsonAsync<Tuple<IList<ProductDto>, int>>($"api/Product/Index/{state.Page}/{state.PageSize}");

        if (_productDtos is not null)
        {
            var data = _productDtos.Item1;

            data = data.OrderBySortDefinitions<ProductDto>(state).ToList();

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

    protected override void OnInitialized()
    {
        try
        {
            test = new List<string>();
            for (int i = 1; i < 100; i++)
            {
                test.Add($"Ehsan-{i}");
            }
        }
        catch (Exception ex)
        {

        }
    }

    private async Task<IEnumerable<string>> Search(string value, CancellationToken token)
    {
        var model = await _httpClient.GetFromJsonAsync<IList<ProductDto>>($"api/Product/SearchProduct/{value}");

        return model.Select(x => x.Name).ToList<string>();
    }
}
