using System;
using Domain.Entities;
using Domain.Specfication;

namespace Domain.EntitiesSpecification.Statespec
{
    public class StateSpecWithIncludesAndFilters : Specification<state>
    {
        public StateSpecWithIncludesAndFilters(StateSpecParams specParams) : base(new StateParamsCriteria(specParams).GetCriteria())
        {
            AddInclude(x=>x.Cities);
            AddInclude(x=>x.country);
            AddInclude(x=>x.properties);
        }
    }
}