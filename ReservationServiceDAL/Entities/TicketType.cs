namespace ReservationServiceDAL.Entities
{
    public class TicketType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
