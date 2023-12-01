using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<T> where T:BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery,ISpecification<T>spec){
            var query = inputQuery;

            if(spec.Criteria != null){
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query,(current,include) => current.Include(include));
            return query;
        }
    }
}