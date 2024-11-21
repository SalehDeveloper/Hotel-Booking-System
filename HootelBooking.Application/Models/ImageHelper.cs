using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Models
{
    public class ImageHelper
    {
        public string Directory { get; set; }

        public string DefaultPhoto {  get; set; } 
        public List<string> AllowedTypes { get; set; } = new List<string>();
    }
}
