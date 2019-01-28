using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicApp.MusicAPIs.JSON_Classes
{

    /// <summary>
    /// Success sample: {
    ///                         "access_token":"YOUR_NEW_ACCESS_TOKEN",
    ///                         "expires_in":7200,
    ///                         "token_type":"bearer",
    ///                         "scope":"music",
    ///                         "refresh_token":"YOUR_NEW_REFRESH_TOKEN"
    ///                 }
    /// </summary>
    class AccessToken
    {
        [JsonProperty("access_token")]
        internal string Access_Token { get; set; }

        [JsonProperty("expires_in")]
        internal string expires_in { get; set; }

        [JsonProperty("token_type")]
        internal string token_type { get; set; }

        [JsonProperty("scope")]
        internal string scope { get; set; }

        [JsonProperty("refresh_token")]
        internal string refresh_token { get; set; }

        internal string Grant_Type;

        internal string Response_Type;

        internal DateTime Expiring_Date { get; set; }

        internal string URL { get; set; }

        internal string Authorization_Code { get; set; }

        internal string Redirect_Url = "AndrewJobelApp";

        internal string Client_ID { get; set; }

        internal string Client_Secret { get; set; }
    }
}
