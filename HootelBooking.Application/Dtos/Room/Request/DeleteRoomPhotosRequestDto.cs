using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.Room.Request
{
    public class DeleteRoomPhotosRequestDto
    {
        public int RoomId { get; set; } 

        public List<string> PhotoNames { get; set; }
    }
}
