using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.Reservation.Response
{
    public class ReservationResponseDto
    {
        public int Id { get; set; }

        public string RoomNumber { get; set; }

        public string BookDate { get; set; }

        public string CheckInDate { get; set; }

        public string CheckOutDate { get; set; }

        public int NumberOfNights { get; set; }

        public string Status {  get; set; } 
        public decimal TotalPrice { get; set; }




    }
}
