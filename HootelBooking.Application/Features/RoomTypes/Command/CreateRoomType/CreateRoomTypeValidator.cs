using FluentValidation;
using HootelBooking.Application.Dtos.RoomType.Request;
using HootelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.RoomTypes.Command.CreateRoomType
{
    public class CreateRoomTypeValidator:AbstractValidator<CreateRoomTypeCommand>
    {
        public CreateRoomTypeValidator()
        {
            RuleFor(x=>x.RoomType.Type).NotEmpty().NotNull();
            RuleFor(x => x.RoomType.Description).MaximumLength(256);
            RuleFor(x => x.RoomType.AmenitiesIds).Must(list => list.All(Id => Id > 0)).WithMessage("All Amenity IDs must be greater than 0 if provided.");


        }
    }
}
