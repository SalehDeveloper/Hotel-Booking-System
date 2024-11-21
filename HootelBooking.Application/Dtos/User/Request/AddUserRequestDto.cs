using HootelBooking.Application.Dtos.Auth.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.User.Request
{
    public class AddUserRequestDto:RegisterRequestDto
    {
        public string Role {  get; set; }   

    }
}
