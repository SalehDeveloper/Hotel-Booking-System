using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Application.Dtos.RoomType.Response;
using HootelBooking.Application.Models;
using MediatR;

namespace HootelBooking.Application.Features.Rooms.Queries.GetByID
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<RoomResponseDto>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IRoomRepository roomTypeRepository, IMapper mapper)
        {
            _roomRepository = roomTypeRepository;
            _mapper = mapper;
        }
        public async Task<Result<RoomResponseDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await   _roomRepository.GetByIdAsync(request.Id);

            if (res is not null)
            {
                var mappedResult = _mapper.Map<RoomResponseDto>(res);

                return new Result<RoomResponseDto>(mappedResult, 200, "Retrived Successfully");

            }
            return new Result<RoomResponseDto>(404, $"Room With Id: {request.Id} Not Found");
        }
    }
}
