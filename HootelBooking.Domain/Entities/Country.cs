namespace HootelBooking.Domain.Entities
{
    public class Country
    {


        public int Id { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

        public ICollection<State> States { get; set; } = new List<State>();



    }
}
