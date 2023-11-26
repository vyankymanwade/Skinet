using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data
{
    public class StoreContextSeedData
    {
        public static async Task SeedDataAsync(DbStoreContext context){
            if(!context.ProductTypes.Any()){
                var data = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var typesData = JsonSerializer.Deserialize<List<ProductType>>(data);
                context.ProductTypes.AddRange(typesData);
            }

            if(!context.ProductBrands.Any()){
                var data = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brandData = JsonSerializer.Deserialize<List<ProductBrand>>(data);
                context.ProductBrands.AddRange(brandData);
            }

            if(!context.ProductTypes.Any()){
                var data = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var productData = JsonSerializer.Deserialize<List<Product>>(data);
                context.Products.AddRange(productData);
            }

            if(context.ChangeTracker.HasChanges()){
                await context.SaveChangesAsync();
            }
        }
    }
}