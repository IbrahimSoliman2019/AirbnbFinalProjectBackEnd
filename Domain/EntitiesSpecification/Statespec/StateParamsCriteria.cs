using System;
using Domain.Entities;

namespace Domain.EntitiesSpecification.Statespec
{
    public class StateParamsCriteria
    {
        private readonly StateSpecParams _params;
        public StateParamsCriteria(StateSpecParams Params)
        {
            _params = Params;
        }

        public  Func<state,bool> GetCriteria(){
            Func<state,bool> criteria = x=>{
                return (string.IsNullOrEmpty(x.country.name)||x.country.name==_params.Country);
            };
            return criteria; 



        }
    }
}