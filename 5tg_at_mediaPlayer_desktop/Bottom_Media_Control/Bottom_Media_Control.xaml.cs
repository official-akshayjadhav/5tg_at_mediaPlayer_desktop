﻿using _5tg_at_mediaPlayer_desktop.connection;
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
        //public DispatcherTimer autoPlayTick = null;

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

        public void playSong(string trakString, string title, TimeSpan startTime, TimeSpan endSpan, TimeSpan intro, TimeSpan eOM)
        {
            mediaPlayer.Stop();

            string rootFolder = @"D:\";
            string authorsFile = "song.mp3";

            if (File.Exists(Path.Combine(rootFolder, authorsFile)))
                File.Delete(Path.Combine(rootFolder, authorsFile));

            song.Text = title;
            string trimPath = "D:\\Tsong.mp3";
            string path = "D:\\song.mp3";

            byte[] songByte = Convert.FromBase64String(trakString);
            File.Delete(trimPath);
            File.Delete(path);

            //{
            //    string input = "D:\\Tsong.opus";

            //    File.WriteAllBytes(input, songByte);
            //    { }
            //    string output = "D:\\song.mp3";
            //    ConvertFileDetails convertFileDetails = new ConvertFileDetails();
            //    convertFileDetails.InputFilePath = input;
            //    convertFileDetails.OutputFilePath = output;

            //    string opVal = FfmpegHandler.convertFile2(convertFileDetails, 0);
            //    { }

            //    //songByte = Convert.FromBase64String(trakString);
            //    //File.WriteAllBytes(output, songByte);
            //    { }
            //    Uri uri = new Uri("D:\\song.mp3");
            //    mediaPlayer.Open(uri);
            //    mediaPlayer.Play();
            //    mediaPlayer.Volume = 1;
            //}

            File.WriteAllBytes(trimPath, songByte);
            { }



            //ConvertFileDetails convertFileDetails = new ConvertFileDetails();
            //convertFileDetails.InputFilePath = trimPath;
            //{ }
            //convertFileDetails.OutputFilePath = path;
            //{ }
            //string outputFileName = FfmpegHandler.convertFile2(convertFileDetails, 0);
            //{ }
            //trimPath = "D:\\Tsong.mp3";
            //songByte = Convert.FromBase64String(trakString);
            //File.Delete(trimPath);
            //File.Delete(path);
            //File.WriteAllBytes(trimPath, songByte);

            { }
            TrimMp3(trimPath, path, startTime, endSpan);

            {
                int intr = Convert.ToInt32(eOM.TotalSeconds);
                int startTimes = Convert.ToInt32(startTime.TotalSeconds);
                double val = intr - startTimes;
                startInterval = val / 10;
                if (startInterval == 0)
                { startInterval = 0.1; }
            }

            {

                songDuration = Convert.ToInt32(eOM.TotalSeconds);
                int endTime = Convert.ToInt32(endSpan.TotalSeconds);
                double val = (endTime - songDuration);
                endInterval = val / 10;
            }

            Uri musicPath = new Uri("D:\\song.mp3");
            LoadSong(musicPath, path, intro);


            //autoPlayTick = new DispatcherTimer();
            //autoPlayTick.IsEnabled = true;
            //autoPlayTick.Interval = TimeSpan.FromSeconds(autoplaySongTime_Sec);
            //autoPlayTick.Tick += autoPlay_Timer_Tick;
            //autoPlayTick.Start();
        }

        public static double vol = 0.1;
        public static bool volDown = false;
        public static DispatcherTimer autoVolumeIncrement = new DispatcherTimer();
        public static int songDuration;

        public static double startInterval;
        public static double endInterval;

        public void LoadSong(Uri musicPath, string path, TimeSpan intro)
        {
            mediaPlayer.Stop();
            mediaPlayer.Close();
            mediaPlayer.Open(musicPath);

            mediaPlayer.Volume = 0.1;
            mediaPlayer.Play();
            volDown = false;

            if(startInterval<=0)
            {
                startInterval = 0.1;
            }
            autoVolumeIncrement.IsEnabled = true;
            autoVolumeIncrement.Interval = TimeSpan.FromSeconds(startInterval);
            autoVolumeIncrement.Tick += autoVolumeIncrement_Tick;
            autoVolumeIncrement.Start();

            //mediaPlayer.Volume = 1;//// (double)volumeSlider.Value;
            mediaPlayer.SpeedRatio = 1;// (double)speedRatioSlider.Value;
        }

        private void autoVolumeIncrement_Tick(object sender, EventArgs e)
        {
            if (volDown == false)
            {
                if (mediaPlayer.Volume == 1.0)
                {
                    autoVolumeIncrement.IsEnabled = false;
                }
                else
                {
                    mediaPlayer.Volume += 0.1;
                }
            }
            else
            {
                if (mediaPlayer.Volume == 0)
                {
                    autoVolumeIncrement.IsEnabled = false;
                }
                else
                {
                    mediaPlayer.Volume -= 0.1;
                }
            }
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

        static int val = 0;
        static int val1 = 0;
        public static int i = 0;
        public static double autoplaySongTime_Sec = 0;
        List<PlaylistAudio> currentPlayList { get; set; }
        int currentPlayListIndex { get; set; }

        public static DispatcherTimer autoPlayTick = new DispatcherTimer();

        internal void AutoPlaySong(List<PlaylistAudio> autoPlaylist)
        {
            if (autoPlaylist.Count > 0)
            {
                currentPlayList = autoPlaylist;
                mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
                currentPlayListIndex = 0;
                PlaylistAudio song1 = currentPlayList[currentPlayListIndex];
                autoplaySongTime_Sec = 0;
                getTimeInSec(song1.Duration);

                val = 0;
                val1 = 0;

                playSong(song1.track, song1.Name, song1.Trim_Start, song1.Trim_End, song1.Intro, song1.EOM);

                autoPlayTick.IsEnabled = true;
                autoPlayTick.Interval = TimeSpan.FromSeconds(autoplaySongTime_Sec);
                autoPlayTick.Tick += autoPlay_Timer_Tick;
                autoPlayTick.Start();
            }
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {

            //autoPlayTick.Tick -= autoPlayTick_Ended;

            Global_Log.fM_Custom.loadProgressBar(0, 0);
            Global_Log.isSongPlay = false;

            currentPlayListIndex++;
            if (currentPlayList.Count > currentPlayListIndex)
            {
                //currentPlayListIndex++;
                PlaylistAudio song1 = currentPlayList[currentPlayListIndex];
                autoplaySongTime_Sec = 0;
                getTimeInSec(song1.Duration);
                playSong(song1.track, song1.Name, song1.Trim_Start, song1.Trim_End, song1.Intro, song1.EOM);

                val = 0;
                val1 = 0;

            }
            else
            {
                Global_Log.fM_Custom.loadProgressBar(0, 0);

                //if (Global_Log.fM_Custom == null)
                {
                    Global_Log.fM_Custom = new FM.FM_Custom();
                }
                Global_Log.fM_Custom.AutoPlay_PreviewMouseLeftButtonDown();
            }
        }

        private void autoPlayTick_Ended(object sender, EventArgs e)
        {
            //autoPlayTick.Stop();
            Global_Log.fM_Custom.loadProgressBar(0, 0);
        }

        private void getTimeInSec(TimeSpan duration)
        {
            autoplaySongTime_Sec = duration.TotalSeconds / 100;
        }

        private void autoPlay_Timer_Tick(object sender, EventArgs e)
        {
            if (Global_Log.fM_Custom == null)
            {
                Global_Log.fM_Custom = new FM.FM_Custom();
            }
            val += 2;
            val1 += 1;

            Global_Log.fM_Custom.loadProgressBar(val1, val);
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
            Global_Log.isSongPlay = true;

            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
            int currentTime = Convert.ToInt32((TimeSpan.FromSeconds(sliProgress.Value)).TotalSeconds);

            Global_Log.currentPlayTime = lblProgressStatus.Text.ToString();



            if (songDuration == currentTime)
            {
                volDown = true;
                autoVolumeIncrement.IsEnabled = true;
                autoVolumeIncrement.Interval = TimeSpan.FromSeconds(endInterval);
            }
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

        int check = 0;

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mediaPlayer.Source != null) && (mediaPlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mediaPlayer.Position.TotalSeconds;
            }
        }

        public void playSong()
        {
            mediaPlayer.Play();
            autoPlayTick.IsEnabled = true;
        }

        public void pauseSong()
        {
            mediaPlayer.Pause();
            autoPlayTick.IsEnabled = false;
        }

        public void stopSong()
        {
            mediaPlayer.Stop();
            autoPlayTick.IsEnabled = false;
            val = val1 = 0;
        }

        public void PlayMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Play();
            InitializePropertyValues();
        }

        public void PauseMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Pause();
        }

        public void StopMedia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
