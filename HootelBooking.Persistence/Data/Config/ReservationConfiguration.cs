using HootelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HootelBooking.Persistence.Data.Config
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");
            builder.ToTable(t => t.HasCheckConstraint("CK_Reservation_NumberOfGuests", "NumberOfGuests >=1 AND NumberOfGuests<=4"));
            builder.ToTable(t => t.HasCheckConstraint("CK_Reservation_NumberOfNights", "NumberOfNights >=1 AND NumberOfNights<=30"));

            builder.HasKey(res => res.Id);

            builder.Property(res => res.Id).ValueGeneratedOnAdd();
            builder.Property(x=> x.IsActive).HasDefaultValue(true);

            builder.Property(p => p.TotalPrice).HasColumnName("TotalPrice")
            .HasPrecision(18, 2); //


            // relation with FeedBacks 
            builder.HasMany(res => res.FeedBacks)
                   .WithOne(feed => feed.Reservation)
                   .HasForeignKey(feed => feed.ReservationID).IsRequired().OnDelete(DeleteBehavior.NoAction);

            //relation for ReservationStatuses 

            builder.HasOne(res => res.ReversationStatus)
                   .WithMany(resStat => resStat.Reservations)
                   .HasForeignKey(res => res.ReservationStatusID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.NoAction);

            // relation for Rooms 
            builder.HasOne(res => res.Room)
                   .WithMany(room => room.Reservations)
                   .HasForeignKey(res => res.RoomID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.NoAction);

            // relation for payments 
            builder.HasOne(res => res.Payment)
                   .WithOne(payment => payment.Reservation)
                   .HasForeignKey<Payment>(payment => payment.ReservationID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.NoAction);

            

       

            




        }
    }
}
