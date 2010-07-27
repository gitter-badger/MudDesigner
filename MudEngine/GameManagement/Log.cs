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
        static List<string> cachedMessages = new List<string>();

        public static void Write(string message)
        {
            string filename = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Root), "Log.txt");
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

        public static string GetMessages()
        {
            string messages = "";
            StringBuilder sb = new StringBuilder();

            foreach (string message in cachedMessages)
            {
                sb.AppendLine(message);
            }

            if (sb.ToString() == "")
                return "";
            else
                return sb.ToString();
        }

        public static void FlushMessages()
        {
            cachedMessages = new List<string>();
        }
    }
}
