using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specification
{
    public class ProductWithFilterSpec : Specification<Product>
    {
        public ProductWithFilterSpec(ProductSpecParams productParams) : base(x => 
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) && 
            (!productParams.typeId.HasValue || x.ProductTypeId == productParams.typeId) &&
            (!productParams.brandId.HasValue || x.ProductBrandId == productParams.brandId)
        )
        {
        }
    }
}