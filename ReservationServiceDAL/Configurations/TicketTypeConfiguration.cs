using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationServiceDAL.Entities;
using ReservationServiceDAL.Seeding;

namespace ReservationServiceDAL.Configurations
{
    public class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
    {
        public void Configure(EntityTypeBuilder<TicketType> builder)
        {
            builder.HasKey(ticket => ticket.Id);

            builder.Property(ticket => ticket.Type)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(ticket => ticket.Price)
                   .HasColumnType("decimal(10, 2)")
                   .IsRequired();

            new TicketTypeSeeder().Seed(builder);
        }
    }
}
