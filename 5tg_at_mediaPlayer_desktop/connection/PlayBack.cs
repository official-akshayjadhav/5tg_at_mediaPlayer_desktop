using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5tg_at_mediaPlayer_desktop.connection
{
    public class PlayBack
    {
        string currentSong = "";
        int getStatus = 0;
        public ConnectionClass connectionClass = new ConnectionClass();

        //queryStatus=true => Insert The Song ... 
        //else Update the song..

        public void insertSong(Audio audio, bool queryStatus)
        {
            try
            {
                if (queryStatus)
                {
                    currentSong = "insert into Audio (UID, Title, fileName, filesize, fileType, filepath," +
                        " duration, track, trimStart, trimEnd)values(" +
                        "'" + audio.UID + "','" + audio.Title + "','" + audio.FileName + "'," + audio.Filesize + "," +
                        "'" + audio.Filetype + "','" + audio.Filepath + "','" + audio.Duration + "','" + audio.Track + "" +
                        "','" + audio.Trim_Start + "','" + audio.Trim_End + "')";
                    { }
                    getStatus = connectionClass.insertData(currentSong);
                }
                else
                {
                    //currentSong = "update Audio set title='" + audio.Title + "',Trim_Start='" + audio.Trim_Start + "'" +
                    //    ",Trim_End='" + audio.Trim_End + "' where ID=" + audio.ID;
                    currentSong = "update Audio set Title='" + audio.Title + "',trimStart='" + audio.Trim_Start + "'" +
                        ",trimEnd='" + audio.Trim_End + "' where ID=" + audio.ID;
                    getStatus = connectionClass.insertData(currentSong);
                }
            }
            catch (Exception ex)
            {
                Global_Log.EXC_WriteIn_LOGfile(ex.StackTrace);
            }
        }

        public void DeleteSong(int ID)
        {
            try
            {
                currentSong = "delete from Audio where ID=" + ID;
                getStatus = connectionClass.insertData(currentSong);
            }
            catch (Exception ex)
            {
                Global_Log.EXC_WriteIn_LOGfile(ex.StackTrace);
            }
        }


        public DataTable getAllSong()
        {
            DataTable dt = null;
            try
            {
                dt = connectionClass.retriveData("select * from Audio", "Audio");
                { }
            }
            catch (Exception ex)
            {
                Global_Log.EXC_WriteIn_LOGfile(ex.StackTrace);
            }
            return dt;
        }
    }
}
