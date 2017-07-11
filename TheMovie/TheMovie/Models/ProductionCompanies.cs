using Newtonsoft.Json;

namespace TheMovie.Models
{
    public class ProductionCompanies
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
    }
}
