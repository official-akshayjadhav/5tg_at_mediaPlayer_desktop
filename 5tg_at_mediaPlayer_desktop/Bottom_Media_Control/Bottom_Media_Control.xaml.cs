using _5tg_at_mediaPlayer_desktop.connection;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;


namespace _5tg_at_mediaPlayer_desktop.Bottom_Media_Control
{
    /// <summary>
    /// Interaction logic for Bottom_Media_Control.xaml
    /// </summary>
    public partial class Bottom_Media_Control : UserControl
    {
        private static MediaPlayer mediaPlayer = new MediaPlayer();
        private bool userIsDraggingSlider = false;

        public void GetURIOfSong(int AID)
        {
            string getfilepath = "Select filepath from Audio where ID =" + AID;
            String songpath = Global_Log.connectionClass.getsongfilepath(getfilepath);

            Uri music = new Uri(songpath);
            mediaPlayer.Open(music);
            song.Text = music.ToString();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            PlayMediaFunction();
        }


        public Bottom_Media_Control()
        {
            InitializeComponent();
            
            {
                //Uri music = new Uri("C:\\Users\\shubh\\Desktop\\part time\\1st.mp3");
                //Uri music = new Uri("D:\\MP3\\07_Rin_Rin_Ringa(256k).mp3");
                //mediaPlayer.Open(music);
                //song.Text = music.ToString();
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();
            }

            {
                /*var mediaInfo = new MediaInfo.DotNetWrapper.MediaInfo();
                mediaInfo.Open("C:\\Users\\shubh\\Desktop\\part time\\1st.mp3");
               // MediaFile uploadedFile = new MediaFile("C:\\Users\\jpmcfeely\\Desktop\\Videos\\Quarry.mp4");
               // string duration = uploadedFile.General.DurationString.ToString();
               // String duration = mediaInfo.Option;
                text += "\r\n\r\nInform with Complete=true\r\n";
                mediaInfo.Option("Complete");
                text += mediaInfo.Inform();
                Console.WriteLine(text);
                string text;
                using (var mediaInfo = new MediaInfo())
                {
                    text = mediaInfo.Option("Info_Version");

                    //Information about MediaInfo
                    text += "\r\n\r\nInfo_Parameters\r\n";
                    text += mediaInfo.Option("Info_Parameters");

                    text += "\r\n\r\nInfo_Capacities\r\n";
                    text += mediaInfo.Option("Info_Capacities");

                    text += "\r\n\r\nInfo_Codecs\r\n";
                    text += mediaInfo.Option("Info_Codecs");
                }
                */
                /*String text;
                using (var mediaInfo = new MediaInfo())
                {
                    text = mediaInfo.Option("Info_Version");

                    //Information about MediaInfo
                    text += "\r\n\r\nInfo_Parameters\r\n";
                    text += mediaInfo.Option("Info_Parameters");

                    text += "\r\n\r\nInfo_Capacities\r\n";
                    text += mediaInfo.Option("Info_Capacities");

                    text += "\r\n\r\nInfo_Codecs\r\n";
                    text += mediaInfo.Option("Info_Codecs");
                }

                // An example of how to use the library
                foreach (string filename in new[]
                {
                    "Example.ogg",

                })
                {
                    using (var mediaInfo = new MediaInfo())
                    {
                        text += "\r\n\r\nOpen\r\n";
                        mediaInfo.Open(filename);

                        text += "\r\n\r\nInform with Complete=false\r\n";
                        mediaInfo.Option("Complete");
                        text += mediaInfo.Inform();

                        text += "\r\n\r\nInform with Complete=true\r\n";
                        mediaInfo.Option("Complete", "1");
                        text += mediaInfo.Inform();

                        text += "\r\n\r\nCustom Inform\r\n";
                        mediaInfo.Option("Inform", "General;File size is %FileSize% bytes");
                        text += mediaInfo.Inform();

                        foreach (string param in new[] { "BitRate", "BitRate/String", "BitRate_Mode" })
                        {
                            text += "\r\n\r\nGet with Stream=Audio and Parameter='" + param + "'\r\n";
                            text += mediaInfo.Get(StreamKind.Audio, 0, param);
                        }

                        text += "\r\n\r\nGet with Stream=General and Parameter=46\r\n";
                        text += mediaInfo.Get(StreamKind.General, 0, 46);

                        text += "\r\n\r\nCount_Get with StreamKind=Stream_Audio\r\n";
                        text += mediaInfo.CountGet(StreamKind.Audio);

                        text += "\r\n\r\nGet with Stream=General and Parameter='AudioCount'\r\n";
                        text += mediaInfo.Get(StreamKind.General, 0, "AudioCount");

                        text += "\r\n\r\nGet with Stream=Audio and Parameter='StreamCount'\r\n";
                        text += mediaInfo.Get(StreamKind.Audio, 0, "StreamCount");
                    }

                    Console.WriteLine(text);
                }*/
            }
        }
        public Bottom_Media_Control(String AID) 
        {
            InitializeComponent();
            if (AID != null)
            {
                //AID = Convert.ToInt32(AID);

                string getfilepath = "Select filepath from Audio where ID =" + AID;
                String songpath = Global_Log.connectionClass.getsongfilepath(getfilepath);

                Uri music = new Uri(songpath);
                mediaPlayer.Open(music);
                //song.Text = music.ToString();
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();
            }
            //InitializePropertyValues();
        }
        public void PlayMediaFunction() {
            mediaPlayer.Play();
            InitializePropertyValues();
        }

