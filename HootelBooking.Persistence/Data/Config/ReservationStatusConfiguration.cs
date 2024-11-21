using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HootelBooking.Persistence.Data.Config
{
    public class ReservationStatusConfiguration : IEntityTypeConfiguration<ReservationStatus>
    {
        public void Configure(EntityTypeBuilder<ReservationStatus> builder)
        {
            builder.ToTable("ReservationStatuses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Staus)
                  .HasConversion(
                  x => x.ToString(),
                  x => (enReservationStatus)Enum.Parse(typeof(enReservationStatus), x)
                  );
        }
    }
}
