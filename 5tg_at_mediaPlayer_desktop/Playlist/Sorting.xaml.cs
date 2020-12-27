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
    /// Interaction logic for Sorting.xaml
    /// </summary>
    public partial class Sorting : Window
    {
        public Sorting()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtSortNumber.Text = Convert.ToString(Global_Log.playlistAudio.SortId);
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int SortValue = Convert.ToInt32(txtSortNumber.Text);
            setSortID(SortValue);
            this.Close();
        }

        public void setSortID(int SortValue)
        {
            //Global_Log.playlistAudio.SortId;
            string update_1 = "update Playlist set SortID = " + SortValue + " where pid = " + Global_Log.playlistAudio.PID + " and Aid = " + Global_Log.playlistAudio.AID;
            string update_2 = "update Playlist set SortID = SortID + 1 where SortID<1000 and SortID>= 1";

            Global_Log.connectionClass.insertData(update_1);
            Global_Log.connectionClass.insertData(update_2);
            Global_Log.connectionClass.insertData(update_1);

        }
    }
}
