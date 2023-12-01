
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using API.DTOs;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        public readonly IGenericRepository<Product> _productRepo;
        public readonly IGenericRepository<ProductBrand> _productBrandRepo;
        public readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductController
        (
            IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper
        )
        {
            _productRepo = productRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("products")]
        public async Task<IReadOnlyList<ProductToBeReturnedDTO>> GetProducts(){
            var spec = new ProductWithBrandAndTypeSpecification();
            IReadOnlyList<Product> pro  = await _productRepo.GetListAsync(spec);

            return _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToBeReturnedDTO>>(pro);
        }

        [HttpGet]
        [Route("product/{id}")]
        public async Task<ActionResult<ProductToBeReturnedDTO>> GetProduct(int id){
            var spec = new ProductWithBrandAndTypeSpecification(id);
            Product pro = await _productRepo.GetEntityBySpec(spec);

            return _mapper.Map<Product,ProductToBeReturnedDTO>(pro);
        }

        [HttpGet]
        [Route("brands")]
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrands(){
            return await _productBrandRepo.GetAllListAync();
        }


        [HttpGet]
        [Route("types")]
        public async Task<IReadOnlyList<ProductType>> GetProductTypes(){
            return await _productTypeRepo.GetAllListAync();
        }
    }
}