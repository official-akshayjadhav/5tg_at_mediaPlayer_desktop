using _5tg_at_mediaPlayer_desktop.connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using _5tg_at_mediaPlayer_desktop.Popup;
using System.Windows.Input;
using _5tg_at_mediaPlayer_desktop.All_Songs;

namespace _5tg_at_mediaPlayer_desktop.Playlist
{
    /// <summary>
    /// Interaction logic for Playlist.xaml
    /// </summary>
    public partial class Playlist : UserControl
    {
        public OrderStatus status;

        public Playlist()
        {
            InitializeComponent();
            loadPlaylist();
            LoadAllSong();
        }

        public void loadPlaylist()
        {
            if (Global_Log.playBack == null)
            {
                Global_Log.playBack = new PlayBack();
            }

            List<Playlists> list_Playlists = new List<Playlists>();
            try
            {
                if (Global_Log.connectionClass == null)
                {
                    Global_Log.connectionClass = new ConnectionClass();
                }
                DataTable dt = Global_Log.connectionClass.retriveData("select PID,name,CAST(createdDate as date), TotalSong from playlists", "playlists");

                int count = dt.Rows.Count;
                Playlists playlists = new Playlists();
                for (int i = 0; i < count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    try
                    {
                        list_Playlists.Add(new Playlists()
                        {
                            PID = Convert.ToInt32(dr.ItemArray[0]),
                            Name = dr.ItemArray[1].ToString(),
                            Date = dr.ItemArray[2].ToString(),
                            TotalSong = Convert.ToInt32(dr.ItemArray[3])
                        });
                    }
                    catch (Exception ex)
                    { }
                }
                if (allPlaylist != null)
                    allPlaylist.ItemsSource = list_Playlists;
            }
            catch (Exception ex)
            { }
        }
        public static int currentPlaylist = 0;
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var image = e.AddedItems[0] as ComboBoxItem;
            string currentOperation = image.Content.ToString();

            var ttp = sender as ComboBox;
            Playlists playlists = null;
            if (image != null && ttp.Tag is Playlists)
            {
                playlists = (Playlists)ttp.Tag;
            }

            if (playlists != null)
            {
                Create_Playlist create_Playlist = new Create_Playlist();
                Global_Log.pID = playlists.PID;
                Global_Log.playlistName = playlists.Name;

                if (currentOperation == "Update")
                {
                    create_Playlist.SetProperty(playlists.Name.ToString());
                }
                else if (currentOperation == "Delete")
                {
                    create_Playlist.deletePlaylist(playlists.PID);
                }
                else if (currentOperation == "View")
                {
                    currentPlaylist = playlists.PID;

                    loadPlaylistSong(Global_Log.pID, playlists.SortId);
                }
                else if (currentOperation == "Schedule")
                {
                    currentPlaylist = playlists.PID;
                    Schedular schedular = new Schedular();
                    schedular.ShowDialog();
                    //loadPlaylistSong(Global_Log.pID, playlists.SortId);
                }
            }
            loadPlaylist();
        }

        private void loadPlaylistSong(int playlistID, int Sorts, bool isSort = false)
        {
            List<PlaylistAudio> playlistAudio = new List<PlaylistAudio>();
            string query = "";
            if (isSort)
            {
                ///query = "select ID, title, duration, track from Audio where ID in(select AID from playlist where PID = " + playlistID + ")";
                //query = "select a.ID, a.title, a.duration, a.track from Audio a inner join playlist p on a.ID=p.AID where p.PID =" + playlistID;
                query = "select a.ID, ps.Schedule, a.title, a.duration, a.track from Audio a inner join playlist p on " +
                    "a.ID=p.AID inner join playlists ps on ps.PID = p.PID where p.PID =" + playlistID;

            }
            else
            {
                //query = "select ID, title, duration, track from Audio where ID in(select AID from playlist where PID = " + playlistID + ")";
                //query = "select a.ID, a.title, a.duration, a.track from Audio a inner join playlist p on a.ID=p.AID where p.PID =" + playlistID;

                query = "select a.ID, ps.Schedule, a.title, a.duration, a.track from Audio a inner join playlist p on " +
                    "a.ID=p.AID inner join playlists ps on ps.PID = p.PID where p.PID =" + playlistID;

            }
            DataTable dt = Global_Log.connectionClass.retriveData(query, "Audio");

            int count = dt.Rows.Count;
            Playlists playlists = new Playlists();
            string time = "";
            if (dt != null)
            {
                time = dt.Rows[0][1].ToString();                               
            }
            for (int i = 0; i < count; i++)
            {
                DataRow dr = dt.Rows[i];
                try
                {
                    DateTime date1 = (DateTime)dr[1];
                    TimeSpan time1 = (TimeSpan)dr[3];
                    
                    playlistAudio.Add(new PlaylistAudio()
                    {
                        AID = Convert.ToInt32(dr[0]),
                        AirTime = time,
                        PID = playlistID,
                        SortId = Sorts,
                        Name = dr[2].ToString(),
                        track = dr[4].ToString(),
                        Duration = (TimeSpan)dr[3]
                    });

                    DateTime combined = date1.Add(time1);
                    time = combined.ToString();
                }
                catch (Exception ex)
                { }
            }
            PlaylistSong.ItemsSource = null;
            PlaylistSong.Items.Clear();
            PlaylistSong.ItemsSource = playlistAudio;
        }

