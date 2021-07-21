using Domain.Entities;
using Domain.Specification;
namespace Domain.EntitiesSpecification.Propertyspec
{
    public class PropertySpecWithFilters : BaseSpecification<property>
    {
        public PropertySpecWithFilters(PropertySpecParams propertySpecParams) : 
        base(new PropertySpecCriteria(propertySpecParams).CreateCriteria())
        {
            

        }
    }
}