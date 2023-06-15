using Project_Core.Dtos;
using Project_Core.Services;

namespace Project_MVC.Services
{
    public class ProductApiServices : IProductApiServices
    {
        private readonly HttpClient _httpClient;

        public ProductApiServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductsWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductsWithCategoryDto>>>
                ("Products/GetProductsWithCategoryAsync");

            return response.Data;
        }
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"Products/{id}");
            return response.Data;
        }

        public async Task<ProductDto> SaveAsync(ProductDto productDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Products", productDto);

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductDto>> ();
            return responseBody.Data;
        }

        

        public async Task<bool> UpdateAsync(ProductDto productDto)
        {
            var response = await _httpClient.PutAsJsonAsync("Products", productDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Products/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
