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
        }
    }
}
