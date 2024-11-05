using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ReservationServiceDAL.Entities;
using ReservationServiceDAL.Seeding;

namespace ReservationServiceDAL.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                   .HasMaxLength(70)
                   .IsRequired();

            builder.Property(r => r.Date)
                   .HasColumnType("date")
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.Property(r => r.ShowtimeId)
                   .IsRequired();

            builder.HasOne(r => r.Ticket)
                   .WithMany(ticket => ticket.Reservations)
                   .HasForeignKey(r => r.TicketTypeId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("FK_Reservations_TicketType");

            builder.Property(r => r.Amount)
                   .HasColumnType("int")
                   .IsRequired();

            builder.Property(r => r.TotalCost)
                   .HasColumnType("decimal(10, 2)");

            builder.Property(r => r.isCancelable)
                   .HasColumnType("bit");

            builder.Property(r => r.TimeOfReservation)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETDATE()");

            new ReservationSeeder().Seed(builder);
        }
    }
}
