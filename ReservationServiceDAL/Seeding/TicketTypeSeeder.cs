using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationServiceDAL.Entities;
using ReservationServiceDAL.Interfaces;

namespace ReservationServiceDAL.Seeding
{
    public class TicketTypeSeeder : ISeeder<TicketType>
    {
        private static readonly List<TicketType> ticketTypes = new()
        {
            new TicketType
            {
                Id = 1,
                Type = "For adults",
                Price = 320
            },
              new TicketType
            {
                Id = 2,
                Type = "For children",
                Price = 175
            },
                new TicketType
            {
                Id = 3,
                Type = "For students",
                Price = 250
            },
        };

        public void Seed(EntityTypeBuilder<TicketType> builder) => builder.HasData(ticketTypes);
    }
}
