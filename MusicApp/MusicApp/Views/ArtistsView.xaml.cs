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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MusicApp.MusicAPIs.JSON_Classes;
using MusicApp.MusicAPIs.Repository;

namespace MusicApp.Views
{
    /// <summary>
    /// The ArtistsView is the place where you can see the Artists that the user are following.
    /// </summary>
    public partial class ArtistsView : UserControl
    {
        List<Artists> UserArtists = new List<Artists>();
        private User_Authentication auth=new User_Authentication();
        static int IndexOfArtist = 0;

        /// <summary>
        /// Initial Component for Initializing the ArtistView Components
        /// </summary>
        public ArtistsView()
        {
            InitializeComponent();
        }
        

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            // Get the Data from  the CSVFile then check similarity between it and Rest after displaying.
            UserArtists = UserArtistRepo.GetCsvArtist(auth);
            if (UserArtists.Count == 1)
            {
                P1.Visibility = Visibility.Hidden;
                P3.Visibility = Visibility.Hidden;
                P2.Visibility = Visibility.Hidden;
                id.Content = UserArtists[0].id;
                ArtistImage.Source = UserRepo.GetArtistImage(UserArtists[0].image);
                ArtistName.Content = UserArtists[0].name;
            }
            else if (UserArtists.Count == 2)
            {
                P2.Visibility = Visibility.Hidden;
                P3.Visibility = Visibility.Hidden;

                id.Content = UserArtists[0].id;
                id1.Content = UserArtists[1].id;

                ArtistImage.Source = UserRepo.GetArtistImage(UserArtists[0].image);
                ArtistImage1.Source = UserRepo.GetArtistImage(UserArtists[1].image);

                ArtistName.Content = UserArtists[0].name;
                ArtistName1.Content = UserArtists[1].name;
            }
            else if (UserArtists.Count == 3)
            {
                P3.Visibility = Visibility.Hidden;

                ArtistImage.Source = UserRepo.GetArtistImage(UserArtists[0].image);
                ArtistImage1.Source = UserRepo.GetArtistImage(UserArtists[1].image);
                ArtistImage2.Source = UserRepo.GetArtistImage(UserArtists[2].image);

                ArtistName.Content = UserArtists[0].name;
                ArtistName1.Content = UserArtists[1].name;
                ArtistName2.Content = UserArtists[2].name;
            }
            else if (UserArtists.Count >= 4)
            {
                ArtistImage.Source = UserRepo.GetArtistImage(UserArtists[0].image);
                ArtistImage1.Source = UserRepo.GetArtistImage(UserArtists[1].image);
                ArtistImage2.Source = UserRepo.GetArtistImage(UserArtists[2].image);
                ArtistImage3.Source = UserRepo.GetArtistImage(UserArtists[3].image);

                ArtistName.Content = UserArtists[0].name;
                ArtistName1.Content = UserArtists[1].name;
                ArtistName2.Content = UserArtists[2].name;
                ArtistName3.Content = UserArtists[3].name;
            }

        }
        internal void CheckUpdates(object sender,EventArgs e)
        {
            // Check Similarity between the rest and CsvFile
            UserArtists = UserArtistRepo.CheckSimilarty(UserArtists, auth);
            if (UserArtists.Count == 1)
            {
                P.Visibility = Visibility.Visible;
                P1.Visibility = Visibility.Hidden;
                P3.Visibility = Visibility.Hidden;
                P2.Visibility = Visibility.Hidden;

                ArtistImage.Source = UserRepo.GetArtistImage(UserArtists[0].image);
                ArtistName.Content = UserArtists[0].name;
            }
            else if (UserArtists.Count == 2)
            {
                P.Visibility = Visibility.Visible;
                P1.Visibility = Visibility.Visible;
                P2.Visibility = Visibility.Hidden;
                P3.Visibility = Visibility.Hidden;

                ArtistImage.Source = UserRepo.GetArtistImage(UserArtists[0].image);
                ArtistImage1.Source = UserRepo.GetArtistImage(UserArtists[1].image);

                ArtistName.Content = UserArtists[0].name;
                ArtistName1.Content = UserArtists[1].name;
            }
            else if (UserArtists.Count == 3)
            {

                P.Visibility = Visibility.Visible;
                P1.Visibility = Visibility.Visible;
                P2.Visibility = Visibility.Visible;
                P3.Visibility = Visibility.Hidden;

                ArtistImage.Source = UserRepo.GetArtistImage(UserArtists[0].image);
                ArtistImage1.Source = UserRepo.GetArtistImage(UserArtists[1].image);
                ArtistImage2.Source = UserRepo.GetArtistImage(UserArtists[2].image);

                ArtistName.Content = UserArtists[0].name;
                ArtistName1.Content = UserArtists[1].name;
                ArtistName2.Content = UserArtists[2].name;
            }
            else if (UserArtists.Count >= 4)
            {

                P.Visibility = Visibility.Visible;
                P1.Visibility = Visibility.Visible;
                P2.Visibility = Visibility.Visible;
                P3.Visibility = Visibility.Visible;
                ArtistImage.Source = UserRepo.GetArtistImage(UserArtists[0].image);
                ArtistImage1.Source = UserRepo.GetArtistImage(UserArtists[1].image);
                ArtistImage2.Source = UserRepo.GetArtistImage(UserArtists[2].image);
                ArtistImage3.Source = UserRepo.GetArtistImage(UserArtists[3].image);

                ArtistName.Content = UserArtists[0].name;
                ArtistName1.Content = UserArtists[1].name;
                ArtistName2.Content = UserArtists[2].name;
                ArtistName3.Content = UserArtists[3].name;
            }
        }
    }
}
