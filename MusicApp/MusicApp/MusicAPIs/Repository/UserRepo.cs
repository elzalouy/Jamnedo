using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicApp.MusicAPIs.JSON_Classes;
using RestSharp;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using System.IO;

namespace MusicApp.MusicAPIs.Repository
{
    public static class UserRepo
    {

        private static User_Authentication auth = new User_Authentication();
        //If User is similar to the old in CSV then only return, otherwise Update the Csv first
        internal static User GetUser()
        {
            User CSVuser = CsvUser();

            User RestUser = RestRequest();
            if (!RestUser.Equals(CSVuser))
            {
                UpdateCSV(RestUser);
            }
            return RestUser;
        }

        internal static BitmapImage GetArtistImage(string path)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();
            return bitmap;
        }

        //Private Methods
        private static User RestRequest()
        {
            try
            {
                var RestClient = new RestClient(auth.token.URL);
                var Request = new RestRequest("users");
                Request.AddParameter("client_id", auth.token.Client_ID);
                Request.AddParameter("access_token", auth.token.Access_Token);
                var tRsponse = RestClient.Execute(Request);
                JsonUser usr = JsonConvert.DeserializeObject<JsonUser>(tRsponse.Content);
                return usr.Users.First();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static User CsvUser()
        {
            User user=new User();
            string Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int length = Path.IndexOf("Debug\\");
            Path = Path.Substring(0, length);
            Path += "Debug\\UserDetails.csv";

            if(File.Exists(Path) && File.ReadAllText(Path).Length > 0)
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
            return new User() {
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
                File.WriteAllLines(Path,content);
            }
        }
    }
}
