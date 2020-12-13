using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace _5tg_at_mediaPlayer_desktop.Center_Playlist
{
    /// <summary>
    /// Interaction logic for Center_Playlist.xaml
    /// </summary>
    public partial class Center_Playlist : UserControl
    {

        public Center_Playlist()
        {
            InitializeComponent();
        }

        //public object ConfigurationManager { get; private set; }

        private void View_all_playlist_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /*
             * //SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\MiniProject\MiniProject\MiniProject\Persons.mdf;Integrated Security=True");
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\shubh\Documents\Visual Studio 2017\Projects\5tg_at_mediaPlayer_desktop\5tg_at_mediaPlayer_desktop\Database1.mdf;Integrated Security=True;");
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                string CmdString = "SELECT * FROM Playlist_Name";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable("Playlist_Name");
                sda.Fill(dt);
                playlistnames.ItemsSource = dt.DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }*/
        }

        private void Playlistnames_LoadingRowDetails(object sender, DataGridRowDetailsEventArgs e)
        {
            /*DataTable dt = new DataTable();
            dt.Columns.Add("Sr. No.", typeof(String));
            dt.Columns.Add("ID", typeof(String));
            dt.Columns.Add("Name", typeof(String));
            dt.Columns.Add("Count",typeof(String));
            dt.Columns.Add("Controls",typeof(String));
            dt.Rows.Add("1","PLS1","PLNAME","15","-");
            dt.Rows.Add("2", "PLS1", "PLNAME", "15", "-");
            //Dictionary<string, string, string, string, string> dictMapping = new Dictionary<>();
            playlistnames.ItemsSource = dt.DefaultView;
            foreach (DataGridViewColumn col in playlistnames.Columns)
            {
            }*/

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Show_Click(object sender, RoutedEventArgs e) {
            MyPopup.IsOpen = true;
        }
        private void Hide_Click(object sender, RoutedEventArgs e) {
            MyPopup.IsOpen = false;
        }
    }
}