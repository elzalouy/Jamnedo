using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for PublicArtists.xaml
    /// </summary>
    public partial class PublicArtists : UserControl
    {
        private User_Authentication auth;
        public PublicArtists()
        {
            InitializeComponent();
        }
        private void ListView_Initialized(object sender, EventArgs e)
        {
        }
    }
}
