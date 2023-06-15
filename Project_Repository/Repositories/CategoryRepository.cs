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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            return await _context.Categories.Include(x => x.Products).Where(c => c.Id == categoryId).SingleOrDefaultAsync();
        }
    }
}
