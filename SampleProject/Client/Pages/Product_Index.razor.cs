using SampleProjects.Shared.Dtos;
using System;
using System.Net.Http.Json;

namespace SampleProject.Client.Pages
{
    public partial class Product_Index
    {
        private IList<ProductDto>? productDtos;

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

        void OnPersonDbClicked(object item)
        {
            //var person = item as Person;
            //if (person == null)
            //{
            //    _currentSelectedPerson = "noone";
            //    return;
            //}

            //_currentSelectedPerson = $"{person.Firstname} {person.Lastname}";
        }
    }
}