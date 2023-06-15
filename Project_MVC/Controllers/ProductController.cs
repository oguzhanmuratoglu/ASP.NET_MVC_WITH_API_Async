using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Core.Dtos;
using Project_Core.Models;
using Project_Core.Services;
using Project_MVC.Filters;

namespace Project_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiServices _productApiService;
        private readonly ICategoryApiServices _categoryApiService;

        public ProductController(ICategoryApiServices categoryApiService, IProductApiServices productApiService)
        {
            _categoryApiService = categoryApiService;
            _productApiService = productApiService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _productApiService.GetProductsWithCategoryAsync();
            return View(model);
        }

        public async Task<IActionResult> Save()
        {
            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.SaveAsync(productDto);
                return RedirectToAction(nameof(Index));
            }
            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productApiService.GetByIdAsync(id);

            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name" , product.CategoryId);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.UpdateAsync(productDto);
                return RedirectToAction(nameof(Index));
            }
            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoriesDto, "Id", "Name" , productDto.CategoryId);

            return View(productDto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productApiService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
