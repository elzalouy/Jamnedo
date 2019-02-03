using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Navigation;
using System.Web;
using System.Windows.Media.Imaging;
using MusicApp.Views;
using System.Windows.Media;
using JamendoCoreAPI.Models;
using JamendoCoreAPI.Repository;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MusicApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary
    public partial class MainWindow : Window
    {

        private static List<Tracks> SearchList = new List<Tracks>();
        public static int indexOfTrack = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
        }
        
        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            string searcVal = searchBox.Text.ToString();
            SearchList = UserTracks_Repo.SearchTracks(App._userAuth,searcVal);
            RefreshScrollViewer(SearchList);
        }
        private void RefreshScrollViewer(List<Tracks> SearchList)
        {
            indexOfTrack = 0;
            NextOrPrevious(indexOfTrack);
            SearchTracksScrollViewer.Visibility = Visibility.Visible;
        }

        private void NextBTN_Click(object sender, RoutedEventArgs e)
        {
            if (SearchList.Count >= indexOfTrack + 10)
            {
                indexOfTrack += 10;
                NextOrPrevious(indexOfTrack);
            }
        }
        private void NextOrPrevious(int index)
        {
            Track1.Visibility = Visibility.Hidden;
            Track2.Visibility = Visibility.Hidden;
            Track3.Visibility = Visibility.Hidden;
            Track4.Visibility = Visibility.Hidden;
            Track5.Visibility = Visibility.Hidden;
            Track6.Visibility = Visibility.Hidden;
            Track7.Visibility = Visibility.Hidden;
            Track8.Visibility = Visibility.Hidden;
            Track9.Visibility = Visibility.Hidden;
            Track10.Visibility = Visibility.Hidden;
            if (SearchList.Count <= 10)
            {
                NextBTN.IsEnabled = false;
                PreviousBTN.IsEnabled = false;
            }
            if (SearchList.Count >= index+1)
            {
                Track1.Visibility = Visibility.Visible;
                Track1Name.Content = SearchList[index].name;
                Track1Image.Source = new BitmapImage(new Uri(SearchList[index].image));
            }
            if (SearchList.Count >= index+2)
            {
                Track2.Visibility = Visibility.Visible;
                Track2Name.Content = SearchList[index+1].name;
                Track2Image.Source = new BitmapImage(new Uri(SearchList[index+1].image));
            }
            if (SearchList.Count >= index+3)
            {
                Track3.Visibility = Visibility.Visible;
                Track3Name.Content = SearchList[index+2].name;
                Track3Image.Source = new BitmapImage(new Uri(SearchList[index+2].image));
            }
            if (SearchList.Count >= index+4)
            {

                Track4.Visibility = Visibility.Visible;
                Track4Name.Content = SearchList[index+3].name;
                Track4Image.Source = new BitmapImage(new Uri(SearchList[index+3].image));
            }
            if (SearchList.Count >= index+5)
            {

                Track5.Visibility = Visibility.Visible;
                Track5Name.Content = SearchList[index+4].name;
                Track5Image.Source = new BitmapImage(new Uri(SearchList[index+4].image));
            }
            if (SearchList.Count >= index+6)
            {
                Track6.Visibility = Visibility.Visible;
                Track6Name.Content = SearchList[index+5].name;
                Track6Image.Source = new BitmapImage(new Uri(SearchList[index+5].image));
            }
            if (SearchList.Count >= index+7)
            {

                Track7.Visibility = Visibility.Visible;
                Track7Name.Content = SearchList[index+6].name;
                Track7Image.Source = new BitmapImage(new Uri(SearchList[index+6].image));
            }
            if (SearchList.Count >= index+8)
            {
                Track8.Visibility = Visibility.Visible;
                Track8Name.Content = SearchList[index+7].name;
                Track8Image.Source = new BitmapImage(new Uri(SearchList[index+7].image));
            }
            if (SearchList.Count >= index+9)
            {
                Track9.Visibility = Visibility.Visible;
                Track9Name.Content = SearchList[index+8].name;
                Track9Image.Source = new BitmapImage(new Uri(SearchList[index+8].image));
            }
            if (SearchList.Count >= index+10)
            {
                Track10.Visibility = Visibility.Visible;
                Track10Name.Content = SearchList[index+9].name;
                Track10Image.Source = new BitmapImage(new Uri(SearchList[index+9].image));
            }
        }
        private void PreviousBTN_Click(object sender, RoutedEventArgs e)
        {
            if(indexOfTrack>0 && SearchList.Count >= indexOfTrack)
            {
                indexOfTrack -= 10;
                NextOrPrevious(indexOfTrack);
            }
        }

        private void AddTo_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if(button != null)
            {
                int index = GetIndexOfTrack(button.Uid) + indexOfTrack;
                Tracks AddTrack = SearchList[index];
                UserTracks_Repo.AddTrackToFavorites(App._userAuth, AddTrack.id);
            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if(button != null)
            {
                int index = GetIndexOfTrack(button.Uid) + indexOfTrack;
                Tracks PlayTrack = SearchList[index];
            }
        }
        private int GetIndexOfTrack(string Uid)
        {
            int index = 0;
            
            if (Uid.Contains("10"))
            {
                index = 9;
            }
            else if (Uid.Contains("9"))
            {
                index = 8;
            }
            else if (Uid.Contains("8"))
            {
                index = 7;
            }
            else if (Uid.Contains("7"))
            {
                index = 6;
            }
            else if (Uid.Contains("6"))
            {
                index = 5;
            }
            else if (Uid.Contains("5"))
            {
                index = 4;
            }
            else if (Uid.Contains("4"))
            {
                index = 3;
            }
            else if (Uid.Contains("3"))
            {
                index = 2;
            }
            else if (Uid.Contains("2"))
            {
                index = 1;
            }
            else if (Uid.Contains("1"))
            {
                index = 0;
            }
            return index;
        }
    }
}
