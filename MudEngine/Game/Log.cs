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
        
        /// <summary>
        /// Writes a message to the log file and if pushMessage is true it will ignore placing the message
        /// into the cachedmessages for later pooling, but push it directly to the console.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="pushMessage"></param>
        public static void Write(String message, Boolean pushMessage)
        {
            String filename = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Root), "Log.txt");
            StreamWriter sw;

            if (File.Exists(filename))
                sw = File.AppendText(filename);
            else
                sw = File.CreateText(filename);

            sw.WriteLine(DateTime.Now.ToString() + ": " + message);
            sw.Close();

            //Add to the cache so consoles can get these messages if they want to.
            //If Pushmessage=true then we skip caching and dump it straight to the console
            //TODO: Allow for enabling critical error messages being forced into the console, regardless if !IsMultiplayer
            if ((pushMessage) && (!IsVerbose))
                Console.WriteLine(message);
            else
                cachedMessages.Add(message);
        }
    
        public static void Write(String message)
        {
            Write(message, true);
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
