using System.Collections.Generic;
using System.Linq;
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
        GetProperties([FromQuery]PropertySpecParams specParams)
        {
            var spec = new PropertySpecwithFiltersAndIncludes(specParams);
            var specCount = new PropertySpecWithFilters(specParams);
            var propertiesWithoutIncludes = await _genericRepo.ListAllBySpec(specCount);
            var propcounted = FilTer(propertiesWithoutIncludes,specParams);
            var properties = await _genericRepo.ListAllBySpec(spec);

            var propertiesfiltered = FilTer(properties,specParams);
            var data =
                _mapper
                    .Map
                    <List<property>, List<PropertyDTo>>(propertiesfiltered);
            return Ok(new Pagination<PropertyDTo>(specParams.PageIndex,
                specParams.PageSize,
                 propcounted.Count(),
                data));
        }

        private List<property> FilTer(IReadOnlyList<property> properties, PropertySpecParams specParams)
        {
            var filtered = properties.Where(  x=> (!specParams.NumOfBeds.HasValue || x.bed_count == specParams.NumOfBeds) &&
                 (!specParams.NumOfBedrooms.HasValue || x.bedroom_count == specParams.NumOfBedrooms) &&
                (!specParams.StateId.HasValue || x.state_id == specParams.StateId) &&
                 (!specParams.NumOfBedrooms.HasValue || x.bedroom_count == specParams.NumOfBedrooms) &&
                 (specParams.Amenities == null ||  x.property_amenities.Select(s => s.amenity).Select(s => s.name).ToList().All(specParams.Amenities.Contains) &&  x.property_amenities.Select(s => s.amenity).Select(s => s.name).ToList().Count == specParams.Amenities.Count) &&
                 (string.IsNullOrEmpty(specParams.PropertyType) || x.property_tybe.name == specParams.PropertyType)).ToList();

                 return filtered;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status404NotFound)]

        public async Task<ActionResult<PropertyDTo>> GetProperty(int id)
        {
            var spec = new PropertySpecwithFiltersAndIncludes(id);
            var property = await _genericRepo.GetBySpec(spec);
            if(property==null) return NotFound(new ApiErrorResponse(404));
            var data = _mapper.Map<property, PropertyDTo>(property);
            return Ok(data);
        }
        [HttpPost]
        public async Task<ActionResult<PropertyDTo>> PostProperty(PropertyDTo prop){
           var property = _mapper.Map<PropertyDTo,property>(prop);
        var obj =   await _genericRepo.AddAsync(property);
           return Ok(_mapper.Map<property,PropertyDTo>(obj));


        }
        

    }
}
