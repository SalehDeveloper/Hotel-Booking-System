using HootelBooking.Application.Dtos.Amenity.Response;
using HootelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.RoomType.Response
{
    public class RoomTypeResponseDto
    {

        public int Id { get; set; }

        public string Type { get; set; }  
        public string? Description { get; set; }
        public bool IsActive { get; set; }  
      
        public ICollection<RoomTypeAmenityResponseDto> Amenities { get; set; } = new List<RoomTypeAmenityResponseDto>();
    }
}
