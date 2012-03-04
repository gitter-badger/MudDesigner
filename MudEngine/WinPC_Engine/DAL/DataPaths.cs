using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace MudEngine.DAL
{
    public enum DataTypes
    {
        Players,
        Environments,
        Characters,
        Equipment
    }

    /// <summary>
    /// Contains the paths for the engines file storage.
    /// </summary>
    public class DataPaths
    {
        public DataPaths()
        {
            String path = Assembly.GetExecutingAssembly().Location;
            String assemblyFile = Path.GetFileName(path);
            this._InstallRoot = path.Substring(0, path.Length - assemblyFile.Length);

            this.SetAbsolutePath(Path.Combine(this._InstallRoot, "Players"), DataTypes.Players);
        }

        public void SetAbsolutePath(String path, DataTypes objectType)
        {
            if (!path.EndsWith(@"\"))
                path = path.Insert(path.Length, @"\");

            switch (objectType)
            {
                case DataTypes.Players:
                    this._Players = path;
                    break;
            }
        }

        public void SetRelativePath(String path, DataTypes objectType)
        {
        }

        public String GetPath(DataTypes objectType)
        {
            if (objectType == DataTypes.Players)
                return this._Players;
            else
                return String.Empty;
        }

        private String _InstallRoot;
        private String _Players;
        private String _Environments;
        private String _Characters;
        private String _Equipment;
    }
}
