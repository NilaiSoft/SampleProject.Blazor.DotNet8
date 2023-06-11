namespace SampleProject.Client.Pages;
public partial class Product_Edit
{
    private ProductVM? productVM = new();

    [Parameter]
    public int Id { get; set; }

    protected async override Task OnInitializedAsync()
    {
        productVM = await _httpClient.GetFromJsonAsync<ProductVM>($"api/Product/Edit/{Id}");
    }

    public async Task Edit()
    {
        var responce = await _httpClient.PostAsJsonAsync("api/Product/Edit", productVM);
        if (responce.IsSuccessStatusCode)
            _navigationManager.NavigateTo("Product_Index");
    }
}
