using Newtonsoft.Json;

namespace TheMovie.Models
{
    public class ProductionCountries
    {
        [JsonProperty(PropertyName = "iso_3166_1")]
        public string Iso31661 { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

}
