using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.FeedBack.Request
{
    public class UpdateFeedbackRequestDto
    { 
        public int FeedbackID { get; set; }

        public int Rate { get; set; }   

        public string Comment { get; set; } 


    }
}
