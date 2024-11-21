using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Queries.Search
{
    public class RoomSearchQueryHandler : IRequestHandler<RoomSearchQuery, Result<IEnumerable<RoomResponseDto>>>
    {

        private readonly IRoomRepository _roomRepository;   
        private readonly IMapper _mapper;

        public RoomSearchQueryHandler(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoomResponseDto>>> Handle(RoomSearchQuery request, CancellationToken cancellationToken)
        {
            var res = await _roomRepository.Search(request.price, request.roomType, request.viewType, request.bedType);

            if (res.Any())
            {
                
                var mappedResult  = _mapper.Map<IEnumerable<RoomResponseDto>>(res);

                return new Result<IEnumerable<RoomResponseDto>>(mappedResult, 200, "Retrived Successfully");

             }
            return new Result<IEnumerable<RoomResponseDto>>(404, "No Rooms Found");
              
        }
    }
}
