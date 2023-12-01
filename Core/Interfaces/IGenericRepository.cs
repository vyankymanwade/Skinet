using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Core.Entities;
using Core.Specification;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<T> GetByIdAsync(int id);
        public Task<IReadOnlyList<T>> GetAllListAync();

        public Task<T> GetEntityBySpec(ISpecification<T> spec);

        public Task<IReadOnlyList<T>> GetListAsync(ISpecification<T> spec);
    }
}