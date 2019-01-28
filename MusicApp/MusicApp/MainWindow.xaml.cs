using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Navigation;
using MusicApp.MusicAPIs.Repository;
using System.Web;
using System.Windows.Media.Imaging;
using MusicApp.MusicAPIs.JSON_Classes;
using MusicApp.Views;
using System.Windows.Media;

namespace MusicApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary
    public partial class MainWindow : Window
    {
        private User_Authentication userAuth = new User_Authentication();
        User User;

        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void Grid_Initialized(object sender, EventArgs e)
        {
            ArtistView.Visibility = Visibility.Visible;
            ArtistView.Visibility = Visibility.Hidden;

            //Here i will check if the Access Token is expired or not 
            if (userAuth.token.Access_Token == "" || userAuth.token.Access_Token == null || userAuth.token.Expiring_Date < DateTime.Now)
            {
                WebBrawser browser = new WebBrawser();
                browser.Show();
                this.Close();
            }
            else
            {
                User = UserRepo.GetUser();
                UserNamelbl.Content =User.DispName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource =new Uri(User.Image);
                bitmap.EndInit();
                UserImagelbl.Source = bitmap;
            }
        }
        
        private void FansBTN_Click(object sender, RoutedEventArgs e)
        {
            //Visible
            ArtistView.Visibility = Visibility.Visible;
            ArtistView.CheckUpdates(sender,e);
            //Hidden
            PublicArtistView.Visibility = Visibility.Hidden;
        }

        private void TracksBTN_Click(object sender, RoutedEventArgs e)
        {
            //Visible

            //Hidden
            PublicArtistView.Visibility = Visibility.Hidden;
            ArtistView.Visibility = Visibility.Hidden;
        }

        private void AlbumsBTN_Click(object sender, RoutedEventArgs e)
        {


            //Hidden
            PublicArtistView.Visibility = Visibility.Hidden;
            ArtistView.Visibility = Visibility.Hidden;
        }

        private void PublicArtists_Click(object sender, RoutedEventArgs e)
        {
            //Visible
            PublicArtistView.Visibility = Visibility.Visible;

            //Hidden
            ArtistView.Visibility = Visibility.Hidden;
        }
    }
}
