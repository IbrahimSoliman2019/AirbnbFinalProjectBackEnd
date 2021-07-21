using System;
using Domain.Entities;
using Domain.Specification;

namespace Domain.EntitiesSpecification.Statespec
{
    public class StateSpecWithIncludesAndFilters : BaseSpecification<state>
    {
        public StateSpecWithIncludesAndFilters(StateSpecParams specParams) : base(new StateParamsCriteria(specParams).GetCriteria())
        {
            AddInclude(x=>x.Cities);
            AddInclude(x=>x.country);
            AddInclude(x=>x.properties);
        }
    }
}