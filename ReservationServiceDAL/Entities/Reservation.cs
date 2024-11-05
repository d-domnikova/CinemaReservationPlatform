namespace ReservationServiceDAL.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Date {  get; set; }
        public int ShowtimeId { get; set; }

        public int TicketTypeId { get; set; }
        public TicketType Ticket { get; set; }

        public int Amount { get; set; }
        public decimal TotalCost { get; set; }
        public bool isCancelable { get; set; }
        public DateTime TimeOfReservation { get; set; } = DateTime.UtcNow;
    }
}
