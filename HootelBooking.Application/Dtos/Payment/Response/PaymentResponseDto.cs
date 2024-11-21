using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.Payment.Response
{
    public class PaymentResponseDto
    {
        public int Id { get; set; }
        public int ReservationID { get; set; }
        public string PaymentMethod { get; set; }   
        public decimal Price { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
