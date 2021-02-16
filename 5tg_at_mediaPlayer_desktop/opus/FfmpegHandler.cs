using System;
using System.IO;
using MediaToolkit;
using MediaToolkit.Model;
using System.Diagnostics;
using _5tg_at_mediaPlayer_desktop.connection;

namespace _5tg_at_mediaPlayer_desktop
{
    public class FfmpegHandler
    {
        public static bool ExecuteFFMpeg(string arguments)
        {
            try
            {
                Process process = Process.Start("cmd.exe", $@"\k E:\Amol Sir\31122020 - Copy\5tg_at_mediaPlayer_desktop\5tg_at_mediaPlayer_desktop\opus\ffmpeg.exe {arguments}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return true;
        }

        public static string convertFile2(ConvertFileDetails convertFileDetails, int id)
        {
            try
            {
                using (var engine = new Engine())
                {

                    var inputFile = new MediaFile { Filename = convertFileDetails.InputFilePath };
                    var outputFile = new MediaFile { Filename = convertFileDetails.OutputFilePath };
                    engine.Convert(inputFile, outputFile);
                    { }
                    //AudioTestDBEntities db = new AudioTestDBEntities();
                    //var uploadAudio = db.Audios.FirstOrDefault(x => x.ID == id);


                    var result = Process.Start("explorer.exe", "/select, \"" + convertFileDetails.OutputFilePath + "\"");
                    var outputurl = "/";
                    { }
                    byte[] fileDataBytes = File.ReadAllBytes(convertFileDetails.OutputFilePath);
                    String fileDataBase64 = Convert.ToBase64String(fileDataBytes);

                    var outputpath = Path.GetFileName(convertFileDetails.OutputFilePath);
                    var title = Path.GetFileNameWithoutExtension(convertFileDetails.OutputFilePath);

                    var outputtype = Path.GetExtension(convertFileDetails.OutputFilePath);
                    //if (uploadAudio.ID > 0)
                    //{
                    //    uploadAudio.FileType = outputtype;
                    //    uploadAudio.Title = title;
                    //    uploadAudio.FilePath = outputurl+outputpath;
                    //    db.SaveChanges();
                    //}
                    //return "Success";

                    return fileDataBase64;
                }
            }
            catch (Exception ex)
            {
                var message = "nothing" + ex.Message;
                return "error";
            }
        }

        public static string ConvertFile(ConvertFileDetails convertFileDetails,
            EventHandler<ConvertProgressEventArgs> convertProgressEvent, EventHandler<ConversionCompleteEventArgs> conversionCompleteEvent)
        {
            try
            {
                using (var engine = new Engine())
                {
                    var inputFile = new MediaFile { Filename = convertFileDetails.InputFilePath };
                    var outputFile = new MediaFile { Filename = convertFileDetails.OutputFilePath };


                    engine.ConvertProgressEvent += convertProgressEvent;
                    engine.ConversionCompleteEvent += conversionCompleteEvent;

                    engine.Convert(inputFile, outputFile);

                    Process.Start("explorer.exe", "/select, \"" + convertFileDetails.OutputFilePath + "\"");

                    return "Success";

                }
            }
            catch
            {
                return "Error";
            }
        }
    }
}
