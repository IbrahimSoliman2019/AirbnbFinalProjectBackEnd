using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specfication;
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
        public async Task<IReadOnlyList<T>> GetAllBySpecAsync(ISpecifecation<T> spec)
        {
            return await ApplyingSpec(spec).ToListAsync();
        }

        public async Task<T> GetBySpecAsync(ISpecifecation<T> spec)
        {
            return await ApplyingSpec(spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecifecation<T> spec)
        {
            return await ApplyingSpec(spec).CountAsync();
        }
        private IQueryable<T> ApplyingSpec(ISpecifecation<T> spec){
            return SpecificationEvaluator<T>.Evaluate(_context.Set<T>().AsQueryable(),spec);
        }

        public async Task<T> AddAsync(T obj)
        {
          _context.Set<T>().Add(obj);
          await  _context.SaveChangesAsync();
          return obj;
        }

        public async Task<T> UpdateAsync(T obj)
        {
          _context.Set<T>().Attach(obj);
          _context.Entry(obj).State=EntityState.Modified;
          await _context.SaveChangesAsync();
          return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
           var obj =await _context.Set<T>().FindAsync(id);
           _context.Set<T>().Remove(obj);
          await _context.SaveChangesAsync();
          return true;
        }
    }
}