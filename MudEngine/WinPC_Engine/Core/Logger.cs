using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace MudEngine.Core
{
    /// <summary>
    /// Public Engine Logging Class.
    /// </summary>
    public static class Logger
    {
        public enum Importance
        {
            Critical = 0,
            Error = 1,
            Warning = 2,
            Information = 3,
            Chat = 4,
            All = 5
        }

        /// <summary>
        /// The Log Filename for the engine log.
        /// </summary>
        public static string LogFilename { get; set; }

        /// <summary>
        /// Gets or Sets if the Logger is enabled or disabled.
        /// </summary>
        public static Boolean Enabled { get; set; }

        /// <summary>
        /// Gets or Sets if the logger will output it's information to the Console.
        /// </summary>
        public static Boolean ConsoleOutPut { get; set; }

        /// <summary>
        /// Clears the queued log messages from cache
        /// </summary>
        public static void ClearLog()
        {
            //If the log file exists, delete it.
            if (String.IsNullOrEmpty(LogFilename))
                LogFilename = "Engine.Log";

            if (System.IO.File.Exists(LogFilename))
                System.IO.File.Delete(LogFilename);

            //Clear the cache.
            _Messages.Clear();
        }

        public static void WriteLine(String message, Importance importance)
        {
            //Only write to log if enabled.
            if (!Enabled)
                return;

            //Make sure we have a valid filename
            if (String.IsNullOrEmpty(LogFilename))
                LogFilename = "Engine.Log";

            //Ensure that the log messages cache is not null
            if (_Messages == null)
                _Messages = new List<string>();

            //Get the current time and format it
            String Time = DateTime.Now.ToString("h:mm:ss:ff tt");

            //Output to console if enabled.
            if (ConsoleOutPut)
                Console.WriteLine(Time + ": " + message);

            //Try to write the message to the log file.
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(LogFilename, true))
                {
                    //Write the message to file
                    file.WriteLine(Time + ": " + message);
                    //Add it to the messages cache.
                    _Messages.Add(Time + ": " + message);
                }
            }
            catch
            {
                throw new Exception("Unable to write message (" + message + ") to log file (" + LogFilename + ").");
            }
        }

        /// <summary>
        /// Writes a single line to the engine log file.
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLine(String message)
        {
            //Just output as typical informational stuff.
            Logger.WriteLine(message, Importance.Information);
        }

        /// <summary>
        /// Returns an array of messages that have been queued in the log cache.
        /// </summary>
        /// <returns></returns>
        public static String[] GetMessages()
        {
            if (_Messages == null)
                return new string[0];
            else
              return _Messages.ToArray();
        }

        private static List<String> _Messages;
    }
}
