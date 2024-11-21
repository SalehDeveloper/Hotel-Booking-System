using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Application.Models;
using MediatR;

namespace HootelBooking.Application.Features.Rooms.Queries.GetInActive
{
    public class GetInActiveQueryHanlder : IRequestHandler<GetInActiveQuery, Result<IEnumerable<RoomResponseDto>>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public GetInActiveQueryHanlder(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoomResponseDto>>> Handle(GetInActiveQuery request, CancellationToken cancellationToken)
        {
            var res = await _roomRepository.GetInActiveAsync();

            if (res.Any())
            {
                var mappedResult = _mapper.Map<IEnumerable<RoomResponseDto>>(res);

                return new Result<IEnumerable<RoomResponseDto>>(mappedResult, 200, "Retrived Successfully");
            }
            return new Result<IEnumerable<RoomResponseDto>>(404, "No Active Rooms Found");
        }
    }

}
