using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Specfication;

namespace Domain.Interfaces
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        Task<T> GetByIDAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetBySpecAsync(ISpecifecation<T> spec);
        Task<IReadOnlyList<T>> GetAllBySpecAsync(ISpecifecation<T> spec);
        Task<int> CountAsync(ISpecifecation<T> spec);


    }
}