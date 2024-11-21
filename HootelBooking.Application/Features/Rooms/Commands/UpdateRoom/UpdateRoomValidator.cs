using FluentValidation;
using HootelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomValidator:AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomValidator()
        {
            RuleFor(x => x.Room.RoomId).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Room.IsActive).NotEmpty().NotNull();
            RuleFor(x => x.Room.RoomNumber).NotEmpty().NotNull();
            RuleFor(x=> x.Room.RoomType).NotEmpty().NotNull();
            RuleFor(x => x.Room.ViewType).Must(BeValidViewType).WithMessage("Invalid view Type");
            RuleFor(x => x.Room.BedType).Must(BeValidBedType).WithMessage("Invalid Bed Type");
            RuleFor(x => x.Room.RoomStatus).Must(BeValidRoomStatus).WithMessage("Invalid Room Status");
            RuleFor(x=> x.Room.Price).GreaterThan(0).WithMessage("Price Should Be Greater Than 0");





        }

        private bool BeValidViewType (string viewType)
        {
            return Enum.TryParse<enViewType>(viewType, true, out enViewType _);
        }

        private bool BeValidBedType (string bedType)
        {
            return Enum.TryParse<enBedType>(bedType, true, out enBedType _);
        }

        private bool BeValidRoomStatus(string roomStatus)
        {
             return Enum.TryParse<enRoomStatus>(roomStatus, true, out enRoomStatus _);
        }
    }
}
