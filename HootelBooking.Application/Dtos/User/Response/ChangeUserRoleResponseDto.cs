using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Dtos.User.Response
{
    public class ChangeUserRoleResponseDto
    {
        public int Id { get; set; }
        
        public string OldRole { get; set; } 

        public string NewRole { get; set; } 
    }
}
