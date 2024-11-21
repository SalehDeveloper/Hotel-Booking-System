using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.Payment.Request
{
    public class ProccessPaymentRequestDto
    {
        public int reservationId { get; set; }

        public string Method {  get; set; } 
    }
}
