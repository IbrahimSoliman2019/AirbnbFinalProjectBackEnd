using Domain.Entities;
using Domain.Specfication;
namespace Domain.EntitiesSpecification.Propertyspec
{
    public class PropertySpecWithFilters : Specification<property>
    {
        public PropertySpecWithFilters(PropertySpecParams propertySpecParams) :
        base(X => PropertySpecCriteria.CreateCriteria(propertySpecParams)(X))
        {

            AddInclude(x => x.property_tybe);
            AddInclude(x => x.property_reviews);
            AddInclude(x => x.property_images);
        }
    }
}