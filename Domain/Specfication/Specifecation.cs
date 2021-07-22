using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Specfication
{
    public class Specification<T> : ISpecifecation<T> where T: BaseEntity
    {
        public Specification(Expression<Func<T,bool>> criteria)
        {
            this.Criteria=criteria;
        }

        public List<Expression<Func<T, object>>> Includes {get;private set;}=new List<Expression<Func<T, object>>>();

        public Expression<Func<T, bool>> Criteria {get;private set;}

        public Expression<Func<T, object>> OrderBy{get;private set;}

        public bool PaginationEnabled {get;private set;}=false;

        public int Take {get;private set;}

        public int Skip {get;private set;}

        public Expression<Func<T, object>> OrderByDescending{get;private set;}

        public void AddInclude(Expression<Func<T, object>> include){
            Includes.Add(include);
         }
        public void AddOrderBy(Expression<Func<T, object>> orderBy){
           this.OrderBy=orderBy;
        } 
        
        public void AddOrderByDescending(Expression<Func<T, object>> orderBy){
           this.OrderByDescending=orderBy;
        }

        public void AddPagination(int skip,int take){
            Skip=skip;
            Take=take;
            PaginationEnabled=true;
        }
         
    }
}