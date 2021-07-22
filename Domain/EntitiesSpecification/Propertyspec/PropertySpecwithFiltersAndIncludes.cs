using System;
using Domain.Entities;
using Domain.Specfication;

namespace Domain.EntitiesSpecification.Propertyspec
{
    public class PropertySpecwithFiltersAndIncludes : Specification<property>
    {
        public PropertySpecwithFiltersAndIncludes(PropertySpecParams propertySpecParams) : 
        base(new PropertySpecCriteria(propertySpecParams).CreateCriteria())
        {
            AddInclude(x=>x.Bookings);
            AddInclude(x=>x.User);
            AddInclude(x=>x.City);
            AddInclude(x=>x.country);
            AddInclude(x=>x.state);
            AddInclude(x=>x.currency);
            AddOrderBy(x=>x.name);
            AddPagination(propertySpecParams.PageSize*(propertySpecParams.PageIndex-1),propertySpecParams.PageSize);
            switch(propertySpecParams.sort){
                case "priceAsc":
                AddOrderBy(x=>x.price);
                break;
                case "priceDesc":
                AddOrderByDescending(x=>x.price);
                break;
                case "address":
                AddOrderBy(x=>x.address);
                break;
                default:
                AddOrderBy(x=>x.name);
                break;
            }

        }

        public PropertySpecwithFiltersAndIncludes(int id) : base(x=>x.id==id)
        {
              AddInclude(x=>x.Bookings);
            AddInclude(x=>x.User);
            AddInclude(x=>x.City);
            AddInclude(x=>x.country);
            AddInclude(x=>x.state);
            AddInclude(x=>x.currency);
            AddOrderBy(x=>x.name);
        }
    }
}