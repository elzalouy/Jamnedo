using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamendoCoreAPI.Models
{
    public class UserTracks
    {
        [JsonProperty("headers")]
        public headears headears { get; set; }
        [JsonProperty("results")]
        public List<JsonUserTracks> results { get; set; }
    }
    public class JsonUserTracks
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

        [JsonProperty("avatar_type")]
        public string avatar_type { get; set; }

        [JsonProperty("avatar")]
        public string avatar { get; set; }

        [JsonProperty("tracks")]
        public List<Tracks> tracks { get; set; }
    }
    public class Tracks
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("duration")]
        public string duration { get; set; }

        [JsonProperty("artist_id")]
        public string artist_id { get; set; }

        [JsonProperty("artist_name")]
        public string artist_name { get; set; }

        [JsonProperty("artist_idstr")]
        public string artist_idstr { get; set; }

        [JsonProperty("releasedate")]
        public string releasedate { get; set; }

        [JsonProperty("album_name")]
        public string album_name { get; set; }

        [JsonProperty("album_id")]
        public string album_id { get; set; }


        [JsonProperty("license_ccurl")]
        public string license_ccurl { get; set; }

        [JsonProperty("updatedate")]
        public string updatedate { get; set; }

        [JsonProperty("album_image")]
        public string album_image { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }

        [JsonProperty("audio")]
        public string audio { get; set; }

        [JsonProperty("audiodownload")]
        public string audiodownload { get; set; }

        [JsonProperty("prourl")]
        public string prourl { get; set; }

        [JsonProperty("shorturl")]
        public string shorturl { get; set; }

        [JsonProperty("shareurl")]
        public string shareurl { get; set; }

        [JsonProperty("relations")]
        public TrackRelations relations { get; set; }
    }
    public class TrackRelations
    {
        [JsonProperty("review")]
        public string review { get; set; }

        [JsonProperty("favorite")]
        public string favorite { get; set; }

        [JsonProperty("like")]
        public string like { get; set; }
    }
}
