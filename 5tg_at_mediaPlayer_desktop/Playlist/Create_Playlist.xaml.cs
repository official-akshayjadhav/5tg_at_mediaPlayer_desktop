using _5tg_at_mediaPlayer_desktop.connection;
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
using System.Windows.Shapes;

namespace _5tg_at_mediaPlayer_desktop.Playlist
{
    /// <summary>
    /// Interaction logic for Create_Playlist.xaml
    /// </summary>
    public partial class Create_Playlist : Window
    {
        public Create_Playlist()
        {
            InitializeComponent();
            //SetProperty("");
        }

        string query = "";
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SetControl();
            this.Close();
        }

        public void SetProperty(string value = "")
        {
            if (value == "")
            {
                this.Title = "Create Playlist";
                HeaderText.Text = "New Playlist:";
                txtPlaylist.Text = "";
                button1.Content = "Create Playlist";
            }
            else
            {
                this.Title = "Update Playlist";
                HeaderText.Text = "Update Playlist:";
                txtPlaylist.Text = value;
                button1.Content = "Update Playlist";
                this.ShowDialog();
            }
            this.ShowDialog();
        }

        public void SetControl()
        {
            try
            {
                //Global_Log.pID = playlists.PID;
                //Global_Log.playlistName = playlists.Name;

                int count = 0;
                if (HeaderText.Text == "New Playlist:")
                {
                    string get = "GETDATE()";
                    query = "insert into playlists(name, createdDate, TotalSong) values('" + txtPlaylist.Text + "'," + get + "," + count + ")";
                    Global_Log.connectionClass.insertData(query);
                    MessageBox.Show(this, "Playlist Created..!", "Playlist");
                }
                else if (HeaderText.Text == "Update Playlist:")
                {
                    //query = "update playlists set totalsong =" + count + " where pid =" + 2;
                    query = "update playlists set name='" + txtPlaylist.Text + "'where PID=" + Global_Log.pID;
                    Global_Log.connectionClass.insertData(query);
                    MessageBox.Show(this, "Playlist Updated..!", "Playlist");
                }

            }
            catch (Exception ex)
            {
                Global_Log.EXC_WriteIn_LOGfile(ex.StackTrace);
                
            }

            this.Close();
        }

        public void deletePlaylist(int PID)
        {
            string query = "delete playlists where PID = " + PID;
            Global_Log.connectionClass.insertData(query);

            MessageBox.Show("Playlist Successfully Deleted");
        }
    }
}
