using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MusicApp.MusicAPIs.JSON_Classes;
using Newtonsoft.Json;
using System.Windows;
using RestSharp.Authenticators;

namespace MusicApp.MusicAPIs.Repository
{
    internal class User_Authentication
    { 
        // It will stay static and cashed on the Ram until the Process is in executing time.
        internal AccessToken token = new AccessToken();
        private string csvFilePath;

        /// <summary>
        /// In this Class Component,The Method called GetCsvAuth() has beed called to get AccessToken from CSV file
        /// </summary>
        internal User_Authentication()
        {
            //assign token variables
            token.URL = "https://api.jamendo.com/v3.0/";
            token.Client_ID = "152fe76f";
            token.Client_Secret = "6098c1d73dc1d7d826ba1172c3b5f74a";
            token.Redirect_Url = "AndrewJobelApp";

            //Check if the there is a csv file fir auth or not and then if it contains the Access token ,assign it to the token object
            GetCSVAuth();
        }


        /// <summary>
        /// Getting the authorization code from the Web Service for requesting the Access Token.
        /// </summary>
        /// <returns>Authorization Code</returns>
        internal string GetAuthCode()
        {
            try
            {
                token.Response_Type = "code";
                var restclient = new RestClient(token.URL);
                restclient.Authenticator = OAuth1Authenticator.ForRequestToken(token.Client_ID,token.Client_Secret);
                RestRequest request = new RestRequest("oauth/authorize") { Method = Method.GET };
                request.AddParameter("client_id",token.Client_ID);
                request.AddParameter("client_secret",token.Client_Secret);
                request.AddParameter("response_type",token.Response_Type);
                request.AddParameter("redirect_uri",token.Redirect_Url);
                var tResponse = restclient.Execute(request);
                if (tResponse.ResponseUri.IsAbsoluteUri)
                {
                    return tResponse.ResponseUri.AbsoluteUri;
                }
                else return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("May be because the Internet connection is lost or other things.");
                return "Error";
            }   
        }


        /// <summary>
        /// Using the Authorization code in the webBrowser Window to get the Access token from the web service
        /// </summary>
        /// <param name="code"></param>
         internal void GetAccessToken(string code)
        {
            try
            {

                if (token.Expiring_Date < DateTime.Now)
                {
                    token.Grant_Type = "authorization_code";
                    token.Authorization_Code = code;
                    var restClient = new RestClient(token.URL);
                    RestRequest request = new RestRequest("oauth/grant") { Method = Method.POST };
                    request.AddParameter("client_id", token.Client_ID);
                    request.AddParameter("client_secret", token.Client_Secret);
                    request.AddParameter("grant_type", token.Grant_Type);
                    request.AddParameter("code", token.Authorization_Code);
                    request.AddParameter("redirect_uri", "AndrewJobelApp");
                    var tResponse = restClient.Execute(request);
                    var tkn = JsonConvert.DeserializeObject<AccessToken>(tResponse.Content);
                    token.Access_Token = tkn.Access_Token;
                    token.expires_in = tkn.expires_in;
                    token.token_type = tkn.token_type;
                    token.scope = tkn.scope;
                    token.refresh_token = tkn.refresh_token;
                    token.Expiring_Date = DateTime.Now.AddHours(int.Parse(tkn.expires_in) / 3600);
                    SaveToCsv();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("There are something wrong, May be the Internet Connection is lost.");
            }
        }

        /// <summary>
        /// If the Access Token is expired it will called to refresh it.
        /// </summary>
        internal void RefreshAccessToken()
        {
            try
            {

                token.Grant_Type = "refresh_token";
                var restClient = new RestClient(token.URL);
                RestRequest request = new RestRequest("oauth/grant") { Method = Method.POST };
                request.AddParameter("client_id", token.Client_ID);
                request.AddParameter("client_secret", token.Client_Secret);
                request.AddParameter("grant_type", token.Grant_Type);
                request.AddParameter("refresh_token", token.refresh_token);
                var tResponse = restClient.Execute(request);
                var tkn = JsonConvert.DeserializeObject<AccessToken>(tResponse.Content);
                token.Access_Token = tkn.Access_Token;
                token.expires_in = tkn.expires_in;
                token.token_type = tkn.token_type;
                token.scope = tkn.scope;
                token.refresh_token = tkn.refresh_token;
                token.Expiring_Date = DateTime.Now.AddHours(int.Parse(tkn.expires_in) / 3600);
                SaveToCsv();
            }
            catch(Exception ex)
            {
                MessageBox.Show("There are something worng, may be the internet connection is lost");
            }
        }
        


        /// <summary>
        /// Covert CSV file to Access Token Object
        /// </summary>
        /// <param name="csvLine"></param>
        /// <returns>token</returns>
        private AccessToken CsvToToken(string csvLine)
        {
            string[] values = csvLine.Split(',');
            AccessToken token = new AccessToken();
            token.Access_Token = values[0];
            token.expires_in = values[1];
            token.token_type = values[2];
            token.scope = values[3];
            token.refresh_token = values[4];
            token.Expiring_Date = Convert.ToDateTime(values[5]);
            return token;
        }


        /// <summary>
        /// Saving the Access Token to The Csv File
        /// </summary>
        private void SaveToCsv()
        {
            try
            {
                if (File.Exists(csvFilePath))
                {
                    File.Delete(csvFilePath);
                }
                string[] content = { "Access_Token,expires_in,token_type,scope,refresh_token,Expiring_Date",
                    $"{token.Access_Token},{token.expires_in},{token.token_type},{token.scope},{token.refresh_token},{token.Expiring_Date}" };
                File.WriteAllLines(csvFilePath, content);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// It will assign the Access Token with the value if the Access Token has not expired yet.
        /// </summary>
        private void GetCSVAuth()
        {
            // CSV Path finding the file
            string Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int length = Path.IndexOf("Debug\\");
            Path = Path.Substring(0, length);
            Path += "Debug\\UserAccess.csv";
            csvFilePath = Path;


            // If the file is existed and contains the AccessToken then assign the variable with its values, else then assign it as null value.
            if (File.Exists(Path) && File.ReadAllText(Path).Length > 0)
            {
                AccessToken tkn = File.ReadAllLines(csvFilePath)
                                        .Skip(1)
                                        .Select(v => CsvToToken(v)).First();
                token.Access_Token = tkn.Access_Token;
                token.expires_in = tkn.expires_in;
                token.Expiring_Date = tkn.Expiring_Date;
                token.token_type = tkn.token_type;
                token.scope = tkn.scope;
                token.refresh_token = tkn.refresh_token;
                if (tkn.Expiring_Date < DateTime.Now)
                {
                    RefreshAccessToken();
                }
                else
                {
                    token.Access_Token = tkn.Access_Token;
                    token.refresh_token = tkn.refresh_token;
                    token.Expiring_Date = tkn.Expiring_Date;
                    token.expires_in = tkn.expires_in;
                    token.token_type = tkn.token_type;
                    token.scope = tkn.scope;
                }
            }
            else
            {
                token.Access_Token = null;
                token.Authorization_Code = null;
            }
        }
    }
}
