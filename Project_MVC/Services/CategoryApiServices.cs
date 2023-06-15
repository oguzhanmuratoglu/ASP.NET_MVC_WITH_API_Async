using Project_Core.Dtos;
using Project_Core.Services;

namespace Project_MVC.Services
{
    public class CategoryApiServices : ICategoryApiServices
    {
        private readonly HttpClient _httpClient;

        public CategoryApiServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<CategoryDto>>>("categories");
            return response.Data;
        }

        public async Task<CategoryWithProductsDto> GetSingleCategoryByIdWithProductsAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<CategoryWithProductsDto>>
                ($"categories/GetSingleCategoryByIdWithProductsAsync/{id}");

            return response.Data;
        }

    }
}
