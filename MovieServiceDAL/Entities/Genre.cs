using System.Text.Json.Serialization;

namespace MovieServiceDAL.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [JsonIgnore]
        public List<Movie> Movies { get; set; }
    }
}
