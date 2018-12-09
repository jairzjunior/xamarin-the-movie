using Newtonsoft.Json;
using System.Collections.Generic;

namespace TheMovie.Models
{
    public class MovieVideo
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<Video> Results { get; set; }
    }

}
