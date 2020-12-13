using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _5tg_at_mediaPlayer_desktop.connection
{
    public static class Global_Log
    {
        public static MainWindow mainWindow = new MainWindow();
        public static PlayBack playBack = new PlayBack();
        public static ConnectionClass connectionClass = new ConnectionClass();
        public static FileStream LOG_fileStream;
        public static StreamWriter LOGstreamWriter;

        public static string cartId;

        public static string ActiveDir = Directory.GetCurrentDirectory();
        readonly static string LogFolderPath = ActiveDir + "\\MultiMedia\\Log";
        readonly static string LogFileName = "Log_" + DateTime.Now.ToString("MMMMyyyy") + ".eng";
        readonly static string LogFilePath = LogFolderPath + "\\" + LogFileName;

        #region /-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/     Save Exception Logs     /-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/

        public static void EXC_WriteIn_LOGfile(Exception excp)
        {
            try
            {
                EXC_WriteIn_LOGfile(excp.Source + DateTime.Now.ToString() + System.Environment.NewLine +
                    excp.Message + System.Environment.NewLine + excp.StackTrace);
            }
            catch
            {
            }
        }

        public static void EXC_WriteIn_LOGfile(string str)
        {
            try
            {
                LOGstreamWriter = File.AppendText(LogFilePath);
                String message_str = System.Environment.NewLine + System.Environment.NewLine +
                   "-------------------------------------------------------"
                    + System.Environment.NewLine +
                    str + DateTime.Now.ToString()
                    + System.Environment.NewLine +
                    "-------------------------------------------------------"
                    + System.Environment.NewLine;
                LOGstreamWriter.Write(message_str);
                LOGstreamWriter.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public static void EXC_Init_LOGfile(string version)
        {
            try
            {
                if (!Directory.Exists(LogFolderPath))
                {
                    Directory.CreateDirectory(LogFolderPath);
                }

                if (File.Exists(LogFilePath))
                {
                    FileInfo fInfo = new FileInfo(LogFilePath);
                    {
                        if ((fInfo.Length / 1024f) / 1024f > 5.0)
                        {
                            fInfo.MoveTo(LogFolderPath + "\\" + "Log_" + DateTime.Now.ToString("ddMMMMyyyy hhmmss") + ".eng");
                        }
                    }
                }

                if (!File.Exists(LogFilePath))
                {
                    //LOG_fileStream = new FileStream(Global.ApplicationFilePath + "\\LOG.eng", FileMode.Create);
                    LOG_fileStream = new FileStream(LogFilePath, FileMode.Create);
                    LOGstreamWriter = new StreamWriter(LOG_fileStream);
                    object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                    AssemblyTitleAttribute titleAttribute = null;
                    if (attributes.Length > 0)
                        titleAttribute = (AssemblyTitleAttribute)attributes[0];

                    string title = "-----------------------------------------------" + System.Environment.NewLine + "VMS";
                    if (!string.IsNullOrEmpty(titleAttribute.Title))
                        title = titleAttribute.Title;

                    //title += " " + Global.MACHINE_MODE.ToString() + " ";
                    title += " Multimedia Radio Station ";
                    string s = "--------------------------------------------------------------------" + System.Environment.NewLine +
                    title + version +
                        " Session Started  " + DateTime.Now.ToString() + System.Environment.NewLine +
                        "--------------------------------------------------------------------" + System.Environment.NewLine;

                    LOGstreamWriter.WriteLine(s);
                    LOGstreamWriter.Close();
                    LOG_fileStream.Close();
                }
                else
                {
                    EXC_WriteIn_LOGfile(" Session Started ");
                }
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Source + System.Environment.NewLine +
                        excp.Message + System.Environment.NewLine + excp.StackTrace);
            }
        }

        #endregion

        #region /-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/     Save Logs     /-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/



        #endregion

    }
}
