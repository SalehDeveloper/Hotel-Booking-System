using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.Room.Request
{
    public class UpdateRoomRequestDto
    {
        public int RoomId { get; set; }

        public string RoomNumber { get; set; }

        public string RoomType { get; set; }

        public string RoomStatus { get; set; }
        public decimal Price { get; set; }

        public string BedType { get; set; }

        public string ViewType { get; set; }

        public bool IsActive { get; set; }
       // public string ModifiedBy { get; set; }
        public DateTime ModifiedDate => DateTime.Now;
    }
    
}
