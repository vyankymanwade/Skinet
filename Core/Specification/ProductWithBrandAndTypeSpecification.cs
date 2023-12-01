using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specification
{
    public class ProductWithBrandAndTypeSpecification:Specification<Product>
    {
        public ProductWithBrandAndTypeSpecification(){
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }

        public ProductWithBrandAndTypeSpecification(int id) 
        : base(p => p.Id == id)
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }
    }
}