using _5tg_at_mediaPlayer_desktop.connection;
using _5tg_at_mediaPlayer_desktop.Bottom_Media_Control;
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
                else if (currentOperation == "Delete")
                {
                    create_Playlist.deletePlaylist(playlists.PID);
                }
                else if (currentOperation == "View")
                {
                    loadPlaylistSong(Global_Log.pID);
                }
            }
            loadPlaylist();
        }

        private void loadPlaylistSong(int playlistID)
        {
            List<PlaylistAudio> playlistAudio = new List<PlaylistAudio>();
            
            string query = "select ID, title, duration from Audio where ID in(select AID from playlist where PID = " + playlistID + ")";

            DataTable dt = Global_Log.connectionClass.retriveData(query, "Audio");

            int count = dt.Rows.Count;
            Playlists playlists = new Playlists();
            for (int i = 0; i < count; i++)
            {
                DataRow dr = dt.Rows[i];
                try
                {
                    playlistAudio.Add(new PlaylistAudio()
                    {
                        AID = Convert.ToInt32(dr[0]),
                        PID = playlistID,
                        Name = dr[1].ToString(),
                        Duration = (TimeSpan)dr[2]
                    });
                }
                catch (Exception ex)
                { }
            }
            PlaylistSong.ItemsSource = null;
            PlaylistSong.Items.Clear();
            PlaylistSong.ItemsSource = playlistAudio;
        }

        private void PlaylistSongEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var image = e.AddedItems[0] as ComboBoxItem;
            string currentOperation = image.Content.ToString();

            var ttp = sender as ComboBox;
            PlaylistAudio PlaylistAudio = null;
            if (image != null && ttp.Tag is PlaylistAudio)
            {
                PlaylistAudio = (PlaylistAudio)ttp.Tag;
            }
            { }
            if (PlaylistAudio != null)
            {
                Create_Playlist create_Playlist = new Create_Playlist();
                Global_Log.pID = PlaylistAudio.PID;
                Global_Log.playlistName = PlaylistAudio.Name;

                if (currentOperation == "Play")
                {
                    //create_Playlist.SetProperty(playlists.Name.ToString());
                    Bottom_Media_Control.Bottom_Media_Control bottom_Media_Control = new Bottom_Media_Control.Bottom_Media_Control();
                    bottom_Media_Control.PlayMediaFunction();
                }
                else if (currentOperation == "Delete")
                {
                    string query = "delete playlist where PID =" + PlaylistAudio.PID + "and AID = " + PlaylistAudio.AID;
                    Global_Log.connectionClass.insertData(query);
                }
            }
            loadPlaylistSong(PlaylistAudio.PID);
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Create_Playlist createPlaylist = new Create_Playlist();
            createPlaylist.ShowDialog();
            loadPlaylist();
        }

    }
}
