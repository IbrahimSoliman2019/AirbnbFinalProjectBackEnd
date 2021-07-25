using Api.DTOS;
using AutoMapper;
using Domain.Entities;
using Domain.IdentityEntities;

namespace Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            MapProperty();

            // Booking
            CreateMap<Booking, BookingDTO>()
            .ForMember(d=>d.transaction,o=>o.MapFrom(s=>s.transaction))
            .ReverseMap();
            //User
            CreateMap<ApplicationUser,UserDto>().ReverseMap();
            //Transaction
            CreateMap<transaction,TransactionDto>().ReverseMap();

            // state
            CreateMap<state, StateDTO>()
                .ForMember(d => d.country, o => o.MapFrom(s => s.country.name));


        }

        public void MapProperty()
        {
            
            
            CreateMap<property, PropertyDTo>()
                .ForMember(d => d.CityName, o => o.MapFrom(s => s.City.name))
                .ForMember(d => d.countryName,
                o => o.MapFrom(s => s.country.name))
                .ForMember(d => d.currencyName,
                o => o.MapFrom(s => s.currency.name))
                .ForMember(d => d.propertybeName,
                o => o.MapFrom(s => s.property_tybe.name))
                .ForMember(d => d.stateName, o => o.MapFrom(s => s.state.name))
                .ForMember(d => d.User,
                o => o.MapFrom(s => s.User))
                .ForMember(d=>d.property_amenities,o=>o.MapFrom(s=>s.property_amenities))
                .ReverseMap();
               // ;
        }
        public void MapPropertyDto(){
             /* CreateMap<p, property>()
                .ForMember(d => d.CityName, o => o.MapFrom(s => s.City.name))
                .ForMember(d => d.countryName,
                o => o.MapFrom(s => s.country.name))
                .ForMember(d => d.currencyName,
                o => o.MapFrom(s => s.currency.name))
                .ForMember(d => d.propertybeName,
                o => o.MapFrom(s => s.property_tybe.name))
                .ForMember(d => d.stateName, o => o.MapFrom(s => s.state.name))
                .ForMember(d => d.User,
                o => o.MapFrom(s => s.User))
                .ForMember(d=>d.property_amenities,o=>o.MapFrom(s=>s.property_amenities))
                .ReverseMap(); */
        } 
    }
}
