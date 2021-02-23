using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using _5tg_at_mediaPlayer_desktop.connection;

namespace _5tg_at_mediaPlayer_desktop
{
    /// <summary>
    /// Interaction logic for AddToPlaylistWindow.xaml
    /// </summary>
    public partial class AddToPlaylistWindow : Window
    {
        public AddToPlaylistWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string query = "select * from playlists";
            DataTable dt = Global_Log.connectionClass.retriveData(query, "playlists");
            { }

            List<string> playlistName = new List<string>();
            int cnt = dt.Rows.Count;

            for (int i = 0; i < cnt; i++)
            {
                DataRow dr = dt.Rows[i];
                playlistName.Add(dr[1].ToString());

            }

            comboAddToPlaylist.Items.Clear();
            comboAddToPlaylist.ItemsSource = playlistName;
        }

        private void BtnAddPlaylist_Click(object sender, RoutedEventArgs e)
        {
            string playlistName = comboAddToPlaylist.SelectedItem.ToString();

            string totalSongItem = "update playlists set TotalSong = TotalSong + 1 where name = '" + playlistName + "'";

            Global_Log.connectionClass.insertData(totalSongItem);

            string getPlaylistID = "select PID from playlists where name = '" + playlistName + "'";
            DataTable dt = Global_Log.connectionClass.retriveData(getPlaylistID, "playlists");
            DataRow dr = dt.Rows[0];

            int PIDValue = Convert.ToInt32(dr[0]);

            string playlistEntry = "insert into playlist(PID, AID) values(" + PIDValue + "," + Global_Log.audio.ID + ")";

            Global_Log.connectionClass.insertData(totalSongItem);
            Global_Log.connectionClass.insertData(playlistEntry);
            MessageBox.Show("Playlist is updated");
        }
    }
}
