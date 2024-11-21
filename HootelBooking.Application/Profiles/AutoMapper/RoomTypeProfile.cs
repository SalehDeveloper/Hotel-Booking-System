using AutoMapper;
using HootelBooking.Application.Dtos.RoomType.Request;
using HootelBooking.Application.Dtos.RoomType.Response;
using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Profiles.AutoMapper
{
    public class RoomTypeProfile : Profile
    {
         
        public RoomTypeProfile()
        {

            CreateMap<RoomType, RoomTypeResponseDto>()
                .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src => src.Amenities))
                .ReverseMap();



            CreateMap<RoomType, CreateRoomTypeRequestDto>().ReverseMap();
                
         
            
            CreateMap<UpdateRoomTypeRequestDto, RoomType>().ReverseMap();

        }
    }
}
