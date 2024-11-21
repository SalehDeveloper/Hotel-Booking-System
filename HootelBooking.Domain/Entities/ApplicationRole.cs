using Microsoft.AspNetCore.Identity;

namespace HootelBooking.Domain.Entities
{
    public class ApplicationRole : IdentityRole<int>
    {
        public string Descritption { get; set; }

        public bool IsActive { get; set; } = true;


    }
}
