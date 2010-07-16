//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
        /// Saves the object using the specified output format
        /// </summary>
        /// <param name="Filename"></param>
        /// <param name="o"></param>
        public static void Save(string Filename, object o)
        {
            if (FileType == OutputFormats.XML)
            {
                XmlSerialization.Save(Filename, o);
            }
        }

        /// <summary>
        /// Loads the object using the specified FileType format
        /// </summary>
        /// <param name="Filename"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static object Load(string Filename, object o)
        {
            if (FileType == OutputFormats.XML)
            {
                return XmlSerialization.Load(Filename, o);
            }
            else return null;
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
            string rootPath = System.IO.Path.Combine(installBase, "Project");

            if (DataType == SaveDataTypes.Root)
                return rootPath;
            else
                return System.IO.Path.Combine(rootPath, DataType.ToString());
        }

        public static string GetDataPath(string Realm, string Zone)
        {
            string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName;
            string assemblyName = System.IO.Path.GetFileName(assemblyPath);
            string installBase = assemblyPath.Substring(0, assemblyPath.Length - assemblyName.Length);
            string rootPath = System.IO.Path.Combine(installBase, "Project");
            string realmsPath = System.IO.Path.Combine(rootPath, "Realms");
            string requestRealm = Path.Combine(realmsPath, Realm);
            string requestedRealmZones = Path.Combine(requestRealm, "Zones");
            string requestedZone = Path.Combine(requestedRealmZones, Zone);

            return requestedZone;
        }

        public static string GetDataPath(string Realm, string Zone, string Room)
        {
            return System.IO.Path.Combine(GetDataPath(Realm, Zone), Room);
        }

        //TODO Write CopyDirectory method.
    }
}
