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
using System.Windows.Threading;

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
            int i = datagrid.SelectedIndex;
        }

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
                if (dt != null)
                {
                    int count = dt.Rows.Count;
                    if (count != 0)
                    {
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
                                    Chain = dr.ItemArray[8].ToString(),
                                    Track = dr.ItemArray[9].ToString(),
                                    Trim_Start = (TimeSpan)dr.ItemArray[10],
                                    Trim_End = (TimeSpan)dr.ItemArray[11],
                                    Intro = (TimeSpan)dr.ItemArray[12],
                                    EOM = (TimeSpan)dr.ItemArray[13]

                                });
                            }
                            catch (Exception ex)
                            {
                                Global_Log.EXC_WriteIn_LOGfile(ex.StackTrace);
                            }
                        }
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
                    if (Global_Log.bottom_Media_Control == null)
                    {
                        Global_Log.bottom_Media_Control = new Bottom_Media_Control.Bottom_Media_Control();
                    }
                    Global_Log.bottom_Media_Control.playSong(Global_Log.audio.Track, Global_Log.audio.Title,
                        Global_Log.audio.Trim_Start, Global_Log.audio.Trim_End, Global_Log.audio.Intro, Global_Log.audio.EOM);
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

        public static DateTime autoplayTime;
        public static List<PlaylistAudio> autoPlaylist;
        public static DispatcherTimer autoPlay = new DispatcherTimer();

        private void AutoPlay_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string query = "select top 1 PID, name ,Schedule from playlists where Schedule >= GETDATE() order by Schedule";

            DataTable dt = Global_Log.connectionClass.retriveData(query, "playlist");
            { }
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    DataRow dr = dt.Rows[0];
                    { }

                    int currentPlaylistID = Convert.ToInt32(dr.ItemArray[0]);
                    string autoplayName = dr.ItemArray[1].ToString();
                    autoplayTime = (DateTime)dr.ItemArray[2];
                    { }
                    query = "select p.PID,p.AID, a.Title, a.track, a.duration, a.trimIn, a.trimOut from playlist p join audio a on " +
                        "p.AID=a.ID where PID = " + currentPlaylistID + " and AID is not null";
                    { }
                    dt = Global_Log.connectionClass.retriveData(query, "playlist");
                    { }
                    if (dt != null)
                    {
                        autoPlaylist = new List<PlaylistAudio>();

                        int count = dt.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            dr = dt.Rows[i];
                            try
                            {
                                autoPlaylist.Add(new PlaylistAudio()
                                {
                                    PID = Convert.ToInt32(dr.ItemArray[0]),
                                    AID = Convert.ToInt32(dr.ItemArray[0]),
                                    Name = dr.ItemArray[2].ToString(),
                                    track = dr.ItemArray[3].ToString(),
                                    Duration = (TimeSpan)dr.ItemArray[4],
                                    Trim_Start = (TimeSpan)dr.ItemArray[5],
                                    Trim_End = (TimeSpan)dr.ItemArray[6],
                                    AirTime = "",
                                    SortId = 0
                                });
                            }
                            catch (Exception ex)
                            { }
                        }
                    }
                    
                    autoPlay.IsEnabled = true;
                    autoPlay.Interval = TimeSpan.FromMilliseconds(1);
                    autoPlay.Tick += timer_Tick;
                    autoPlay.Start();


                    MessageBox.Show(autoplayName + " Records is Schedule");
                }
                else
                {
                    MessageBox.Show("No Schedule Records");
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime currentDateTime = DateTime.Now;
            //Console.WriteLine(currentDateTime.ToString() + " = " + autoplayTime.ToString());
            string curTime = currentDateTime.ToString();
            string AutoTime = autoplayTime.ToString();

            if (curTime == AutoTime)
            {
                autoPlay.IsEnabled = false;

                if (Global_Log.fM_Custom == null)
                {
                    Global_Log.fM_Custom = new FM.FM_Custom();
                }
                Global_Log.fM_Custom.loadPlaylistSongs(autoPlaylist);
                if (Global_Log.bottom_Media_Control == null)
                {
                    Global_Log.bottom_Media_Control = new Bottom_Media_Control.Bottom_Media_Control();
                }
                Global_Log.bottom_Media_Control.AutoPlaySong(autoPlaylist);
            }
        }

    }
}
