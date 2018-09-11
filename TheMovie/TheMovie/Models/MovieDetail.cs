using Newtonsoft.Json;

namespace TheMovie.Models
{
    public class MovieDetail : Movie
    {                
        [JsonProperty(PropertyName = "belongs_to_collection")]
        public BelongsToCollection BelongsToCollection { get; set; }

        [JsonProperty(PropertyName = "budget")]
        public int? Budget { get; set; }        

        [JsonProperty(PropertyName = "homepage")]
        public string Homepage { get; set; }        

        [JsonProperty(PropertyName = "imdb_id")]
        public string ImdbId { get; set; }

        [JsonProperty(PropertyName = "production_companies")]
        public ProductionCompanies[] ProductionCompanies { get; set; }

        [JsonProperty(PropertyName = "production_countries")]
        public ProductionCountries[] ProductionCountries { get; set; }        

        [JsonProperty(PropertyName = "revenue")]
        public int? Revenue { get; set; }

        [JsonProperty(PropertyName = "runtime")]
        public int? Runtime { get; set; }

        [JsonProperty(PropertyName = "spoken_languages")]
        public SpokenLanguages[] SpokenLanguages { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "tagline")]
        public string Tagline { get; set; }        
    }
}
