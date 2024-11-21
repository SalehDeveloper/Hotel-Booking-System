using HootelBooking.Domain.Enums;

namespace HootelBooking.Domain.Entities
{
    public class RoomStatus
    {
        public int ID { get; set; }

        public enRoomStatus Status { get; set; } 

        public string? Description { get; set; }


        //Navigation Properties

        public ICollection<Room> Rooms { get; set; } = new List<Room>();


    }
}
