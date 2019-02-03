using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamendoCoreAPI.Models
{
    class SearchTracks
    {
        [JsonProperty("headers")]
        public headears headears { get; set; }

        [JsonProperty("results")]
        public List<Tracks> Tracks { get; set; }
    }
    
}
