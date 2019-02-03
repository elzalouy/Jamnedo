using JamendoCoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace JamendoCoreAPI.Repository
{
    class UserArtistsRepo
    {
        /// <summary>
        /// This is a way to access the User Fans or liked Artists using GetCsvArtist
        /// <para>Return a list of artists from csv file if existed and from Http method if csv file not existed</para>
        /// </summary>
        /// <param name="authuser"></param>
        /// <returns>List of Artists</Artist></returns>
        public static List<Artists> GetCsvArtist(User_Authentication authUser)
        {
            List<Artists> Artists = new List<Artists>();
            string Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int length = Path.IndexOf("Debug\\");
            Path = Path.Substring(0, length);
            Path += "Debug\\UserArtists.csv";
            if (File.Exists(Path))
            {
                Artists = File.ReadAllLines(Path).
                     Skip(1).
                     Select(v => CsvToArtist(v)).ToList();
                return Artists;
            }
            else
            {
                Artists = GetRestArtists(authUser);
                UpdateCsvArtists(Artists);
                return Artists;
            }
        }


        /// <summary>
        /// it is prefered to use GetCsvArtist() insted of this method to check first if there are a csv file of your artist
        /// </summary>
        /// <param name="authUser"></param>
        /// <returns>List of Artists</returns>
        public static List<Artists> GetRestArtists(User_Authentication authUser)
        {
            try
            {
                var RestClient = new RestClient(authUser.token.URL);
                var Request = new RestRequest("users/artists");
                Request.AddParameter("client_id", authUser.token.Client_ID);
                Request.AddParameter("access_token", authUser.token.Access_Token);
                var tRsponse = RestClient.Execute(Request);
                UserArtists artists = JsonConvert.DeserializeObject<UserArtists>(tRsponse.Content);
                return artists.Artists[0].Artists;
            }
            catch
            {
                return null;
            }
        }



        /// <summary>
        /// Check if the artists saved in the csv file are identical to Http data or not.
        /// </summary>
        /// <param name="Csvlist"></param>
        /// <param name="userauth"></param>
        /// <returns>List Of Artists</returns>
        public static List<Artists> CheckSimilarty(List<Artists> Csvlist, User_Authentication userauth)
        {
            List<Artists> Rest = GetRestArtists(userauth);
            if (!Rest.Equals(Csvlist))
            {
                UpdateCsvArtists(Rest);
            }
            return Rest;
        }

        /// <summary>
        /// Set to your fans or Remove from your fans.
        /// </summary>
        /// <param name="ArtistId"></param>
        /// <param name="Artists"></param>
        /// <returns></returns>
        public static bool SetUserFan(string ArtistId, User_Authentication authUser)
        {
            try
            {
                //Set the Artist to Fans
                var RestClient = new RestClient(authUser.token.URL);
                var Request = new RestRequest("users/artists?relation='fan'");
                Request.AddParameter("client_id", authUser.token.Client_ID);
                Request.AddParameter("access_token", authUser.token.Access_Token);
                Request.AddParameter("artist_id", ArtistId);
                var tRsponse = RestClient.Execute(Request);
            }
            catch
            {
                return false;
            }

            //set to the Csv File
            string Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int length = Path.IndexOf("Debug\\");
            Path = Path.Substring(0, length);
            Path += "Debug\\UserArtists.csv";
            Artists art = GetArtistFromCsv(ArtistId, Path);
            SetArtistCsv(art);
            SetUserFan(ArtistId, authUser);
            return true;
        }


        // Private Methods
        /// <summary>
        /// Update the Csv Artist  or create it if it doesn't existed
        /// </summary>
        /// <param name="RestList"></param>
        private static void UpdateCsvArtists(List<Artists> RestList)
        {
            List<Artists> Artists = new List<Artists>();
            string Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int length = Path.IndexOf("Debug\\");
            Path = Path.Substring(0, length);
            Path += "Debug\\UserArtists.csv";
            string[] data = new string[RestList.Count + 1];
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
            data[0] = "id,name,image,joindate,updatedate,relations";
            for (int i = 0; i < RestList.Count; i++)
            {
                Artists item = RestList[i];
                string line = $"{item.id},{item.name},{item.image},{item.JoinDate},{item.UpdateDate},{item.relations.Fan}";
                data[i + 1] = line;
            }
            File.WriteAllLines(Path, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="artist"></param>
        /// <param name="Artists"></param>
        private static void SetArtistCsv(Artists artist)
        {
            string Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int length = Path.IndexOf("Debug\\");
            Path = Path.Substring(0, length);
            Path += "Debug\\UserArtists.csv";

            if (!File.Exists(Path))
            {
                File.Create(Path);
            }
            else
            {
                List<string> content = new List<string>();
                content.Add($"{artist.id},{artist.name},{artist.image},{artist.JoinDate},{artist.UpdateDate},{artist.relations.Fan}");
                File.AppendAllLines(Path, content);
            }
        }
        private static Artists GetArtistFromCsv(string id, string Path)
        {
            Artists artist = File.ReadAllLines(Path).
                Where(v => v.Contains(id)).
                Select(v => CsvToArtist(v)).FirstOrDefault();
            return artist;
        }
        private static Artists CsvToArtist(string csvline)
        {
            string[] values = csvline.Split(',');
            return new Artists()
            {
                id = values[0],
                name = values[1],
                image = values[2],
                JoinDate = values[3],
                UpdateDate = values[4],
                relations = new Relation() { Fan = values[5] }
            };
        }
        private static string ArtistToCsv(Artists artist)
        {
            string line = $"{artist.id},{artist.name},{artist.image},{artist.JoinDate},{artist.UpdateDate},{artist.relations.Fan}";
            return line;
        }
    }
}
