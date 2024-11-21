using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Amenity.Response;
using HootelBooking.Application.Dtos.RoomType.Response;
using HootelBooking.Application.Dtos.State.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.RoomTypes.Command.DeleteRoomType
{
    public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand, Result<RoomTypeResponseDto>>
    {

        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IMapper _mapper;

        public DeleteRoomTypeCommandHandler(IRoomTypeRepository roomTypeRepository, IMapper mapper)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<RoomTypeResponseDto>> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {

            var roomTypeToDelete = await _roomTypeRepository.GetByIdAsync(request.Id);

            if (roomTypeToDelete != null)
            {
                var res = await _roomTypeRepository.DeleteAsync(request.Id);

                var mappedRoomType = _mapper.Map<RoomTypeResponseDto>(roomTypeToDelete);
                if (res == 0)
                    return new Result<RoomTypeResponseDto>(mappedRoomType, 409, "RoomType is Already delted ");

                if (res == request.Id)
                    return new Result<RoomTypeResponseDto>(mappedRoomType, 200, "RoomType Deleted Successfully");


            }
            return new Result<RoomTypeResponseDto>(404, $"RoomType with Id: {request.Id} Not Found");





        }
    }
}
