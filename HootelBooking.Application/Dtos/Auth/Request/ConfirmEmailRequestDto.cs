﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.Auth.Request
{
    public class ConfirmEmailRequestDto
    {
        public string Email { get; set; }   

        public string Code {  get; set; }   
    }
}
