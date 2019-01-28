using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MusicApp.MusicAPIs.JSON_Classes;
using MusicApp.MusicAPIs.Repository;
using System.Web;

namespace MusicApp.Views
{
    /// <summary>
    /// Interaction logic for WebBrawser.xaml
    /// </summary>
    public partial class WebBrawser : Window
    {
        private User_Authentication GetUser=new User_Authentication();
        public WebBrawser()
        {
            InitializeComponent();
        }

        private void webBrowser_Initialized(object sender, EventArgs e)
        {
            string url = GetUser.GetAuthCode();
            webBrowser.Navigate(new Uri(url));
        }
        
        private void webBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (webBrowser.Source.AbsoluteUri.Contains("code="))
            {
                //Get Access Token and then Close the browser and open the MainWindow
                Uri source = webBrowser.Source;
                webBrowser.Visibility = Visibility.Hidden;
                webBrowser.IsEnabled = false;
                var qs = HttpUtility.ParseQueryString(source.Query).Get("code");
                GetUser.GetAccessToken(qs);

                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }
    }
}
