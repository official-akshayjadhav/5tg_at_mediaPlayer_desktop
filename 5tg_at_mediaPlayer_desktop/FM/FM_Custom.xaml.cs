using _5tg_at_mediaPlayer_desktop.connection;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

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
            //for (int i = 0; i <= 100; i++)
            //{
            //    circulare_ProgressBar.Value = i;
            //}

            //progress_value.Text = "10";
            //circulare_ProgressBar.Value = 25;
            //loadPlaylistSongs(Global_Log.autoPlaylist);
        }


        public void loadProgressBar(int i, int j)
        {

            if (i <= 100)
            {
                progress_value.Text = i.ToString() + " %";
                circulare_ProgressBar.Value = j;
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
