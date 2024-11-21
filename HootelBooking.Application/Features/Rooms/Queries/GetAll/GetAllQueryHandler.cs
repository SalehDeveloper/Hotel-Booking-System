using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Application.Models;
using MediatR;

namespace HootelBooking.Application.Features.Rooms.Queries.GetAll
{
    public class GetAllQueryHandler:IRequestHandler<GetAllQuery  , Result<IEnumerable<RoomResponseDto>>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoomResponseDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var res = await _roomRepository.ListAllAsync();

            if (res.Any())
            {
                var mappedResult = _mapper.Map<IEnumerable<RoomResponseDto>>(res);

                return new Result<IEnumerable<RoomResponseDto>>(mappedResult, 200, "Retrived Successfully");
            }
            return new Result<IEnumerable<RoomResponseDto>>(404, "No  Rooms Found");
        }
    }
}
