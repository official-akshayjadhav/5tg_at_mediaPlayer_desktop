using ATL;
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Collections.Generic;
using _5tg_at_mediaPlayer_desktop.Popup;
using _5tg_at_mediaPlayer_desktop.Playlist;
using _5tg_at_mediaPlayer_desktop.connection;
using _5tg_at_mediaPlayer_desktop.Bottom_Media_Control;

namespace _5tg_at_mediaPlayer_desktop.All_Songs
{
    /// <summary>
    /// Interaction logic for Center_Playlist.xaml
    /// </summary>
    public enum OrderStatus { None, New, Processing, Shipped, Received };

    public partial class All_Songs : UserControl
    {
        public OrderStatus status;
        public All_Songs()
        {
            InitializeComponent();
            LoadAllSong();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        //public object ConfigurationManager { get; private set; }

        //Track track = null;
        List<Audio> audioList = null;

        private void View_all_playlist_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Track_Metadata addMusic = new Track_Metadata();
            //addMusic.ShowDialog();

            LoadAllSong();

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

        public void LoadAllSong()
        {

            if (Global_Log.playBack == null)
            {
                Global_Log.playBack = new PlayBack();
            }
            List<Audio> audioList = new List<Audio>();
            try
            {
                DataTable dt = Global_Log.playBack.getAllSong();

                int count = dt.Rows.Count;
                Audio info = new Audio();
                for (int i = 0; i < count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    try
                    {
                        audioList.Add(new Audio()
                        {
                            ID = Convert.ToInt32(dr.ItemArray[0]),

                            UID = dr.ItemArray[1].ToString(),
                            Title = dr.ItemArray[2].ToString(),
                            FileName = dr.ItemArray[3].ToString(),
                            Filesize = Convert.ToInt32(dr.ItemArray[4]),
                            Filetype = dr.ItemArray[5].ToString(),
                            Filepath = dr.ItemArray[6].ToString(),
                            Duration = (TimeSpan)dr.ItemArray[7],
                            Track = dr.ItemArray[8].ToString(),
                            Trim_Start = (TimeSpan)dr.ItemArray[9],
                            Trim_End = (TimeSpan)dr.ItemArray[10],

                        });
                    }
                    catch (Exception ex)
                    {
                        Global_Log.EXC_WriteIn_LOGfile(ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            { }

            //playlistnames.ItemsSource = audioList;
            datagrid.ItemsSource = audioList;
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

        

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Create_Playlist createPlaylist = new Create_Playlist();
            createPlaylist.ShowDialog();
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var image = e.AddedItems[0] as ComboBoxItem;
            string currentOperation = image.Content.ToString();
            { }
            var ttp = sender as ComboBox;
            Audio audio = null;
            if (image != null && ttp.Tag is Audio)
            {
                //cartID = (image.Tag as Audio).UID;
                audio = (Audio)ttp.Tag;
            }

            if (audio != null)
            {
                Global_Log.audio = audio;
                if (currentOperation == "Update")
                {
                    Track_Metadata addMusic = new Track_Metadata();
                    //Global_Log.audio = audio;
                    addMusic.updateSong();
                }
                else if (currentOperation == "Delete")
                {
                    Global_Log.playBack.DeleteSong(audio.ID);
                }
                else if (currentOperation == "Add to Playlist")
                {
                    Add_To_Playlist add_To_Playlist = new Add_To_Playlist();
                    add_To_Playlist.ShowDialog();
                }
                else if (currentOperation == "Play Song")
                {
                    if(Global_Log.bottom_Media_Control==null)
                    {
                        Global_Log.bottom_Media_Control = new Bottom_Media_Control.Bottom_Media_Control();
                    }
                    Global_Log.bottom_Media_Control.playSong(Global_Log.audio.Track, Global_Log.audio.Title);

                }

            }
            LoadAllSong();
        }

        private void AddTrack_Click(object sender, RoutedEventArgs e)
        {
            Track_Metadata addMusic = new Track_Metadata();
            addMusic.insertSong();

            LoadAllSong();
        }

        private void AddSong_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Track_Metadata addMusic = new Track_Metadata();
            addMusic.ShowDialog();

            LoadAllSong();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
