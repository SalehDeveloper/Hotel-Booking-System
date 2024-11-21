using AutoMapper;
using HootelBooking.Application.Dtos.Payment.Response;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Profiles.AutoMapper
{
    public class PaymentProfile:Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentResponseDto>()
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => ((enPaymentType)src.PaymentMethodID).ToString()));
        }
    }
}
