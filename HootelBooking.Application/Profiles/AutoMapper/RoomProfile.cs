using AutoMapper;
using HootelBooking.Application.Dtos.Room.Request;
using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Profiles.AutoMapper
{
    public class RoomProfile:Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.RoomStatus.Status))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.RoomType.Type))
                .ForMember(dest => dest.BedType, opt => opt.MapFrom(src => src.BedType))
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => src.ViewType))
                .ForMember(dest=> dest.Photos , opt=> opt.MapFrom(src=> src.RoomPhotos.Select(x=> x.PhotoName).ToList()))
                .ReverseMap();

            CreateMap<CreateRoomRequestDto, Room>()
         .ForMember(dest => dest.RoomType, opt => opt.Ignore())
         .ForMember(dest => dest.RoomStatus, opt => opt.Ignore()) // Ignore RoomType navigation property
         .ForMember(dest => dest.RoomTypeID, opt => opt.Ignore()) // Ignore RoomTypeID because it's set manually
         .ForMember(dest => dest.RoomStatusID, opt => opt.Ignore())
         .ForMember(dest => dest.RoomPhotos, opt => opt.Ignore()) // Ignore RoomStatusID because it's set manually
         .ForMember(dest => dest.BedType, opt => opt.MapFrom(src => Enum.Parse<enBedType>(src.BedType, true))) // Parse BedType from string, ignore case
         .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => Enum.Parse<enViewType>(src.ViewType, true))) // Parse ViewType from string, ignore case
         .ReverseMap();

            CreateMap<UpdateRoomRequestDto, Room>()
            .ForMember(dest => dest.RoomType, opt => opt.Ignore())
            .ForMember(dest=> dest.RoomStatus, opt=> opt.Ignore() )// Ignore RoomType (navigation property)
            .ForMember(dest => dest.BedType, opt => opt.MapFrom(src => Enum.Parse<enBedType>(src.BedType , true) ))     // Map BedType enum
            .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => Enum.Parse<enViewType>(src.ViewType , true)))   // Map ViewType enum
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))                                 // Map Price
            .ReverseMap();

        }
    }
}
