using FluentValidation;
using HootelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Commands.CreateRoom
{
    public class CreateRoomValidator:AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomValidator()
        {
            RuleFor(x => x.Request.RoomNumber).NotEmpty().WithMessage("Room number is required.");
            RuleFor(x => x.Request.ViewType).Must(BeValidViewType).WithMessage("Invalid view type.");
            RuleFor(x => x.Request.BedType).Must(BeValidBedType).WithMessage("Invalid bed type.");

        }

        private bool BeValidViewType(string viewType)
        {
            return Enum.TryParse<enViewType>(viewType, true, out var _);
        }

        private bool BeValidBedType(string bedType)
        {
            return Enum.TryParse<enBedType>(bedType, true, out var _);
        }
    }
}
