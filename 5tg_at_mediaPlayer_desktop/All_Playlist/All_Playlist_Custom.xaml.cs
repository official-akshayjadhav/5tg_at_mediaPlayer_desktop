using _5tg_at_mediaPlayer_desktop.connection;
using _5tg_at_mediaPlayer_desktop.Playlist;
using _5tg_at_mediaPlayer_desktop.Popup;
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

namespace _5tg_at_mediaPlayer_desktop.All_Playlist
{
    /// <summary>
    /// Interaction logic for All_Playlist_Custom.xaml
    /// </summary>
    public partial class All_Playlist_Custom : UserControl
    {
        public All_Playlist_Custom()
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
                if (Global_Log.connectionClass == null)
                {
                    Global_Log.connectionClass = new ConnectionClass();
                }
                //DataTable dt = Global_Log.connectionClass.retriveData("select PID,name,CAST(createdDate as date), TotalSong from playlists", "playlists");

                string query = "select PID, name, CAST(createdDate as date), TotalSong, Schedule from playlists";

                DataTable dt = Global_Log.connectionClass.retriveData(query, "playlists");
                
  
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
                            TotalSong = Convert.ToInt32(dr.ItemArray[3]),
                            Schedule = dr.ItemArray[4].ToString()
                        }) ;
                    }
                    catch (Exception ex)
                    { }
                }
                if (allPlaylist != null)
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
                //else if (currentOperation == "View")
                //{
                //    currentPlaylist = playlists.PID;

                //    loadPlaylistSong(Global_Log.pID, playlists.SortId);
                //}
                else if (currentOperation == "Schedule")
                {
                    //currentPlaylist = playlists.PID;
                    Schedular schedular = new Schedular();
                    schedular.ShowDialog();
                    //loadPlaylistSong(Global_Log.pID, playlists.SortId);

                    if (Global_Log.fM_Custom == null)
                    {
                        Global_Log.fM_Custom = new FM.FM_Custom();
                    }
                    Global_Log.fM_Custom.AutoPlay_PreviewMouseLeftButtonDown();

                    
                }
            }
            loadPlaylist();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Create_Playlist createPlaylist = new Create_Playlist();
            createPlaylist.ShowDialog();
            loadPlaylist();
        }
    }
}
