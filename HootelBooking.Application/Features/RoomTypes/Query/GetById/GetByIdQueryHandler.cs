using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.RoomType.Response;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Enums;
using MediatR;

namespace HootelBooking.Application.Features.RoomTypes.Query.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<RoomTypeResponseDto>>
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IRoomTypeRepository roomTypeRepository, IMapper mapper)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
        }
        public async Task<Result<RoomTypeResponseDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
           var res = await _roomTypeRepository.GetByIdAsync(request.Id);

            if (res is not  null)
            {
                var mappedResult  = _mapper.Map<RoomTypeResponseDto>(res);
               
                return new Result<RoomTypeResponseDto>(mappedResult, 200, "Retrived Successfully");
                
            }
            return new Result<RoomTypeResponseDto>(404, $"RoomType With Id: {request.Id} Not Found");
        }
    }
}
