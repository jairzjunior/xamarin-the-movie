using Newtonsoft.Json;
using System.Collections.Generic;

namespace TheMovie.Models
{
    public class SearchMovie
    {
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "total_results")]
        public int TotalResults { get; set; }

        [JsonProperty(PropertyName = "total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<Movie> Movies { get; set; }
    }
}
