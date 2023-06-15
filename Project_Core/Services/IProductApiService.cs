using Project_Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Core.Services
{
    public interface IProductApiServices
    {
        public Task<List<ProductsWithCategoryDto>> GetProductsWithCategoryAsync();
        public Task<ProductDto> GetByIdAsync(int id);
        public Task<ProductDto> SaveAsync(ProductDto productDto);
        public Task<bool> UpdateAsync(ProductDto productDto);
        public Task<bool> DeleteAsync(int id);

    }
}
