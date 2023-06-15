namespace SampleProject.Client.Pages.Product;
public partial class Create
{
    private readonly ProductVM productVM = new();
    public async Task CreateData()
    {
        var responce = await _httpClient.PostAsJsonAsync("api/Product/Create", productVM);
        if (responce.IsSuccessStatusCode)
            _navigationManager.NavigateTo("Product/Index");
    }

    private void BackToList()
    {
        _navigationManager.NavigateTo("Product/Index");
    }
}
