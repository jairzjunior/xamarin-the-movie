using Newtonsoft.Json;
using System.Collections.Generic;

namespace TheMovie.Models
{
    public class MovieImage
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "backdrops")]
        public List<Image> Backdrops { get; set; }

        [JsonProperty(PropertyName = "posters")]
        public List<Image> Posters { get; set; }
    }

}
