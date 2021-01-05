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
using System.Windows.Shapes;

namespace _5tg_at_mediaPlayer_desktop.Popup
{
    /// <summary>
    /// Interaction logic for Command.xaml
    /// </summary>
    public partial class Command : Window
    {
        public Command()
        {
            InitializeComponent();
        }

        private void addCommand_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Global_Log.connectionClass == null)
            {
                Global_Log.connectionClass = new ConnectionClass();
            }
            
            string selectedCmd = Commands.SelectedItem.ToString();
            string playlistName = Playlist.SelectedItem.ToString();
            string DateTime = DateTimePicker.Value.ToString();
            
            string getPlaylistID = "select PID from playlists where name = '" + playlistName + "'";
            DataTable dt = Global_Log.connectionClass.retriveData(getPlaylistID, "playlists");
            DataRow dr = dt.Rows[0];
            int PIDValue = Convert.ToInt32(dr[0]);
            
            int cmd = 0;
            foreach (var item in Enum.GetValues(typeof(CommandEnum)))
            {
                if(item.ToString()==selectedCmd)
                {
                    cmd = Convert.ToInt32(item);
                }
            }

            string query = "insert into playlist (PID, command,types,TimePicker) values(" + PIDValue + "," + cmd + ",2,'" + DateTime + "')";
            Global_Log.connectionClass.insertData(query);
            { }
            MessageBox.Show("Command Updated");
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Global_Log.connectionClass == null)
            {
                Global_Log.connectionClass = new ConnectionClass();
            }
            { }
            string query = "select * from playlists";
            DataTable dt = Global_Log.connectionClass.retriveData(query, "playlists");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                { }
                Playlist.Items.Add(dr.ItemArray[1].ToString());
            }

            foreach (var item in Enum.GetValues(typeof(CommandEnum)))
            {
                Commands.Items.Add(item);
            }

            DateTimePicker.Text = DateTime.Now.ToString();
        }
    }
    public enum CommandEnum
    {
        WAIT = 1,
        DUMPFADE,
        HITMARKER,
        SOFTMARKER,
        LOAD_PLAYLIST
    }
}
