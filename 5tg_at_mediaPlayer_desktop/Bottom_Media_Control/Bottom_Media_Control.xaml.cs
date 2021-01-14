using _5tg_at_mediaPlayer_desktop.connection;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
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
                string getfilepath = "Select filepath,Title from Audio where ID =" + AID;
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
        public void PlayMediaFunction()
        {
            mediaPlayer.Play();
            InitializePropertyValues();
        }

        public void playSong(string trakString, string title)
        {
            mediaPlayer.Stop();

            string rootFolder = @"D:\";
            string authorsFile = "song.mp3";



            if (File.Exists(Path.Combine(rootFolder, authorsFile)))
                File.Delete(Path.Combine(rootFolder, authorsFile));
            { }

            song.Text = title;
            string path = "D:\\song.mp3";
            Uri musicPath = new Uri("D:\\song.mp3");
            //String path = "C:\\Users\\shubh\\Desktop\\part time\\1st.mp3";
            //Uri musicPath = new Uri("C:\\Users\\shubh\\Desktop\\part time\\1st.mp3");
            byte[] songByte = Convert.FromBase64String(trakString);
            File.Delete(path);
            { }


            File.WriteAllBytes(path, songByte);
            LoadSong(musicPath, path);
        }

        public void LoadSong(Uri musicPath, string path)
        {
            mediaPlayer.Stop();
            mediaPlayer.Close();
            //mediaPlayer.Open(musicPath);
            //mediaPlayer.Position = new TimeSpan(0, 1, 50);
            TimeSpan startTime = new TimeSpan(0, 1, 50);
            TimeSpan endTime = new TimeSpan(0, 2, 10);
            { }
            string path1 = "D:\\trimSong.mp3";

            Uri musicPath1 = new Uri("D:\\trimSong.mp3");

            //TrimWavFile(path, path1, startTime, endTime);
            TrimMp3(path, path1, startTime, endTime);
            { }

            mediaPlayer.Open(musicPath);
            mediaPlayer.Play();
            TimeSpan trimTime = endTime.Subtract(startTime);
            //Thread.Sleep(trimTime);
            mediaPlayer.Stop();
            { }
            MediaTimeline timeline = new MediaTimeline((new TimeSpan(0, 1, 5)), (new Duration(new TimeSpan(0, 0, 10))));
            //System.Windows.Media.MediaClock clock = timeline.CreateClock();           
            //mediaPlayer.Clock = clock;

            mediaPlayer.Play();
            { }
            mediaPlayer.Volume = 1;//// (double)volumeSlider.Value;
            mediaPlayer.SpeedRatio = 1;// (double)speedRatioSlider.Value;
        }

        #region Try to trim

        public void TrimMp3(string inputPath, string outputPath, TimeSpan? begin, TimeSpan? end)
        {
            if (begin.HasValue && end.HasValue && begin > end)
                throw new ArgumentOutOfRangeException("end", "end should be greater than begin");

            using (var reader = new Mp3FileReader(inputPath))
            using (var writer = File.Create(outputPath))
            {
                Mp3Frame frame;
                while ((frame = reader.ReadNextFrame()) != null)
                    if (reader.CurrentTime >= begin || !begin.HasValue)
                    {
                        if (reader.CurrentTime <= end || !end.HasValue)
                            writer.Write(frame.RawData, 0, frame.RawData.Length);
                        else break;
                    }
            }
        }
        List<PlaylistAudio> currentPlayList { get; set; }
        int currentPlayListIndex { get; set; }

        internal void AutoPlaySong(List<PlaylistAudio> autoPlaylist)
        {
            if (autoPlaylist.Count > 0) {
                currentPlayList = autoPlaylist;
                mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
                currentPlayListIndex = 0;
                PlaylistAudio song1 = currentPlayList[currentPlayListIndex];
                playSong(song1.track, song1.Name);
                //mediaPlayer.MediaEnded
            } }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            if (currentPlayList.Count > currentPlayListIndex)
            {
                currentPlayListIndex++;
                PlaylistAudio song1 = currentPlayList[currentPlayListIndex];
                playSong(song1.track, song1.Name);
            }
            else
            {
                mediaPlayer.MediaEnded -= MediaPlayer_MediaEnded;
            }

        }

        //public void T+3625149+rimWavFile(string inPath, string outPath, TimeSpan cutFromStart, TimeSpan cutFromEnd)
        //{
        //    using (WaveFileReader reader = new WaveFileReader(inPath))
        //    {
        //        using (WaveFileWriter writer = new WaveFileWriter(outPath, reader.WaveFormat))
        //        {
        //            int bytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000;

        //            int startPos = (int)cutFromStart.TotalMilliseconds * bytesPerMillisecond;
        //            startPos = startPos - startPos % reader.WaveFormat.BlockAlign;

        //            int endBytes = (int)cutFromEnd.TotalMilliseconds * bytesPerMillisecond;
        //            endBytes = endBytes - endBytes % reader.WaveFormat.BlockAlign;
        //            int endPos = (int)reader.Length - endBytes;

        //            TrimWavFile(reader, writer, startPos, endPos);
        //        }
        //    }
        //}

        //private void TrimWavFile(WaveFileReader reader, WaveFileWriter writer, int startPos, int endPos)
        //{
        //    reader.Position = startPos;
        //    byte[] buffer = new byte[1024];
        //    while (reader.Position < endPos)
        //    {
        //        int bytesRequired = (int)(endPos - reader.Position);
        //        if (bytesRequired > 0)
        //        {
        //            int bytesToRead = Math.Min(bytesRequired, buffer.Length);
        //            int bytesRead = reader.Read(buffer, 0, bytesToRead);
        //            if (bytesRead > 0)
        //            {
        //                writer.WriteData(buffer, 0, bytesRead);
        //            }
        //        }
        //    }
        //}

        #endregion


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
            //mediaPlayer.SpeedRatio = (double)speedRatioSlider.Value;
        }

        private void TimelineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //int SliderValue = (int)timelineSlider.Value;
            //TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            //mediaPlayer.Position = ts;
        }


        void InitializePropertyValues()
        {
            //mediaPlayer.Volume = (double)volumeSlider.Value;
            //mediaPlayer.SpeedRatio = (double)speedRatioSlider.Value;

        }

        private void MyMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            //timelineSlider.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
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

        //public event OnNext (object sender);
        private void PlayNextMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Playlist.Playlist playlist = new Playlist.Playlist();
            playlist.nextSong();

            //Global_Log.allSongTrack      = true;
            //Global_Log.playlistSongTrack = true;
        }
        private void PlayPreviousMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Playlist.Playlist playlist = new Playlist.Playlist();
            playlist.previousSong();

            Global_Log.allSongTrack = true;
        }

        private void PlayRepeatMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
    }
}
