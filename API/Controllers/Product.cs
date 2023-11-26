
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure.Data;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepo;
        public ProductController(IProductRepository productRepo)
        {
            this._productRepo = productRepo;
        }

        [HttpGet]
        [Route("products")]
        public async Task<IReadOnlyList<Product>> GetProducts(){
            return await _productRepo.GetProductsAsync();
        }

        [HttpGet]
        [Route("product/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id){
            return await _productRepo.GetProductByIdAsync(id);
        }

        [HttpGet]
        [Route("brands")]
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrands(){
            return await _productRepo.GetProductBrandsAsync();
        }


        [HttpGet]
        [Route("types")]
        public async Task<IReadOnlyList<ProductType>> GetProductTypes(){
            return await _productRepo.GetProductTypesAsync();
        }
    }
}