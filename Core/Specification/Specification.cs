using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class Specification<T> : ISpecification<T>
    {
        
        public Specification(){
            
        }

        public Specification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria {get;}

        public List<Expression<Func<T, object>>> Includes {get;} = 
                        new List<Expression<Func<T, object>>>();

        
        public Expression<Func<T,object>> OrderBy {get; private set;}
        public Expression<Func<T,object>> OrderByDescending {get; private set;}

        public int Skip {get;private set;}

        public int Take {get;private set;}

        public bool isPagingEnabled {get;private set;}
        protected void AddIncludes(Expression<Func<T, object>> include){
            Includes.Add(include);  
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderBy){
            this.OrderBy = orderBy;  
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescending){
            this.OrderByDescending = orderByDescending;  
        }

        protected void ApplyPagenation(int skip,int take){
            this.Skip = skip;
            this.Take = take;
            this.isPagingEnabled = true;
        }
    }
}