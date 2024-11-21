using HootelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.RoomType.Request
{
    public class CreateRoomTypeRequestDto
    {
        public string Type { get; set; }

        public string? Description { get; set; }

        //public string CreatedBy { get; set; }
        public List<int>? AmenitiesIds { get; set; } = new List<int>();
    }

}
