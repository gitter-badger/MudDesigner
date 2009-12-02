using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.MudEngine
{
    public class Engine
    {
        private const string _InstallLocation = @"C:\Codeplex\MudDesigner\Example\";

        public enum SaveDataTypes
        {
            Root,
            Currency,
            Rooms,
            Zones,
            Realms,
        }

        /// <summary>
        /// Used to ensure that the paths needed to run the game exists.
        /// If no path is supplied, the engine uses it's current install path.
        /// </summary>
        /// <param name="validatedPath"></param>
        public static void ValidateDataPaths()
        {
            string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName;
            string assemblyName = System.IO.Path.GetFileName(assemblyPath);
            string installBase = assemblyPath.Substring(0, assemblyPath.Length - assemblyName.Length);
            string rootPath = System.IO.Path.Combine(installBase, "Data");
            ValidateDataPaths(rootPath);
        }

        /// <summary>
        /// Checks the supplied directory to ensure that all of the engines needed
        /// data folders are created
        /// </summary>
        /// <param name="InstallPath"></param>
        public static void ValidateDataPaths(string InstallPath)
        {
            if (!InstallPath.EndsWith("data", true, null))
                InstallPath = System.IO.Path.Combine(InstallPath, "Data");

            if (!System.IO.Directory.Exists(InstallPath))
                System.IO.Directory.CreateDirectory(InstallPath);

            foreach (SaveDataTypes value in Enum.GetValues(typeof(SaveDataTypes)))
            {
                string dataType = value.ToString();
                if (value.ToString() == "Root")
                    continue;

                if (!System.IO.Directory.Exists(System.IO.Path.Combine(InstallPath, dataType)))
                    System.IO.Directory.CreateDirectory(System.IO.Path.Combine(InstallPath, dataType));
            }
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
            string rootPath = System.IO.Path.Combine(installBase, "Data");

            if (DataType == SaveDataTypes.Root)
                return rootPath;
            else
                return System.IO.Path.Combine(rootPath, DataType.ToString());
        }
    }
}
