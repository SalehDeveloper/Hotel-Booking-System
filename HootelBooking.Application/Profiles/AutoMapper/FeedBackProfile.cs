using AutoMapper;
using HootelBooking.Application.Dtos.FeedBack.Response;
using HootelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Profiles.AutoMapper
{
    public class FeedBackProfile:Profile 
    {
        public FeedBackProfile()
        {
            CreateMap<FeedBack, FeedBackResponseDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(x => x.CreatedAt.ToString("yyyy-MM-dd HH:mm")));
                
        }
    }
}
