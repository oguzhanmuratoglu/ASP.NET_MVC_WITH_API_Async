using Project_Core.Dtos;
using Project_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<CustomResponseDto<List<ProductsWithCategoryDto>>> GetProductsWithCategoryAsync();
    }
}
