using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        public Task<IReadOnlyList<Product>> GetProductsAsync();

        public Task<Product> GetProductByIdAsync(int id);

        public Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();

        public Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        
    }
}