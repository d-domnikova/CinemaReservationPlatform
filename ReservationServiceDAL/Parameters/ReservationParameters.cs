namespace ReservationServiceDAL.Parameters
{
    public class ReservationParameters : QueryStringParameters
    {
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public int ShowtimeId { get; set; }
        public int TicketTypeId { get; set; }
    }
}
