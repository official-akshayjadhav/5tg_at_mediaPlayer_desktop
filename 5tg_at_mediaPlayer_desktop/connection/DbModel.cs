﻿using _5tg_at_mediaPlayer_desktop.All_Songs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5tg_at_mediaPlayer_desktop.connection
{
    class DbModel
    {
    }

    public class Audio
    {
        public int ID { get; set; }
        public string UID { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public int Filesize { get; set; }
        public string Filetype { get; set; }
        public string Filepath { get; set; }
        public TimeSpan Duration { get; set; }
        public string Track { get; set; }
        public TimeSpan Trim_Start { get; set; }
        public TimeSpan Trim_End { get; set; }
        //public List<string> controls { get; set; }
        //public OrderStatus status { get; set; }
    }

    public class Playlists
    {
        public int PID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public int TotalSong { get; set; }
    }

    public class PlaylistData
    {
        public int PID { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }

    public class ConvertFileDetails //For Opus Conversion
    {
        public string InputFilePath { get; set; }
        public string OutputFilePath { get; set; }
    }

    public class PlaylistAudio
    {
        public int AID { get; set; }
        public int PID { get; set; }
        public string Name { get; set; }
        public string track { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
