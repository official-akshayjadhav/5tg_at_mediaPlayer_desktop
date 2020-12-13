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
using _5tg_at_mediaPlayer_desktop.connection;

namespace _5tg_at_mediaPlayer_desktop
{
    /// <summary>
    /// Interaction logic for Add_Playlist.xaml
    /// </summary>
    public partial class Add_Playlist : Window
    {       
        public Add_Playlist()
        {
            InitializeComponent();
        }

        public static int i = 0;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //string UID = i.ToString();
                string query = "";
                //query = "insert into PlayLists(PlayListName,IsActive) values('" + txtPlaylist.Text + "',1)";
                query = "insert into Playlist(UID,Title) values('" + Global_Log.cartId + "', '" + txtPlaylist.Text + "')";
                Global_Log.connectionClass.insertData(query);
                i++;
            }
            catch (Exception ex)
            {
                Global_Log.EXC_WriteIn_LOGfile(ex.StackTrace);
            }
            MessageBox.Show(this, "Playlist Created..!", "Playlist");
            this.Close();
        }


    }
}
