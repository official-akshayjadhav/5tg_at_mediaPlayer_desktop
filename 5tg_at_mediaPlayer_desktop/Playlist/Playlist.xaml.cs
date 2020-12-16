using _5tg_at_mediaPlayer_desktop.connection;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace _5tg_at_mediaPlayer_desktop.Playlist
{
    /// <summary>
    /// Interaction logic for Playlist.xaml
    /// </summary>
    public partial class Playlist : UserControl
    {
        public Playlist()
        {
            InitializeComponent();

            loadPlaylist();
        }

        public void loadPlaylist()
        {
            if (Global_Log.playBack == null)
            {
                Global_Log.playBack = new PlayBack();
            }

            List<Playlists> list_Playlists = new List<Playlists>();
            try
            {
                DataTable dt = Global_Log.connectionClass.retriveData("select PID,name,CAST(createdDate as date), TotalSong from playlists", "playlists");

                int count = dt.Rows.Count;
                Playlists playlists = new Playlists();
                for (int i = 0; i < count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    try
                    {
                        list_Playlists.Add(new Playlists()
                        {
                            PID = Convert.ToInt32(dr.ItemArray[0]),
                            Name = dr.ItemArray[1].ToString(),
                            Date = dr.ItemArray[2].ToString(),
                            TotalSong = Convert.ToInt32(dr.ItemArray[3])
                        });
                    }
                    catch (Exception ex)
                    { }
                }
                allPlaylist.ItemsSource = list_Playlists;
            }
            catch (Exception ex)
            { }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var image = e.AddedItems[0] as ComboBoxItem;
            string currentOperation = image.Content.ToString();

            var ttp = sender as ComboBox;
            Playlists playlists = null;
            if (image != null && ttp.Tag is Playlists)
            {
                playlists = (Playlists)ttp.Tag;
            }

            if (playlists != null)
            {
                Create_Playlist create_Playlist = new Create_Playlist();
                Global_Log.pID = playlists.PID;
                Global_Log.playlistName = playlists.Name;

                if (currentOperation == "Update")
                {
                    create_Playlist.SetProperty(playlists.Name.ToString());
                }
                else if (currentOperation == "Dalete")
                {
                    create_Playlist.deletePlaylist(playlists.PID);
                }
            }
            loadPlaylist();
        }
    }
}
