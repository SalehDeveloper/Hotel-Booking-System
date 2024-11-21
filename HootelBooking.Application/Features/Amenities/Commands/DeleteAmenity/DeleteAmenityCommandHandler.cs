using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Amenity.Response;
using HootelBooking.Application.Dtos.State.Response;
using HootelBooking.Application.Models;
using MediatR;


namespace HootelBooking.Application.Features.Amenities.Commands.DeleteAmenity
{
    public class DeleteAmenityCommandHandler : IRequestHandler<DeleteAmenityCommand, Result<AmenityResponseDto>>
    { 
        private readonly IAmenityRepository _amenityRepository;
        private readonly IMapper _mapper;

        public DeleteAmenityCommandHandler(IAmenityRepository amenityRepository, IMapper mapper)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
        }

        public async Task<Result<AmenityResponseDto>> Handle(DeleteAmenityCommand request, CancellationToken cancellationToken)
        {
            var AmenityToDelete = await _amenityRepository.GetByIdAsync(request.Id);

            if (AmenityToDelete != null)
            {
                var res = await _amenityRepository.DeleteAsync(request.Id);

                var mappedAmenity = _mapper.Map<AmenityResponseDto>(AmenityToDelete);
                if (res == 0)
                    return new Result<AmenityResponseDto>(mappedAmenity, 409, "Amenity is Already delted ");

                if (res == request.Id)
                    return new Result<AmenityResponseDto>(mappedAmenity, 200, "Amenity Deleted Successfully");


            }
            return new Result<AmenityResponseDto>(404, $"Amenity with Id: {request.Id} Not Found");
        }
    }
}
