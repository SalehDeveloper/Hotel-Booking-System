using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.Room.Request
{
    public class RoomSearchRequestDto
    {
        public decimal? Price {  get; set; } 

        public string? RoomType { get; set; }

        public string? ViewType { get; set; }

        public string? BedType { get; set; }

    }
}
