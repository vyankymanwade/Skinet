
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure.Data;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly DbStoreContext _context;
        public ProductController(DbStoreContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("products")]
        public async Task<ActionResult<List<Product>>> getProducts(){
            return await _context.Products.ToListAsync<Product>();
        }

        [HttpGet]
        [Route("product/{id}")]
        public async Task<ActionResult<Product>> getProduct(int id){
            return await _context.Products.FindAsync(id);
        }
    }
}