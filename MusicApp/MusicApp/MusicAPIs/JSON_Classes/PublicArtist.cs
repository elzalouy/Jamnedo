using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.MusicAPIs.JSON_Classes
{

    class PublicArtist
    {
        [JsonProperty("headers")]
        public headears headears { get; set; }
        [JsonProperty("results")]
        public List<JsonPublicArtist> results { get; set; }
    }
    public class JsonPublicArtist
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("website")]
        public string website { get; set; }
        [JsonProperty("joindate")]
        public string joindate { get; set; }
        [JsonProperty("image")]
        public string image { get; set; }
        [JsonProperty("shorturl")]
        public string shorturl { get; set; }
        [JsonProperty("shareurl")]
        public string shareurl { get; set; }
    }
}
