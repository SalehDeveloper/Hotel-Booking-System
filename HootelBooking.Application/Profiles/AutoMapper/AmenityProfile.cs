using AutoMapper;
using HootelBooking.Application.Dtos.Amenity.Request;
using HootelBooking.Application.Dtos.Amenity.Response;
using HootelBooking.Application.Dtos.RoomType.Response;
using HootelBooking.Application.Features.Amenities.Commands.CreateAmenity;
using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Profiles.AutoMapper
{
    public class AmenityProfile:Profile
    {
        public AmenityProfile()
        {
            CreateMap< Amenity,AmenityResponseDto >().ReverseMap();
            CreateMap<CreateAmenityRequestDto, Amenity>().ReverseMap() ;
            CreateMap<UpdateAmenityRequestDto, Amenity>().ReverseMap();
            CreateMap<Amenity, RoomTypeAmenityResponseDto>().ReverseMap();
        }
    }
}
