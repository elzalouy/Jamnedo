using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Newtonsoft.Json;
using JamendoCoreAPI.Models;
using System.IO;
using System.Linq;

namespace JamendoCoreAPI.Repository
{
    public class UserTracks_Repo
    {
        /// <summary>
        /// Get the Tracks either single or album tracks as a List of Tracks using the AccessToken in User Authentication object
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public static List<Tracks> GetRestUserTracks(User_Authentication auth)
        {
            if (auth.token.Expiring_Date < DateTime.Now)
            {
                auth.RefreshAccessToken();
            }
            var RestClient = new RestClient(auth.token.URL);
            var Request = new RestRequest("users/tracks");
            Request.AddParameter("client_id", auth.token.Client_ID);
            Request.AddParameter("access_token", auth.token.Access_Token);
            Request.AddParameter("track_type", "single albumtrack");
            var tRsponse = RestClient.Execute(Request);
            UserTracks tracks = JsonConvert.DeserializeObject<UserTracks>(tRsponse.Content);
            return tracks.results[0].tracks;
        }

        /// <summary>
        /// Get the Artists from the CSV file stored offline
        /// </summary>
        /// <returns></returns>
        public static List<Tracks> GetCsvUserTracks()
        {
            List<Tracks> CSVTracks = new List<Tracks>();
            string Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int length = Path.IndexOf("Debug\\");
            Path = Path.Substring(0, length);
            Path += "Debug\\UserTracks.csv";

            if (File.Exists(Path))
            {
                CSVTracks = File.ReadAllLines(Path).Skip(1)
                     .Select(v => CSVToTracks(v)).ToList();
                return CSVTracks;
            }
            else
            {

                return null;
            }
        }

        /// <summary>
        /// Save or Update the SCV using this Method and pass it the new data as a Parameter
        /// </summary>
        /// <param name="Rest"></param>
        public static void UpdateCSV(List<Tracks> Rest)
        {
            string Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int length = Path.IndexOf("Debug\\");
            Path = Path.Substring(0, length);
            Path += "Debug\\UserTracks.csv";
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
            string[] content = new string[Rest.Count + 1];
            content[0] = "id,album_id,album_image,artist_id,artist_name,audio,audiodownload,duration,image,license_ccurl,name,favorite,like,review";
            foreach (var item in Rest)
            {
                content[Rest.IndexOf(item) + 1] = $"{item.id},{item.album_id},{item.album_image},{item.artist_id},{item.artist_name},{item.audio},{item.audiodownload},{item.duration},{item.image},{item.license_ccurl},{item.name},{item.relations.favorite},{item.relations.like},{item.relations.review}";
            }
        }

        /// <summary>
        /// Search about tracks using the namesearch.
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<Tracks> SearchTracks(User_Authentication auth, string value)
        {
            if (auth.token.Expiring_Date < DateTime.Now)
            {
                auth.RefreshAccessToken();
            }
            var RestClient = new RestClient(auth.token.URL);
            var Request = new RestRequest("tracks");
            Request.AddParameter("client_id", auth.token.Client_ID);
            Request.AddParameter("namesearch", value);
            Request.AddParameter("limit", "200");
            var tResponse = RestClient.Execute(Request);
            SearchTracks tracks = JsonConvert.DeserializeObject<SearchTracks>(tResponse.Content);
            return tracks.Tracks;
        }

        public static bool AddTrackToFavorites(User_Authentication auth,string trackid)
        {
            try
            {
                if (auth.token.Expiring_Date < DateTime.Now)
                {
                    auth.RefreshAccessToken();
                }
                var RestClient = new RestClient(auth.token.URL);
                var Request = new RestRequest("setuser/favorite", Method.POST);
                Request.AddParameter("client_id", auth.token.Client_ID);
                Request.AddParameter("access_token", auth.token.Access_Token);
                Request.AddParameter("track_id", trackid);
                var tResponse = RestClient.Execute(Request);
                SearchTracks tracks = JsonConvert.DeserializeObject<SearchTracks>(tResponse.Content);
                    if (tracks.headears.status == "Ok")
                {
                    return true;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Convert the Csv Line to object according to the standard used to save it.
        /// </summary>
        /// <param name="csvLine"></param>
        /// <returns></returns>
        private static Tracks CSVToTracks(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Tracks track = new Tracks()
            {
                id = values[0],
                album_id = values[1],
                album_image = values[2],
                artist_id = values[3],
                artist_name = values[4],
                audio = values[5],
                audiodownload = values[6],
                duration = values[7],
                image = values[8],
                license_ccurl = values[9],
                name = values[10],
                relations = new TrackRelations()
                {
                    favorite = values[11],
                    like = values[12],
                    review = values[13]
                }
            };
            return track;
        }
    }
}
