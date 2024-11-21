using AutoMapper;
using HootelBooking.Application.Dtos.Auth.Request;
using HootelBooking.Application.Dtos.Auth.Response;
using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Profiles.AutoMapper
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterRequestDto, ApplicationUser>()
           .ForMember(dest => dest.Country, opt => opt.Ignore())
           .ForMember(dest => dest.State, opt => opt.Ignore())
           .ForMember(dest => dest.FeedBacks, opt => opt.Ignore())
           .ForMember(dest => dest.Reservations, opt => opt.Ignore());
           

            CreateMap<ApplicationUser, AuthResponseDto>()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Name))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.Name))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<ApplicationUser, UserResponseDto>()
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.Name))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));


            CreateMap<ApplicationUser, ActivationUserResponseDto>()
               .ForMember(dest=> dest.Date , opt=> opt.MapFrom(src=> src.LastModifiedDate));
            







        }
    }
}
