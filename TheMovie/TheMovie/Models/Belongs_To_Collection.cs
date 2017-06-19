using Newtonsoft.Json;

namespace TheMovie.Models
{
    public class Belongs_To_Collection
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty(PropertyName = "backdrop_path")]
        public string BackdropPath { get; set; }
    }

}