        public void playSong(string trakString, string title)
        {
            song.Text = title;
            string path = "D:\\song.mp3";
            Uri musicPath = new Uri("D:\\song.mp3");
            byte[] songByte = Convert.FromBase64String(trakString);
            mediaPlayer.Stop();
            
            File.WriteAllBytes(path, songByte);
            LoadSong(musicPath);
        }

        private void LoadSong(Uri musicPath)
        {
            mediaPlayer.Open(musicPath);
            mediaPlayer.Stop();
            mediaPlayer.Position = new TimeSpan(0, 1, 5);

            MediaTimeline timeline = new MediaTimeline((new TimeSpan(0, 1, 5)), (new Duration(new TimeSpan(0, 0, 10))));
            //System.Windows.Media.MediaClock clock = timeline.CreateClock();           
            //mediaPlayer.Clock = clock;
            
            mediaPlayer.Play();

            mediaPlayer.Volume = 0.3;//// (double)volumeSlider.Value;
            mediaPlayer.SpeedRatio = 1;// (double)speedRatioSlider.Value;
        }

        //play the media

        /*private void PlayMedia_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Play();
            InitializePropertyValues();
        }
        */
        /*private void PlayMedia_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //mediaPlayer.Pause();
        }
        */


        /*private void PauseMedia_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void StopMedia_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Stop();
        }
        */
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = (double)volumeSlider.Value;
        }

        private void SpeedRatioSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.SpeedRatio = (double)speedRatioSlider.Value;
        }

        private void TimelineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int SliderValue = (int)timelineSlider.Value;
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            mediaPlayer.Position = ts;
        }


        void InitializePropertyValues()
        {
            mediaPlayer.Volume = (double)volumeSlider.Value;
            mediaPlayer.SpeedRatio = (double)speedRatioSlider.Value;
           
        }

        private void MyMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            timelineSlider.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void MyMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
        }

        private void SliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void SliProgress_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mediaPlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void SliProgress_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }
        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mediaPlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mediaPlayer.Source != null) && (mediaPlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mediaPlayer.Position.TotalSeconds;
            }
        }

        /*private void PlayMedia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Play();
            InitializePropertyValues();
        }*/


        private void PlayPreviousMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        public void PlayMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Play();
            InitializePropertyValues();
            
        }

        private void PauseMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void StopMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Stop();
        }

        private void PlayNextMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PlayRepeatMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
    }
}
