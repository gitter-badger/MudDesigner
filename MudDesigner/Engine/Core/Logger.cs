/* Logger
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Provides a method for logging events to file. Can be helpful to log information for debugging.
 */

//Microsoft .NET using statements
using System;
using System.Collections.Generic;

namespace MudDesigner.Engine.Core
{
    /// <summary>
    /// Provides a method for logging events to file. Can be helpful to log information for debugging.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// The level of importance the messages have.
        /// </summary>
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
        /// Gets or Sets if the logger should cache each message it receives into a collection pool for
        /// access at a later time.
        /// </summary>
        public static bool CacheContent { get; set; }

        /// <summary>
        /// Gets or Sets the cached message pool for the logger
        /// </summary>
        public static List<string> Cache { get; set; }

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
            if (Cache != null)
                Cache.Clear();
            else
                Cache = new List<string>();
        }

        public static void WriteLine(String message, Importance importance = Importance.Information)
        {
            //Only write to log if enabled.
            if (!Enabled)
                return;

            //Make sure we have a valid filename
            if (String.IsNullOrEmpty(LogFilename))
                LogFilename = "Engine.Log";

            //Ensure that the log messages cache is not null
            if (Cache == null)
                Cache = new List<string>();

            //Get the current time and format it
            String Time = DateTime.Now.ToString("h:mm:ss:ff tt");

            //Output to console if enabled.
            if (ConsoleOutPut)
                Console.WriteLine(Time + ": " + message);

            if (CacheContent)
                Cache.Add(Time + ": " + message);

            //Try to write the message to the log file.
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(LogFilename, true))
                {
                    //Write the message to file
                    file.WriteLine(Time + ": " + message);
                }
            }
            catch
            {
                throw new Exception("Unable to write message (" + message + ") to log file (" + LogFilename + ").");
            }
        }

        /// <summary>
        /// Returns an array of messages that have been queued in the log cache.
        /// </summary>
        /// <returns></returns>
        public static String[] GetMessages()
        {
            if (Cache == null)
                Cache = new List<string>();

            return Cache.ToArray();
        }
    }
}
