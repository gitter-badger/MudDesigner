using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//MudEngine
using MudDesigner.MudEngine;
using MudDesigner.MudEngine.Attributes;
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameManagement;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;
//Script Engine
using ManagedScripting;
using ManagedScripting.CodeBuilding;

namespace MudDesigner
{
    static class Program
    {
        public static ProjectInformation Project { get; set; }
        public static Realm Realm { get; set; }
        public static Zone Zone {get;set;}
        public static Room Room { get; set; }
        public static ScriptingEngine ScriptEngine { get; set; }
        public static Form CurrentEditor { get; set; }
        public static Settings Settings { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Creates a new instance of the project and settings
            Project = new ProjectInformation();
            Settings = new Settings();

            //Make sure our directory structure is created
            FileManager.ValidateDataPaths();
            //Setup what fileformat we are going to use.
            FileManager.FileType = FileManager.OutputFormats.XML;

            //Load the MUD project if the file exists
            string filename = System.IO.Path.Combine(FileManager.GetDataPath(SaveDataTypes.Root), "Project.Xml");
            if (System.IO.File.Exists(filename))
                Project = (ProjectInformation)FileManager.Load(filename, Project);

            //Load the toolkit settings if it exists.
            filename = System.IO.Path.Combine(Application.StartupPath, "Toolkit.xml");
            if (System.IO.File.Exists(filename))
                Settings = (Settings)FileManager.Load(filename, Settings);

            //Setup default application properties
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Run the toolkit
            Application.Run(new MudDesigner.Editors.ToolkitLauncher());
        }
    }
}
