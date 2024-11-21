using AutoMapper;
using HootelBooking.Application.Dtos.Country.Response;
using HootelBooking.Application.Dtos.State.Request;
using HootelBooking.Application.Dtos.State.Response;
using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Profiles.AutoMapper
{
    public class StateProfile:Profile
    {
        public StateProfile()
        {

           
            CreateMap<State, StateResponseDto>().ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Name)).ReverseMap();
            CreateMap<State, StateCountryResponseDto>();
            CreateMap<UpdateStateRequestDto, State>().ReverseMap();
            CreateMap<AddStateRequestDto, State>().ReverseMap();



        }
    }
}
