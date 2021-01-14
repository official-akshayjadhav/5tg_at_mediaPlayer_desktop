using _5tg_at_mediaPlayer_desktop.connection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace _5tg_at_mediaPlayer_desktop.Playlist
{
    /// <summary>
    /// Interaction logic for Add_To_Playlist.xaml
    /// </summary>
    public partial class Add_To_Playlist : Window
    {
        public Add_To_Playlist()
        {
            InitializeComponent();
        }

        public ObservableCollection<PlaylistData> playlistDatas { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select * from playlists";
                DataTable dt = Global_Log.connectionClass.retriveData(query, "playlists");

                playlistDatas = new ObservableCollection<PlaylistData>();

                int count = dt.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    CheckBox chb = new CheckBox();
                    chb.Content = dr[1].ToString();
                    //stackpanal_Playlist.Children.Add(chb);
                    stackpanal_Playlist.Items.Add(chb);
                }
            }
            catch (Exception ex)
            { }
        }

        private void AddPlaylist_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in stackpanal_Playlist.Items)
            {
                CheckBox chk = (CheckBox)item;
                if (chk.IsChecked == true)
                {
                    string playlistName = chk.Content.ToString();
                    { }
                    string totalSongItem = "update playlists set TotalSong = TotalSong + 1 where name = '" + playlistName + "'";
                    { }
                    Global_Log.connectionClass.insertData(totalSongItem);
                    { }
                    string getPlaylistID = "select PID from playlists where name = '" + playlistName + "'";
                    DataTable dt = Global_Log.connectionClass.retriveData(getPlaylistID, "playlists");
                    DataRow dr = dt.Rows[0];
                    { }
                    int PIDValue = Convert.ToInt32(dr[0]);
                    { }
                    string playlistEntry = "insert into playlist(PID, AID) values(" + PIDValue + "," + Global_Log.audio.ID + ")";
                    { }
                    Global_Log.connectionClass.insertData(totalSongItem);
                    Global_Log.connectionClass.insertData(playlistEntry);
                    MessageBox.Show("Playlist is updated");
                }
            }
            this.Close();
        }
    }
}
