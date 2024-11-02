namespace MovieServiceDAL.Entities
{
    public class Showtime
    {
        public int Id { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int MovieId {  get; set; }
    }
}
