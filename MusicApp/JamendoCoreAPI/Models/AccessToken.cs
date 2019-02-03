using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JamendoCoreAPI.Models
{
     public class AccessToken
    {

        [JsonProperty("access_token")]
        public string Access_Token { get; set; }

        [JsonProperty("expires_in")]
        public string expires_in { get; set; }

        [JsonProperty("token_type")]
        public string token_type { get; set; }

        [JsonProperty("scope")]
        public string scope { get; set; }

        [JsonProperty("refresh_token")]
        public string refresh_token { get; set; }

        public string Grant_Type;

        public string Response_Type;

        public DateTime Expiring_Date { get; set; }

        public string URL { get; set; }

        public string Authorization_Code { get; set; }

        public string Redirect_Url = "AndrewJobelApp";

        public string Client_ID { get; set; }

        public string Client_Secret { get; set; }
    }
}
