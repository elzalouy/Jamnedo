using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicApp.MusicAPIs.JSON_Classes
{
    class UserArtists
    {
        [JsonProperty("headers")]
        public headears headears { get; set; }
        [JsonProperty("results")]
        public List<JsonUserArtists> Artists { get; set; }
    }
     class JsonUserArtists
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("dispname")]
        public string dispname { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("lang")]
        public string lang { get; set; }
        [JsonProperty("creationdate")]
        public string creationdate { get; set; }
        [JsonProperty("image")]
        public string image { get; set; }
        [JsonProperty("artists")]
        public List<Artists> Artists { get; set; }
    }
    class Artists
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("image")]
        public string image { get; set; }
        [JsonProperty("joindate")]
        public string JoinDate { get; set; }
        [JsonProperty("updatedate")]
        public string UpdateDate { get; set; }
        [JsonProperty("relations")]
        public Relation relations { get; set; }
    }
    class Relation
    {
        [JsonProperty("fan")]
        public string Fan { get; set; }
    }
}
