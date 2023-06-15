using Project_Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Core.Services
{
    public interface ICategoryApiServices
    {
        public Task<List<CategoryDto>> GetAllAsync();
        public Task<CategoryWithProductsDto> GetSingleCategoryByIdWithProductsAsync(int id);
    }
}
