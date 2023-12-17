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
        public ProductWithBrandAndTypeSpecification(ProductSpecParams productParams):base(
            x => 
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) && 
            (!productParams.brandId.HasValue || x.ProductBrandId == productParams.brandId) &&
            (!productParams.typeId.HasValue || x.ProductTypeId == productParams.typeId) 
        ){
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
            ApplyPagenation(productParams.PageSize * (productParams.PageIndex-1),productParams.PageSize);

            if(!string.IsNullOrEmpty(productParams.sort)){
                switch(productParams.sort){
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