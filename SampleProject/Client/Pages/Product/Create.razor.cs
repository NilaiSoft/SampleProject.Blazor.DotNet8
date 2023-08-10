namespace SampleProject.Client.Pages.Product;
public partial class Create
{
    private readonly ProductVM productVM = new();
    public async Task CreateData()
    {
        var response = await _httpClient.PostAsJsonAsync("api/Product/Create", productVM);

        var insertedId = await response.Content.ReadAsStringAsync();

        if (insertedId == "0")
            return;

        if (response.IsSuccessStatusCode)
            _navigationManager.NavigateTo($"Product/Edit/{insertedId}");
    }

    private void BackToList()
    {
        _navigationManager.NavigateTo("Product/Index");
    }
}
