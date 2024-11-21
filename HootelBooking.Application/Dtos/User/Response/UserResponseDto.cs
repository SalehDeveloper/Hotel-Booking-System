using HootelBooking.Application.Dtos.Auth.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.User.Response
{
    public class UserResponseDto:AuthResponseDto
    {
        public string Role {  get; set; }
    }
}
