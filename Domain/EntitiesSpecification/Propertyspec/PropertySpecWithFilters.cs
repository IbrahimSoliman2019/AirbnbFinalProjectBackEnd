using Domain.Entities;
using Domain.Specfication;
namespace Domain.EntitiesSpecification.Propertyspec
{
    public class PropertySpecWithFilters : Specification<property>
    {
        public PropertySpecWithFilters(PropertySpecParams propertySpecParams) : 
        base(new PropertySpecCriteria(propertySpecParams).CreateCriteria())
        {
            

        }
    }
}