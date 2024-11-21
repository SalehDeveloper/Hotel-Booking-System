using HootelBooking.Domain.Entities;
using HootelBooking.Persistence.Data.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace HootelBooking.Persistence.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {

        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }


        public DbSet<Room> Rooms { get; set; }


        public DbSet<Amenity> Amenities { get; set; }

        public DbSet<FeedBack> FeedBacks { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<ReservationStatus> ReservationStatuses { get; set; }

        public DbSet<RoomTypeAmenity> RoomTypeAmenities { get; set; }

        public DbSet<RoomStatus> RoomStatuses { get; set; }

        public DbSet<RoomType> RoomTypes { get; set; }

        public DbSet<RoomPhoto> RoomPhotos { get; set; }





        public AppDbContext(DbContextOptions<AppDbContext> options )
      : base(options)
        {
          
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Configure Identity tables
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationRole>().ToTable("Roles");

            // Configure custom fields in Identity tables
            builder.Entity<ApplicationRole>().Property(role => role.Descritption).HasMaxLength(256);

            // Identity User Claims
            builder.Entity<IdentityUserClaim<int>>(e => { e.ToTable("UserClaims"); });

            // Identity User Logins
            builder.Entity<IdentityUserLogin<int>>(e =>
            {
                e.ToTable("UserLogins");
                e.HasKey(login => new { login.UserId, login.LoginProvider, login.ProviderKey });
            });

            // Identity User Tokens
            builder.Entity<IdentityUserToken<int>>(e =>
            {
                e.ToTable("UserTokens");
                e.HasKey(token => new { token.UserId, token.LoginProvider, token.Name });
            });

            // Identity Role Claims
            builder.Entity<IdentityRoleClaim<int>>(e => { e.ToTable("RoleClaims"); });

            // Identity User Roles
            builder.Entity<IdentityUserRole<int>>(e =>
            {
                e.ToTable("UserRoles");
                e.HasKey(ur => new { ur.UserId, ur.RoleId });
            });

           // Apply configurations from assembly
            builder.ApplyConfigurationsFromAssembly(typeof(CountryConfiguration).Assembly);

            builder.ApplyConfiguration(new AmenityConfiguration());



        }

   




    }
}
