using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamendoCoreAPI.Models
{
    public class UserArtists
    {
        [JsonProperty("headers")]
        public headears headears { get; set; }
        [JsonProperty("results")]
        public List<JsonUserArtists> Artists { get; set; }
    }
    public class JsonUserArtists
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
    public class Artists
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
    public class Relation
    {
        [JsonProperty("fan")]
        public string Fan { get; set; }
    }
}
