using System.Text.Json.Serialization;

namespace MovieServiceDAL.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public string Description { get; set; }
        public decimal Duration { get; set; }
        public string Language { get; set; }
        
        [JsonIgnore]
        public ICollection<Genre> Genres { get; set; }
    }
}
