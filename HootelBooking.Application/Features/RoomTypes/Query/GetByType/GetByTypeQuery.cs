using HootelBooking.Application.Dtos.RoomType.Response;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.RoomTypes.Query.GetByType
{
    public class GetByTypeQuery:IRequest<Result<RoomTypeResponseDto>>
    {
        public string roomType { get; set; }
    }
}
