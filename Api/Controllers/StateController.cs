using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOS;
using Api.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.EntitiesSpecification.Statespec;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class StateController : ApiBaseController
    {
        private readonly IGenericRepo<state> _repo;
        private readonly IMapper _mapper;
        public StateController(IGenericRepo<state> repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<StateDTO>>> GetStates([FromQuery]StateSpecParams specParams)
        {
            var spec = new StateSpecWithIncludesAndFilters(specParams);
            var states = await _repo.ListAllBySpec(spec);
            var statefiltered = FileStreamResult(states,specParams);
            var data = _mapper.Map<List<state>, List<StateDTO>>(statefiltered);
            return Ok(data);


        }

        private List<state> FileStreamResult(IReadOnlyList<state> states, StateSpecParams specParams)
        {
          var filtered =  states
          .Where(x=>(string.IsNullOrEmpty(specParams.Country)||x.country.name==specParams.Country)).ToList();
          return filtered;
        }
    }
}