using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Specification;

namespace Domain.Interfaces
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        Task<T> GetByIDAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetBySpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAllBySpecAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);


    }
}