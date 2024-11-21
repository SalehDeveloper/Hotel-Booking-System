using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Models
{
    public class Image
    {
        public FileStream File { get; set; }

        public string MimeType { get; set; }    
    }
}
