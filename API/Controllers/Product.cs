
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using API.DTOs;
using AutoMapper;
using API.Errors;
using API.Helpers;

namespace API.Controllers
{
    public class ProductController : BaseController
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
        public async Task<Pagination<ProductToBeReturnedDTO>> GetProducts([FromQuery]ProductSpecParams productParams){
            var spec = new ProductWithBrandAndTypeSpecification(productParams);
            IReadOnlyList<Product> pro  = await _productRepo.GetListAsync(spec);

            var countSpec = new ProductWithFilterSpec(productParams);
            int count = await _productRepo.GetCountAsync(countSpec);

            var products = await _productRepo.GetListAsync(spec);
            IReadOnlyList<ProductToBeReturnedDTO> data = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToBeReturnedDTO>>(products);

            return new Pagination<ProductToBeReturnedDTO>(productParams.PageIndex,productParams.PageSize,count,data);
        }

        [HttpGet]
        [Route("product/{id}")]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status200OK)]

        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
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