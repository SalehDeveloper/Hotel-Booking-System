using HootelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HootelBooking.Persistence.Data.Config
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");



            builder.HasOne(user => user.Country)
                   .WithMany(country => country.Users)
                   .HasForeignKey(user => user.CountryID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.NoAction);


            // relation for state 

            builder.HasOne(user => user.State)
                   .WithMany(state => state.Users)
                   .HasForeignKey(user => user.StateID)
                   .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction); ;

            // relation for Reservation 
            builder.HasMany(user => user.Reservations)
                    .WithOne(reservation => reservation.User)
                    .HasForeignKey(reservation => reservation.UserID)
                    .IsRequired();


            // relation for feedbacks 
            builder.HasMany(user => user.FeedBacks)
                   .WithOne(feedback => feedback.User)
                   .HasForeignKey(feedback => feedback.UserID)
                   .IsRequired();

        }
    }
}
