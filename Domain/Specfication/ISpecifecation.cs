using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Specfication
{
    public interface ISpecifecation<T>  where T:BaseEntity
    {
          List<Expression<Func<T,Object>>> Includes { get; }
          Func<T,bool> Criteria{get;}
          Expression<Func<T,Object>> OrderBy{get;}
          Expression<Func<T,Object>> OrderByDescending{get;}
          bool PaginationEnabled{get;}
          int Take{get;}
          int Skip{get;}
    }
}