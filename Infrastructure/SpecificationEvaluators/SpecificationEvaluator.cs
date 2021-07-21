using System.Linq;
using Domain.Entities;
using Domain.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SpecificationEvaluators
{
    public class SpecificationEvaluator<TEntity> where TEntity:BaseEntity
    {
        public static IQueryable<TEntity> Evaluate(IQueryable<TEntity> inputquery
        ,ISpecification<TEntity> spec)
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
            if(spec.isPaginationEnabled){
                query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query,(current,include)=>current.Include(include));
            return query;

        }
    }
}