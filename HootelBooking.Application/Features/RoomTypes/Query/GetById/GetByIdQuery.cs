using HootelBooking.Application.Dtos.RoomType.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.RoomTypes.Query.GetById
{
    public class GetByIdQuery:IRequest<Result<RoomTypeResponseDto>>
    {
        public int Id { get; set; }
    }
}
