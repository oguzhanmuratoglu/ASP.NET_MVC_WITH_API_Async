using AutoMapper;
using Project_Core.Dtos;
using Project_Core.Models;
using Project_Core.Repositories;
using Project_Core.Services;
using Project_Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        ICategoryRepository _categoryRepository;
        IMapper _mapper;

        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
            return CustomResponseDto<List<CategoryDto>>.Success(200, categoriesDto);
        }

        public async Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            var category = await _categoryRepository.GetSingleCategoryByIdWithProductsAsync(categoryId);
            var categoryDto = _mapper.Map<CategoryWithProductsDto>(category);
            return CustomResponseDto<CategoryWithProductsDto>.Success(200, categoryDto);
        }
    }
}
