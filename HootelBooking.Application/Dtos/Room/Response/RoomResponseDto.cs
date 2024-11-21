using HootelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.Room.Response
{
    public class RoomResponseDto
    {
        public int RoomId { get; set; }

        public string RoomNumber { get; set; }

        public string Type {  get; set; }   
        public string BedType { get; set; }

        public string ViewType { get; set; }

        public decimal Price { get; set; }

        public string Status { get; set; }

        public bool IsActive { get; set; }  

        public List<string>? Photos { get; set; } = new List<string>();


    }
}
