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
        public ProductWithBrandAndTypeSpecification(string sort){
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);

            if(!string.IsNullOrEmpty(sort)){
                switch(sort){
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                    break;

                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                    break;

                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductWithBrandAndTypeSpecification(int id) 
        : base(p => p.Id == id)
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }
    }
}