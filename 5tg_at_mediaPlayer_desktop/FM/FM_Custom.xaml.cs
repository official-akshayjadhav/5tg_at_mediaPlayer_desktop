using _5tg_at_mediaPlayer_desktop.connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _5tg_at_mediaPlayer_desktop.FM
{
    /// <summary>
    /// Interaction logic for FM_Custom.xaml
    /// </summary>
    public partial class FM_Custom : Window
    {
        public FM_Custom()
        {
            InitializeComponent();
            //Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 6 });
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Global_Log.onlyForStart)
            {
                AutoPlay_PreviewMouseLeftButtonDown();
                Global_Log.onlyForStart = false;
            }
        }

        public static DateTime autoplayTime;
        public static List<PlaylistAudio> autoPlaylist;
        public static DispatcherTimer autoPlay = new DispatcherTimer();

        public void AutoPlay_PreviewMouseLeftButtonDown()
        {
            string query = "select top 1 PID, name ,Schedule from playlists where Schedule >= GETDATE() order by Schedule";

            DataTable dt = Global_Log.connectionClass.retriveData(query, "playlist");

            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    DataRow dr = dt.Rows[0];

                    int currentPlaylistID = Convert.ToInt32(dr.ItemArray[0]);
                    string autoplayName = dr.ItemArray[1].ToString();
                    autoplayTime = (DateTime)dr.ItemArray[2];

                    query = "select p.PID,p.AID, a.Title, a.track, a.duration, a.trimIn, a.trimOut from playlist p join audio a on " +
                        "p.AID=a.ID where PID = " + currentPlaylistID + " and AID is not null";

                    dt = Global_Log.connectionClass.retriveData(query, "playlist");

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

            string format = "mm:ss";
            //String time = (autoplayTime - currentDateTime).ToString();


            TimeSpan time = autoplayTime.Subtract(currentDateTime);
            { }
            string gTime = "";
            TimeSpan t1 = new TimeSpan(1, 0, 0);
            if (time > t1)
            {//For Hour
                gTime = string.Format("{00}:{1:00}", (int)time.Hours, time.Minutes);
            }
            else
            {//For Minute
                gTime = string.Format("{0}:{1:00}", (int)time.TotalMinutes, time.Seconds);
            }

            RemainingTime.Text = gTime;
            { }

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


        public void loadProgressBar(int i, int j)
        {
            if (i <= 100)
            {
                progress_value.Text = i.ToString() + " %";
                circulare_ProgressBar.Value = j;

                if (i >= 0 && i <= 10)
                { circulare_ProgressBar.IndicatorBrush = Brushes.Yellow; }
                else if (i >= 10 && i <= 90)
                { circulare_ProgressBar.IndicatorBrush = Brushes.Green; }
                else if (i >= 90 && i <= 100)
                { circulare_ProgressBar.IndicatorBrush = Brushes.Red; }

            }
        }

        public void loadPlaylistSongs(List<connection.PlaylistAudio> autoPlaylist)
        {
            if (autoPlaylist != null)
            {

                List<connection.PlaylistAudio> autoPlaylistData = new List<connection.PlaylistAudio>();
                int i = 0;
                foreach (connection.PlaylistAudio ap in autoPlaylist)
                {
                    autoPlaylistData.Add(new connection.PlaylistAudio()
                    {
                        PID = ap.PID,
                        AID = ap.AID,
                        Name = ap.Name,
                        track = ap.track,
                        Duration = ap.Duration,
                        Trim_Start = ap.Trim_Start,
                        Trim_End = ap.Trim_End,
                        AirTime = ap.AirTime,
                        SortId = i
                    });

                    i++;
                }
                { }
                loadPlaylistSong.ItemsSource = null;
                loadPlaylistSong.Items.Clear();
                loadPlaylistSong.ItemsSource = autoPlaylistData;
            }
        }

        private void Live_assist_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


        private void play_Click(object sender, RoutedEventArgs e)
        {
            checkObj();
            Global_Log.bottom_Media_Control.playSong();
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            checkObj();
            Global_Log.bottom_Media_Control.pauseSong();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            checkObj();
            Global_Log.bottom_Media_Control.stopSong();
        }

        private void checkObj()
        {
            if (Global_Log.bottom_Media_Control == null)
            {
                Global_Log.bottom_Media_Control = new Bottom_Media_Control.Bottom_Media_Control();
            }
        }

    }
}
