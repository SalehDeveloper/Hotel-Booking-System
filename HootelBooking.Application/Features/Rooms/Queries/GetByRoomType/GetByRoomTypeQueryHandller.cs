using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Application.Features.Rooms.Queries.GetByRoomNumber;
using HootelBooking.Application.Models;
using MediatR;

namespace HootelBooking.Application.Features.Rooms.Queries.GetByRoomType
{
    public class GetByRoomTypeQueryHandller : IRequestHandler<GetByRoomTypeQuery, Result<IEnumerable<RoomResponseDto>>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public GetByRoomTypeQueryHandller(IRoomRepository roomTypeRepository, IMapper mapper)
        {
            _roomRepository = roomTypeRepository;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<RoomResponseDto>>> Handle(GetByRoomTypeQuery request, CancellationToken cancellationToken)
        {
            var res = await _roomRepository.GetByRoomType(request.RoomType);

            if (res.Any())
            {
                var mappedResult = _mapper.Map< IEnumerable<RoomResponseDto>>(res);

                return new Result<IEnumerable<RoomResponseDto>>(mappedResult, 200, "Retrived Successfully");

            }
            return new Result<IEnumerable<RoomResponseDto>>(404, $"Room With Type: {request.RoomType} Not Found");
        }

    }

}
