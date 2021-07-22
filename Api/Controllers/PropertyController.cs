using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DTOS;
using Api.ErrorsHandlers;
using Api.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.EntitiesSpecification.Propertyspec;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class PropertyController : ApiBaseController
    {
        private readonly IGenericRepo<property> _genericRepo;

        private readonly IMapper _mapper;

        public PropertyController(
            IGenericRepo<property> genericRepo,
            IMapper mapper
        )
        {
            _mapper = mapper;
            _genericRepo = genericRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<PropertyDTo>>>
        GetProperties([FromQuery] PropertySpecParams specParams)
        {
            var spec = new PropertySpecwithFiltersAndIncludes(specParams);
            var specCount = new PropertySpecWithFilters(specParams);
            var totalCount = await _genericRepo.CountAsync(specCount);
            var properties = await _genericRepo.GetAllBySpecAsync(spec);
            var data =
                _mapper
                    .Map
                    <IReadOnlyList<property>, List<PropertyDTo>>(properties);
            return Ok(new Pagination<PropertyDTo>(specParams.PageIndex,
                specParams.PageSize,
                totalCount,
                data));
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status404NotFound)]

        public async Task<ActionResult<PropertyDTo>> GetProperty(int id)
        {
            var spec = new PropertySpecwithFiltersAndIncludes(id);
            var property = await _genericRepo.GetBySpecAsync(spec);
            if(property==null) return NotFound(new ApiErrorResponse(404));
            var data = _mapper.Map<property, PropertyDTo>(property);
            return Ok(data);
        }
    }
}
