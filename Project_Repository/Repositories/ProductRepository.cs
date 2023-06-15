using Microsoft.EntityFrameworkCore;
using Project_Core.Models;
using Project_Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductsWithCategoryAsync()
        {
            //Eager Loading
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }
    }
}
