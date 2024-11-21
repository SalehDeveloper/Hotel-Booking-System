using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Room.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Application.Services;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Result<RoomResponseDto>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IRoomStatusRepository _roomStatusRepository;   
        private readonly IUserRepository _userRepository;   
        private readonly IRoomPhotoRepository _roomPhotoRepository; 
        private readonly ImageService _imageService;
        private readonly IMapper _mapper;

        public CreateRoomCommandHandler(IRoomRepository roomRepository, IRoomTypeRepository roomTypeRepository, IRoomStatusRepository roomStatusRepository, IUserRepository userRepository, IRoomPhotoRepository roomPhotoRepository, ImageService imageService, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
            _roomStatusRepository = roomStatusRepository;
            _userRepository = userRepository;
            _roomPhotoRepository = roomPhotoRepository;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async  Task<Result<RoomResponseDto>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {


            var room = await _roomRepository.GetByRoomNumber(request.Request.RoomNumber);

            if (room is not null)
            {
                return new Result<RoomResponseDto>(404, "Room Number Already Exists. Try another one.");
            }

            // Validate the room type
            var roomType = await _roomTypeRepository.GetByType(request.Request.RoomType);

            if (roomType is null)
            {
                return new Result<RoomResponseDto>(404, "Invalid Room Type");
            }

            // Get room status
            var roomStatus = await _roomStatusRepository.GetByIdAsync(1);

            // Create room for adding it to database
            var roomToAdd = new Room
            {
                RoomType = roomType,
                RoomStatus = roomStatus,
                RoomTypeID = roomType.Id,
                RoomStatusID = 1,
                CreatedBy = await _userRepository.GetCurrentUserNameAsync()
            };

            // Map request data to the new room entity
            _mapper.Map(request.Request, roomToAdd);

            // Add the room
            var addedRoom = await _roomRepository.AddAsync(roomToAdd);

            if (addedRoom is null)
            {
                throw new ErrorResponseException(500, "Failed To Add", "Internal Server Error");
            }

            // Add photos if any
            if (request.Request.Photos is not null && request.Request.Photos.Any())
            {
                var roomPhotos = new List<RoomPhoto>();
                foreach (var photo in request.Request.Photos)
                {
                    var roomPhoto = new RoomPhoto
                    {
                        RoomId = addedRoom.RoomId,
                        PhotoName = await _imageService.UploadImageAsync(photo),
                    };
                    roomPhotos.Add(roomPhoto);
                }
                await _roomPhotoRepository.AddRangeAsync(roomPhotos);
            }

            // Map the added room to the response DTO
            var mappedResult = _mapper.Map<RoomResponseDto>(addedRoom);
            return new Result<RoomResponseDto>(mappedResult, 200, "Added Successfully");

        }


        
    }
}
