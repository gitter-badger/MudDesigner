using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using MudEngine.FileSystem;

namespace MudEngine.GameManagement
{
    public static class Log
    {
        static List<String> cachedMessages = new List<String>();
        public static Boolean IsVerbose;

        public static void Write(String message)
        {
            String filename = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Root), "Log.txt");
            StreamWriter sw;

            if (File.Exists(filename))
                sw = File.AppendText(filename);
            else
                sw = File.CreateText(filename);

            sw.WriteLine(message);
            sw.Close();

            //Add to the cache so consoles can get these messages if they want to.
            cachedMessages.Add(message);
        }

        public static String GetMessages()
        {
            StringBuilder sb = new StringBuilder();

            foreach (String message in cachedMessages)
            {
                sb.AppendLine(message);
            }

            if ((sb.ToString() == "") || (IsVerbose))
                return "";
            else
                return sb.ToString();
        }

        public static void FlushMessages()
        {
            cachedMessages = new List<String>();
        }
    }
}
