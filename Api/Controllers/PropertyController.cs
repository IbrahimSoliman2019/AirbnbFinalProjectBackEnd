using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ApplictionExtentions;
using Api.DTOS;
using Api.ErrorsHandlers;
using Api.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.EntitiesSpecification.Propertyspec;
using Domain.IdentityEntities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    public class PropertyController : ApiBaseController
    {
        private readonly IGenericRepo<property> _genericRepo;

        private readonly IMapper _mapper;
        private readonly ApplicationContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public PropertyController(
            IGenericRepo<property> genericRepo,
            IMapper mapper,
            ApplicationContext context,
            UserManager<ApplicationUser> userManager
        )
        {
            _mapper = mapper;
            this.context = context;
            this.userManager = userManager;
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
        public async Task<ActionResult<PropertyDTo>> PostProperty(PropertyDToPosting prop){

           
            var property = _mapper.Map<PropertyDTo, property>(prop.Propertydto);
            foreach (var item in prop.PropertyImages)
            {
                var image = _mapper.Map<PropertyImagesDto, property_images>(item);
                property.property_images.Add(image);
            }
            foreach (var item in prop.Amenities)
            {
                var amenity = _mapper.Map<AmenityDto, amenity>(item);
                var propamenity = new property_amenities
                {
                    amenity_id = amenity.Id,

                };
                property.property_amenities.Add(propamenity);
            }
            var country = context.Countries.Where(x => x.name == prop.state.countryName).FirstOrDefault();
            if (country == null)
            {
                context.Countries.Add(new country { name=prop.state.countryName,code =prop.state.countryName});
                context.SaveChanges();
                country = context.Countries.Where(x => x.name == prop.state.countryName).FirstOrDefault();
                property.country = country;
            }
            else
            {
                property.country = country;
            }
            var state = context.States.Where(x => x.name == prop.state.name).FirstOrDefault();
            if (state == null)
            {
                context.States.Add(new state { country=country,name=prop.state.name});
                context.SaveChanges();
                state = context.States.Where(x => x.name == prop.state.name).FirstOrDefault();
                property.state = state;
            }
            else
            {
                property.state = state;
            }
            if (!country.states.Contains(state))
            {
                country.states.Add(state);
                context.SaveChanges();
            }

            property.User = await userManager.FindByEmailFromClaimsPrinciples(HttpContext.User) ?? null;

            var obj = await _genericRepo.AddAsync(property);

            return Ok(_mapper.Map<property, PropertyDTo>(obj));



        }


    }
}
