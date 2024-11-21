using HootelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace HootelBooking.Persistence.Data.Config
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(pay => pay.Id);

            builder.Property(pay => pay.Id).ValueGeneratedOnAdd();


            builder
            .Property(p => p.Price).HasColumnName("Price")
            .HasPrecision(18, 2); //

            // realtion with PaymentMethod 
            builder.HasOne(pay => pay.PaymentMethod)
                   .WithMany(method => method.Payments)
                   .HasForeignKey(pay => pay.PaymentMethodID)
            .IsRequired();

      
        

        }
    }
}
