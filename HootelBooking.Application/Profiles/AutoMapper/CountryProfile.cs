using AutoMapper;
using HootelBooking.Application.Dtos.Country.Request;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Profiles.AutoMapper
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country , CountryResponseDto>().ReverseMap();
            CreateMap<Country, CountryPopulationResponseDto>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<UpdateCountryRequestDto, Country>().ReverseMap();
            CreateMap<CreateCountryRequestDto, Country>().ReverseMap();

        }
    }
}
