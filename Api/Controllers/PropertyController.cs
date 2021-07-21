using Domain.Interfaces;
using Domain.Entities;
using System.Threading.Tasks;
using Api.Helpers;
using Api.DTOS;
using Microsoft.AspNetCore.Mvc;
using Domain.EntitiesSpecification.Propertyspec;
using AutoMapper;
using System.Collections.Generic;

namespace Api.Controllers
{
    public class PropertyController : ApiBaseController
    {
        private readonly IGenericRepo<property> _genericRepo;
        private readonly IMapper _mapper;
        public PropertyController(IGenericRepo<property> genericRepo, IMapper mapper)
        {
            _mapper = mapper;
            _genericRepo = genericRepo;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<PropertyDTo>>> GetProperties(
            [FromQuery] PropertySpecParams specParams
        )
        {
            var spec = new PropertySpecwithFiltersAndIncludes(specParams);
            var specCount = new PropertySpecWithFilters(specParams);
            var totalCount =await _genericRepo.CountAsync(specCount);
            var properties =await _genericRepo.GetAllBySpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<property>,List<PropertyDTo>>(properties);
            return Ok(new Pagination<PropertyDTo>(specParams.PageIndex,specParams.PageSize,totalCount,data));
        }
    }
}