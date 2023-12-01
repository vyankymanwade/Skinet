using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using AutoMapper.Execution;
using Core.Entities;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToBeReturnedDTO, string>
    {
        public IConfiguration _config { get; set; }

        public ProductUrlResolver(IConfiguration config){
            this._config = config; 
        }

        public string Resolve(Product source,ProductToBeReturnedDTO destination
        ,string desMember,ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl)){
                return _config["ApiUrl"] + source.PictureUrl;
            }
            return null;
        }
    }
}