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
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<ProductsWithCategoryDto>>> GetProductsWithCategoryAsync()
        {
            var products = await _productRepository.GetProductsWithCategoryAsync();
            var productsDto = _mapper.Map<List<ProductsWithCategoryDto>>(products);
            return CustomResponseDto<List<ProductsWithCategoryDto>>.Success(200, productsDto);
        }
    }
}
