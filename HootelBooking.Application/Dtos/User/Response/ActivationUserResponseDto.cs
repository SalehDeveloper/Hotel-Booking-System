﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.User.Response
{
    public class ActivationUserResponseDto
    {
        public int Id { get; set; } 

        public bool IsActive {  get; set; } 

        public DateTime Date { get; set; }    
    }
}