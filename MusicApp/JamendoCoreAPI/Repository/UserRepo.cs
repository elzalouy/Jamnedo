using JamendoCoreAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using RestSharp;
using Newtonsoft;
using Newtonsoft.Json;

namespace JamendoCoreAPI.Repository
{
    public class UserRepo
    {
        /// <summary>
        /// Get The user using http request and then update CSV if not similar.
        /// </summary>
        /// <returns>User</returns>
        public static User GetUser(User_Authentication _userAuth)
        {
            User CSVuser = CsvUser();

            User RestUser = RestRequest(_userAuth);
            if (!RestUser.Equals(CSVuser))
            {
                UpdateCSV(RestUser);
            }
            return RestUser;
        }

        //Private Methods
        private static User RestRequest(User_Authentication auth)
        {
            try
            {
                if (auth.token.Expiring_Date < DateTime.Now)
                {
                    auth.RefreshAccessToken();
                }
                var RestClient = new RestClient(auth.token.URL);
                var Request = new RestRequest("users");
                Request.AddParameter("client_id", auth.token.Client_ID);
                Request.AddParameter("access_token", auth.token.Access_Token);
                var tRsponse = RestClient.Execute(Request);
                JsonUser usr = JsonConvert.DeserializeObject<JsonUser>(tRsponse.Content);
                return usr.Users.First();

            }
            catch
            {
                return null;
            }
        }

        private static User CsvUser()
        {
            User user = new User();
            string Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int length = Path.IndexOf("Debug\\");
            Path = Path.Substring(0, length);
            Path += "Debug\\UserDetails.csv";

            if (File.Exists(Path) && File.ReadAllText(Path).Length > 0)
            {
                user = File.ReadAllLines(Path).
                    Skip(1)
                    .Select(v => CsvToUser(v)).First();
            }
            return user;
        }
        private static User CsvToUser(string CsvLine)
        {
            string[] values = CsvLine.Split(',');
            return new User()
            {
                ID = values[0],
                Name = values[1],
                Language = values[2],
                Image = values[3],
                DispName = values[4],
                CreationDate = values[5]
            };
        }

        private static void UpdateCSV(User user)
        {
            string Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int length = Path.IndexOf("Debug\\");
            Path = Path.Substring(0, length);
            Path += "Debug\\UserDetails.csv";

            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
            else
            {
                string[] content = {"ID,Name,Language,Image,DispName,CreationDate"
                        , $"{user.ID},{user.Name},{user.Language},{user.Image},{user.DispName},{user.CreationDate}"};
                File.WriteAllLines(Path, content);
            }
        }
    }
}
