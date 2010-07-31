//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace MudEngine.FileSystem
{
    /// <summary>
    /// Handles saving and loading of engine objects
    /// </summary>
    public static class FileManager
    {
        public enum OutputFormats
        {
            XML = 0,
        }

        /// <summary>
        /// The filetype that the MUDs files will be saved as
        /// </summary>
        public static OutputFormats FileType
        {
            get;
            set;
        }

        /// <summary>
        /// Writes content out to a file.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void WriteLine(string filename, string value, string name)
        {
            if (!File.Exists(filename))
            {
                FileStream s = File.Create(filename);
                s.Close();
            }

            using (StreamWriter sw = File.AppendText(filename))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(name);
                sb.Append("=");
                sb.Append(value);
                sw.WriteLine(sb.ToString());
                sw.Close();
            }
        }

        public static string GetData(string filename, string name)
        {
            foreach (string line in File.ReadAllLines(filename))
            {
                if (line.StartsWith(name))
                    return line.Substring(name.Length + 1); //Accounts for name=value;
            }

            return "No data Found.";
        }

        /// <summary>
        /// Returns the complete path to the specified data's save folder.
        /// </summary>
        /// <param name="DataType"></param>
        /// <returns></returns>
        public static string GetDataPath(SaveDataTypes DataType)
        {
            string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName;
            string assemblyName = System.IO.Path.GetFileName(assemblyPath);
            string installBase = assemblyPath.Substring(0, assemblyPath.Length - assemblyName.Length);

            if (DataType == SaveDataTypes.Root)
                return installBase;
            else
                return System.IO.Path.Combine(installBase, DataType.ToString());
        }

        public static string GetDataPath(string Realm, string Zone)
        {
            string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName;
            string assemblyName = System.IO.Path.GetFileName(assemblyPath);
            string installBase = assemblyPath.Substring(0, assemblyPath.Length - assemblyName.Length);
            string realmsPath = System.IO.Path.Combine(installBase, "Realms");
            string requestRealm = Path.Combine(installBase, Realm);
            string requestedRealmZones = Path.Combine(installBase, "Zones");
            string requestedZone = Path.Combine(installBase, Zone);

            return requestedZone;
        }

        public static string GetDataPath(string Realm, string Zone, string Room)
        {
            return System.IO.Path.Combine(GetDataPath(Realm, Zone), Room);
        }

        //TODO Write CopyDirectory method.
    }
}
