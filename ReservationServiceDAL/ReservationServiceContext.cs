using Microsoft.EntityFrameworkCore;
using ReservationServiceDAL.Configurations;
using ReservationServiceDAL.Entities;

namespace ReservationServiceDAL
{
    public class ReservationServiceContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new TicketTypeConfiguration());
        }

        public ReservationServiceContext(DbContextOptions<ReservationServiceContext> options)
            : base(options)
        {
        }
    }
}
