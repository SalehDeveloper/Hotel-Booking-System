using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.Room.Request
{
    public class AddRoomPhotosRequestDto
    {
        public int RoomId { get; set; }

        public List<IFormFile> files { get; set; }  
    }
}
