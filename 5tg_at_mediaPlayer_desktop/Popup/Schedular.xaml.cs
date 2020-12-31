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

namespace _5tg_at_mediaPlayer_desktop.Popup
{
    /// <summary>
    /// Interaction logic for Schedular.xaml
    /// </summary>
    public partial class Schedular : Window
    {
        public Schedular()
        {
            InitializeComponent();
        }

        private void DateTme_Click(object sender, RoutedEventArgs e)
        {           
            String sch = DateTimePicker1.Value.ToString();   //("yyyy-MM-dd HH:mm:ss");           
            string query = "update playlists set Schedule = '" + sch + "' where PID = " + Global_Log.pID + " and name = '" + Global_Log.playlistName + "'";
            Global_Log.connectionClass.insertData(query);

            MessageBox.Show("Schedule Successfully Updated");
            this.Close();
        }
    }
}