        private void PlaylistSongEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var image = e.AddedItems[0] as ComboBoxItem;
            string currentOperation = image.Content.ToString();

            var ttp = sender as ComboBox;
            PlaylistAudio PlaylistAudio = null;
            if (image != null && ttp.Tag is PlaylistAudio)
            {
                PlaylistAudio = (PlaylistAudio)ttp.Tag;
            }

            if (PlaylistAudio != null)
            {
                Create_Playlist create_Playlist = new Create_Playlist();
                Global_Log.pID = PlaylistAudio.PID;
                Global_Log.playlistName = PlaylistAudio.Name;

                if (currentOperation == "Play")
                {
                    if (Global_Log.bottom_Media_Control == null)
                    {
                        Global_Log.bottom_Media_Control = new Bottom_Media_Control.Bottom_Media_Control();
                    }
                    Global_Log.bottom_Media_Control.playSong(PlaylistAudio.track, PlaylistAudio.Name);
                }
                else if (currentOperation == "Delete")
                {
                    string query = "delete playlist where PID =" + PlaylistAudio.PID + "and AID = " + PlaylistAudio.AID;
                    Global_Log.connectionClass.insertData(query);
                }
                else if (currentOperation == "Update Sort ID")
                {
                    Global_Log.playlistAudio = PlaylistAudio;

                    Sorting sorting = new Sorting();
                    sorting.ShowDialog();
                }
            }
            loadPlaylistSong(PlaylistAudio.PID, PlaylistAudio.SortId);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Create_Playlist createPlaylist = new Create_Playlist();
            createPlaylist.ShowDialog();
            loadPlaylist();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            //string query = " select distinct A.Title, ps.name from playlist p inner join Audio A on p.AID = A.ID," +
            //    " playlist p1 inner join playlists ps on ps.PID = p1.PID where ps.PID = " + currentPlaylist;

            string query = " select p.ID,p.AID,A.Title, p.PID, PS.name FROM  " +
                "playlist p inner join Audio A on p.AID = A.ID inner join playlists pS on ps.PID = P.PID WHERE P.PID =" + currentPlaylist;

            DataTable dt = Global_Log.connectionClass.retriveData(query, "playlist");
            List<AudioIE> writeCsvData = new List<AudioIE>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                try
                {
                    writeCsvData.Add(new AudioIE()
                    {
                        ID = Convert.ToInt32(dr[0]),
                        AID = Convert.ToInt32(dr[1]),
                        AName = dr[2].ToString(),
                        PID = Convert.ToInt32(dr[3]),
                        PName = dr[4].ToString()
                    });
                }
                catch (Exception ex)
                { }
            }

            string strFilePath = Global_Log.ActiveDir + "\\playlist.csv";
            string strSeperator = ",";
            StringBuilder sbOutput = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string s1 = writeCsvData[i].ID.ToString();
                string s2 = writeCsvData[i].AID.ToString();
                string s3 = writeCsvData[i].AName.ToString();
                string s4 = writeCsvData[i].PID.ToString();
                string s5 = writeCsvData[i].PName.ToString();

                String s = s1 + "," + s2 + "," + s3 + "," + s4 + "," + s5 + Environment.NewLine;
                File.AppendAllText(strFilePath, s);
            }
            MessageBox.Show("All Songs Export");
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            string strFilePath = Global_Log.ActiveDir + "\\playlist.csv";

            List<AudioIE> writeCsvData = new List<AudioIE>();
            using (TextFieldParser csvReader = new TextFieldParser(strFilePath))
            {
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                csvReader.ReadLine();

                while (!csvReader.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvReader.ReadFields();

                    writeCsvData.Add(new AudioIE()
                    {
                        ID = Convert.ToInt32(fields[0]),
                        AID = Convert.ToInt32(fields[1]),
                        AName = fields[2],
                        PID = Convert.ToInt32(fields[3]),
                        PName = fields[4],
                    });
                }
            }
        }
        private void ComboSorts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var image = e.AddedItems[0] as ComboBoxItem;
            string currentOperation = image.Content.ToString();

            var ttp = sender as ComboBox;
            //Sort ID
            { }



            //loadPlaylistSong(int playlistID, int Sorts, true)
        }




        /// All Songs  datagrid Code
        /// 
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
                            //Track = dr.ItemArray[8].ToString(),
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
    }
}
