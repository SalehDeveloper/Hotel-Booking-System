
using HootelBooking.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.Room.Request
{
    public class CreateRoomRequestDto
    { 
        public string RoomNumber { get; set; }  

        public decimal Price { get; set; }  
        public string RoomType { get; set; }
        public string ViewType {  get; set; }    
        public string BedType { get; set; }
        public bool IsActive { get; set; } = true;
      
        public DateTime CreatedAt = DateTime.Now;

        public List<IFormFile>? Photos { get; set; }



    }
}
