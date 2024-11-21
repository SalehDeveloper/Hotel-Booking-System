using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.FeedBack.Response
{
    public class FeedBackResponseDto
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; }
        public int ReservationID { get; set; }

        public int Rate { get; set; }  // should be between 1 -  5    

        public string Comment { get; set; }

        public string CreatedAt { get; set; } 
    }
}
