
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;

namespace HootelBooking.Application.Dtos.RoomType.Request
{
    public class UpdateRoomTypeRequestDto
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

       // public string ModifiedBy { get; set; }

        public DateTime ModifiedDate => DateTime.Now;
        public List<int>? AmenitiesIds { get; set; } = new List<int>();


    }
}
