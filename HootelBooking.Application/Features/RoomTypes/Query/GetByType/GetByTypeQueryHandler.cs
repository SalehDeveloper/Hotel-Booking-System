using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.RoomType.Response;
using HootelBooking.Application.Models;
using MediatR;


namespace HootelBooking.Application.Features.RoomTypes.Query.GetByType
{
    public class GetByTypeQueryHandler : IRequestHandler<GetByTypeQuery, Result<RoomTypeResponseDto>>
    { 
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IMapper _mapper;

        public GetByTypeQueryHandler(IRoomTypeRepository roomTypeRepository, IMapper mapper)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<RoomTypeResponseDto>> Handle(GetByTypeQuery request, CancellationToken cancellationToken)
        {
            var res = await _roomTypeRepository.GetByType(request.roomType);

            if (res is not null)
            { 
                var mappedResult  = _mapper.Map<RoomTypeResponseDto>(res);

                return new Result<RoomTypeResponseDto>(mappedResult , 200 , "Retrived Successfully");
                
            }

            return new Result<RoomTypeResponseDto>(404, $"RoomType With Type: {request.roomType} Not Found");
        }
    }
}
