using System.Linq;
using Domain.Entities;
using Domain.Specfication;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SpecificationEvaluators
{
    public class SpecificationEvaluator<TEntity> where TEntity:BaseEntity
    {
        public static IQueryable<TEntity> Evaluate(IQueryable<TEntity> inputquery
        ,ISpecifecation<TEntity> spec)
        {
            var query = inputquery;
            if(spec.Criteria!=null){
                query.Where(spec.Criteria);
            }
            if(spec.OrderBy!=null){
                query.OrderBy(spec.OrderBy);
            }
             if(spec.OrderByDescending!=null){
                query.OrderBy(spec.OrderByDescending);
            }
            if(spec.PaginationEnabled){
                query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query,(current,include)=>current.Include(include));
            return query;

        }
    }
}