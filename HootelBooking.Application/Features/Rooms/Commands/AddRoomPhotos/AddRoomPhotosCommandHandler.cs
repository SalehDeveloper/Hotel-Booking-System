using HootelBooking.Application.Contracts;
using HootelBooking.Application.Models;
using HootelBooking.Application.Services;
using HootelBooking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Commands.AddRoomPhotos
{
    public class AddRoomPhotosCommandHandler : IRequestHandler<AddRoomPhotosCommand, Result<IEnumerable<string>>>
    {    
          private readonly IRoomRepository _roomRepository;  
          private readonly  IRoomPhotoRepository _roomPhotoRepository;
          private readonly ImageService _imageService;

        public AddRoomPhotosCommandHandler(IRoomRepository roomRepository, IRoomPhotoRepository roomPhotoRepository, ImageService imageService)
        {
            _roomRepository = roomRepository;
            _roomPhotoRepository = roomPhotoRepository;
            _imageService = imageService;
        }

        public async Task<Result<IEnumerable<string>>> Handle(AddRoomPhotosCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the room by ID
            var room = await _roomRepository.GetByIdAsync(request.RequestDto.RoomId);

            // Check if the room exists
            if (room is null)
            {
                return new Result<IEnumerable<string>>(404, "Room Not Found");
            }

            // Initialize collections for the room photos and the photo names (results)
            var roomPhotos = new List<RoomPhoto>();
            var results = new List<string>();

            // Upload each file and create RoomPhoto entries
            foreach (var file in request.RequestDto.files)
            {
                var photoName = await _imageService.UploadImageAsync(file);
                results.Add(photoName);

                var roomPhoto = new RoomPhoto
                {
                    RoomId = request.RequestDto.RoomId,
                    PhotoName = photoName
                };
                roomPhotos.Add(roomPhoto);
            }

            // Save the room photos to the repository
            await _roomPhotoRepository.AddRangeAsync(roomPhotos);

            // Return the result
            return new Result<IEnumerable<string>>(results, 200, "Added successfully");
        }
    }
}
