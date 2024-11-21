using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HootelBooking.Persistence.Data.Config
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("PaymentMethods");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).ValueGeneratedOnAdd();


            // Make the Enum Registerd in the Database as its Value 
            builder.Property(x => x.PaymentType)
                   .HasConversion(
                   x => x.ToString(),
                   x => (enPaymentType)Enum.Parse(typeof(enPaymentType), x)
                   );


        }
    }
}
