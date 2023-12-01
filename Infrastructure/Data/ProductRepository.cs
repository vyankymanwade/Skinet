using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {

        public readonly DbStoreContext _context;
        public ProductRepository(DbStoreContext context)
        {
            this._context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            IReadOnlyList<ProductBrand> result= await _context.ProductBrands.ToListAsync();
            return result ;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products 
                        .Include(p => p.ProductBrand)
                        .Include(p => p.ProductType)
                        .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
                return await _context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            IReadOnlyList<ProductType> result= await _context.ProductTypes.ToListAsync();
            return result ;
        }
    }
}