using AutoMapper;
using HootelBooking.Application.Dtos.Reservation.Response;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Profiles.AutoMapper
{
    public class ReservationProfile:Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationResponseDto>()
            .ForMember(dest => dest.BookDate, opt => opt.MapFrom(src => src.BookDate.ToString("yyyy-MM-dd HH:mm")))
            .ForMember(dest => dest.CheckInDate, opt => opt.MapFrom(src => src.CheckInDate.ToString("yyyy-MM-dd HH:mm")))
            .ForMember(dest => dest.CheckOutDate, opt => opt.MapFrom(src => src.CheckOutDate.ToString("yyyy-MM-dd HH:mm")))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ((enReservationStatus)src.ReservationStatusID).ToString()))
            .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Room.RoomNumber));
     
        }
    }
}
