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
        Root,
        Players,
        Environments,
        Characters,
        Equipment,
        Scripts,
    }

    /// <summary>
    /// Contains the paths for the engines file storage.
    /// </summary>
    public class DataPaths
    {
        public DataPaths()
        {
            this._InstallRoot = this.GetInstallPath();

            this.SetupPaths();

            this.SetExtension(DataTypes.Characters, ".character");
            this.SetExtension(DataTypes.Environments, ".environment");
            this.SetExtension(DataTypes.Equipment, ".equipment");
            this.SetExtension(DataTypes.Players, ".player");
            this.SetExtension(DataTypes.Scripts, ".cs");
        }

        public String GetInstallPath()
        {
            String path = Assembly.GetExecutingAssembly().Location;
            String assemblyFile = Path.GetFileName(path);
            return path.Substring(0, path.Length - assemblyFile.Length);
        }

        private void SetupPaths()
        {
            this.SetAbsolutePath(Path.Combine(this._InstallRoot, "Characters"), DataTypes.Characters);
            this.SetAbsolutePath(Path.Combine(this._InstallRoot, "Environments"), DataTypes.Environments);
            this.SetAbsolutePath(Path.Combine(this._InstallRoot, "Equipment"), DataTypes.Equipment);
            this.SetAbsolutePath(Path.Combine(this._InstallRoot, "Players"), DataTypes.Players);
            this.SetAbsolutePath(Path.Combine(this._InstallRoot, "GameScripts"), DataTypes.Scripts);
        }

        public void SetAbsolutePath(String path, DataTypes objectType)
        {
            if (!path.EndsWith(@"\"))
                path = path.Insert(path.Length, @"\");

            switch (objectType)
            {
                case DataTypes.Characters:
                    this._Characters = path;
                    break;
                case DataTypes.Environments:
                    this._Environments = path;
                    break;
                case DataTypes.Equipment:
                    this._Equipment = path;
                    break;
                case DataTypes.Players:
                    this._Players = path;
                    break;
                case DataTypes.Scripts:
                    this._Scripts = path;
                    break;
                case DataTypes.Root:
                    this._InstallRoot = path;
                    this.SetupPaths(); //Re-Setup all the paths since the root has changed.
                    break;
            }
        }

        public void SetRelativePath(String path, DataTypes objectType)
        {
            if (!path.EndsWith(@"\"))
                path = path.Insert(path.Length, @"\");

            String correctedPath = Path.Combine(this._InstallRoot, path);

            switch (objectType)
            {
                case DataTypes.Characters:
                    this._Characters = correctedPath;
                    break;
                case DataTypes.Environments:
                    this._Environments = correctedPath;
                    break;
                case DataTypes.Equipment:
                    this._Equipment = correctedPath;
                    break;
                case DataTypes.Players:
                    this._Players = correctedPath;
                    break;
                case DataTypes.Scripts:
                    this._Scripts = correctedPath;
                    break;
                case DataTypes.Root:
                    this._InstallRoot = Path.Combine(this.GetInstallPath(), path);
                    this.SetupPaths(); //Re-setup all the paths since the root has changed.
                    break;
            }
        }

        public String GetPath(DataTypes objectType)
        {
            switch (objectType)
            {
                case DataTypes.Root:
                    return this._InstallRoot;
                case DataTypes.Characters:
                    return this._Characters;
                case DataTypes.Environments:
                    return this._Environments;
                case DataTypes.Equipment:
                    return this._Equipment;
                case DataTypes.Players:
                    return this._Players;
                case DataTypes.Scripts:
                    return this._Scripts;
            }

            return String.Empty;
        }

        public String GetFilePath(DataTypes objectType, String filename)
        {
            String result = String.Empty;

            switch (objectType)
            {
                case DataTypes.Root:
                    result = Path.Combine(this._InstallRoot, filename + this.GetExtension(DataTypes.Root));
                    break;
                case DataTypes.Characters:
                    result = Path.Combine(this._Characters, filename + this.GetExtension(DataTypes.Characters));
                    break;
                case DataTypes.Environments:
                    result = Path.Combine(this._Environments, filename + this.GetExtension(DataTypes.Environments));
                    break;
                case DataTypes.Equipment:
                    result = Path.Combine(this._Equipment, filename + this.GetExtension(DataTypes.Equipment));
                    break;
                case DataTypes.Players:
                    result = Path.Combine(this._Players, filename + this.GetExtension(DataTypes.Players));
                    break;
                case DataTypes.Scripts:
                    result = Path.Combine(this._Scripts, filename + this.GetExtension(DataTypes.Scripts));
                    break;
            }

            return result;
        }

        public String GetExtension(DataTypes objectType)
        {
            String result = String.Empty;

            switch (objectType)
            {
                case DataTypes.Root:
                    result = ".dat";
                    break;
                case DataTypes.Characters:
                    result = this._CharacterExt;
                    break;
                case DataTypes.Environments:
                    result = this._EnvironmentExt;
                    break;
                case DataTypes.Equipment:
                    result = this._EquipmentExt;
                    break;
                case DataTypes.Scripts:
                    result = this._ScriptExt;
                    break;
                case DataTypes.Players:
                    result = this._PlayersExt;
                    break;  
            }

            return result;
        }

        public void SetExtension(DataTypes objectType, String extension)
        {
            if (!extension.StartsWith("."))
                extension = extension.Insert(0, ".");

            switch (objectType)
            {
                case DataTypes.Characters:
                    this._CharacterExt = extension;
                    break;
                case DataTypes.Environments:
                    this._EnvironmentExt = extension;
                    break;
                case DataTypes.Equipment:
                    this._EquipmentExt = extension;
                    break;
                case DataTypes.Players:
                    this._PlayersExt = extension;
                    break;
                case DataTypes.Scripts:
                    this._ScriptExt = extension;
                    break;
            }
        }

        private String _InstallRoot;
        private String _Players;
        private String _PlayersExt;
        private String _Environments;
        private String _EnvironmentExt;
        private String _Characters;
        private String _CharacterExt;
        private String _Equipment;
        private String _EquipmentExt;
        private String _Scripts;
        private String _ScriptExt;
    }
}
