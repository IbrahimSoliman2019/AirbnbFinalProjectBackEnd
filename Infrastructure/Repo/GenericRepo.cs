using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specification;
using Infrastructure.Data;
using Infrastructure.SpecificationEvaluators;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;
        public GenericRepo(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIDAsync(int id)
        {
           return await  _context.Set<T>().FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> GetAllBySpecAsync(ISpecification<T> spec)
        {
            return await ApplyingSpec(spec).ToListAsync();
        }

        public async Task<T> GetBySpecAsync(ISpecification<T> spec)
        {
            return await ApplyingSpec(spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplyingSpec(spec).CountAsync();
        }
        private IQueryable<T> ApplyingSpec(ISpecification<T> spec){
            return SpecificationEvaluator<T>.Evaluate(_context.Set<T>().AsQueryable(),spec);
        }

    }
}