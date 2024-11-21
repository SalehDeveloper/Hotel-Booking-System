﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Domain.Entities
{
    public class RoomPhoto
    {
        public int PhotoId { get; set; }             // Primary key for the photo
        public int RoomId { get; set; }               // Foreign key to the Room
        public string PhotoName { get; set; }         // File path or URL to the photo
       

        // Navigation property
        public Room Room { get; set; }
    }
}
