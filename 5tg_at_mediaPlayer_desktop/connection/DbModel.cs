using _5tg_at_mediaPlayer_desktop.Center_Playlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _5tg_at_mediaPlayer_desktop.Center_Playlist.Center_Playlist;

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
    
}
