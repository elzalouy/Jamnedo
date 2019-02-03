using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using MusicApp.Views;
using JamendoCoreAPI.Repository;
using JamendoCoreAPI.Models;
namespace MusicApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User_Authentication _userAuth=new User_Authentication();
        public static MainWindow mainWindow=new MusicApp.MainWindow();
        public static WebBrawser WebBrawser;
        public static User _user;
        protected override void OnStartup(StartupEventArgs e)
        {
            if (_userAuth.token.Access_Token == null)
            {
                WebBrawser = new WebBrawser();
                WebBrawser.Show();
            }
            else
            {
                _user = UserRepo.GetUser(_userAuth);
            }
        }
    }
}
