using _5tg_at_mediaPlayer_desktop.connection;
using _5tg_at_mediaPlayer_desktop.Bottom_Media_Control;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using CsvHelper;

namespace _5tg_at_mediaPlayer_desktop.Playlist
{
    /// <summary>
    /// Interaction logic for Playlist.xaml
    /// </summary>
    public partial class Playlist : UserControl
    {
        public Playlist()
        {
            InitializeComponent();

            loadPlaylist();
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
            }
            loadPlaylist();
        }

        private void loadPlaylistSong(int playlistID, int Sorts, bool isSort = false)
        {
            List<PlaylistAudio> playlistAudio = new List<PlaylistAudio>();
            string query = "";
            if (isSort)
            {
                query = "select ID, title, duration, track from Audio where ID in(select AID from playlist where PID = " + playlistID + ")";
            }
            else
            {
                query = "select ID, title, duration, track from Audio where ID in(select AID from playlist where PID = " + playlistID + ")";
            }
            DataTable dt = Global_Log.connectionClass.retriveData(query, "Audio");

            int count = dt.Rows.Count;
            Playlists playlists = new Playlists();
            for (int i = 0; i < count; i++)
            {
                DataRow dr = dt.Rows[i];
                try
                {
                    playlistAudio.Add(new PlaylistAudio()
                    {
                        AID = Convert.ToInt32(dr[0]),
                        PID = playlistID,
                        SortId = Sorts,
                        Name = dr[1].ToString(),
                        track = dr[3].ToString(),
                        Duration = (TimeSpan)dr[2]
                    });
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
            { }
            if (PlaylistAudio != null)
            {
                Create_Playlist create_Playlist = new Create_Playlist();
                Global_Log.pID = PlaylistAudio.PID;
                Global_Log.playlistName = PlaylistAudio.Name;

                if (currentOperation == "Play")
                {
                    //create_Playlist.SetProperty(playlists.Name.ToString());
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
    }
}
