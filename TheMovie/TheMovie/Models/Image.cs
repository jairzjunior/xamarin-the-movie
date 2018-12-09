using Newtonsoft.Json;

namespace TheMovie.Models
{
    public class Image
    {
        [JsonProperty(PropertyName = "aspect_ratio")]
        public float AspectRatio { get; set; }

        [JsonProperty(PropertyName = "file_path")]
        public string FilePath { get; set; }

        [JsonProperty(PropertyName = "height")]
        public int Height { get; set; }        

        [JsonProperty(PropertyName = "vote_average")]
        public float VoteAverage { get; set; }

        [JsonProperty(PropertyName = "vote_count")]
        public int VoteCount { get; set; }

        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }
    }

}
