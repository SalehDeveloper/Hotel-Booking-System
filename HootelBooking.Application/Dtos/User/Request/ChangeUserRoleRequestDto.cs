using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.User.Request
{
    public class ChangeUserRoleRequestDto
    {
        public int Id { get; set; } 

        public string Role {  get; set; }   
    }
}
