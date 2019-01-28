using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicApp.MusicAPIs.JSON_Classes
{
    class JsonUser
    {
        [JsonProperty("headers")]
        public headears headears { get; set; }
        [JsonProperty("results")]
        public List<User> Users { get; set; }
    }

    class headears
    {
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("code")]
        public string code { get; set; }
        [JsonProperty("error_message")]
        public string error_message { get; set; }
        [JsonProperty("warnings")]
        public string warnings { get; set; }
        [JsonProperty("results_count")]
        public string results_count { get; set; }
    }
    class User
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("dispname")]
        public string DispName { get; set; }
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("lang")]
        public string Language { get; set; }
        [JsonProperty("creationdate")]
        public string CreationDate { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
    }
}
