using Api.DTOS;
using AutoMapper;
using Domain.Entities;

namespace Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<property, PropertyDTo>()
            .ForMember(d => d.CityName, o => o.MapFrom(s => s.City.name))
            .ForMember(d => d.countryName, o => o.MapFrom(s => s.country.name))
            .ForMember(d => d.currencyName, o => o.MapFrom(s => s.currency.name))
            .ForMember(d => d.propertybeName, o => o.MapFrom(s => s.property_tybe.name))
            .ForMember(d => d.stateName, o => o.MapFrom(s => s.state.name))
            .ForMember(d => d.UserEmail, o => o.MapFrom(s => s.User.Email));
            // Booking
            CreateMap<Booking, BookingDTO>();
            // state
            CreateMap<state, StateDTO>()
            .ForMember(d => d.country, o => o.MapFrom(s => s.country.name));



        }
    }
}