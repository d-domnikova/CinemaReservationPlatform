using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationServiceDAL.Entities;
using ReservationServiceDAL.Interfaces;

namespace ReservationServiceDAL.Seeding
{
    public class ReservationSeeder : ISeeder<Reservation>
    {
        private static readonly List<Reservation> reservations = new()
        {
            new Reservation
            {
                Id = 1,
                Name = "Jane Smith",
                Date = new DateOnly(2024, 11, 7),
                ShowtimeId = 2,
                TicketTypeId = 1,
                Amount = 3,
                isCancelable = true,
            },
               new Reservation
            {
                Id = 2,
                Name = "Jane Smith",
                Date = new DateOnly(2024, 11, 7),
                ShowtimeId = 2,
                TicketTypeId = 2,
                Amount = 1,
                isCancelable = true,

            },
                new Reservation
            {
                Id = 3,
                Name = "Mei Chao",
                Date = new DateOnly(2024, 11, 12),
                ShowtimeId = 4,
                TicketTypeId = 1,
                Amount = 2,
                isCancelable = true,

            },
                 new Reservation
            {
                Id = 4,
                Name = "Richard Cameron",
                Date = new DateOnly(2024, 11, 5),
                ShowtimeId = 2,
                TicketTypeId = 3,
                Amount = 1,
                isCancelable = false,
            }
        };

        public void Seed(EntityTypeBuilder<Reservation> builder) => builder.HasData(reservations);
    }
}
