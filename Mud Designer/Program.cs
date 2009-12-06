using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//MudEngine
using MudDesigner.MudEngine;
using MudDesigner.MudEngine.Attributes;
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.Objects;
using MudDesigner.MudEngine.Objects.Environment;

namespace MudDesigner
{
    static class Program
    {
        public static ProjectInformation Project { get; set; }
        public static Realm Realm { get; set; }
        public static Zone Zone {get;set;}
        public static Room Room { get; set; }
        public static ManagedScripting.ScriptingEngine ScriptEngine { get; set; }
        public static Form CurrentEditor { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Project = new ProjectInformation();

            FileManager.ValidateDataPaths();
            FileManager.FileType = FileManager.OutputFormats.XML;

            string filename = System.IO.Path.Combine(FileManager.GetDataPath(SaveDataTypes.Root), "Project.Xml");
            if (System.IO.File.Exists(filename))
                Project = (ProjectInformation)FileManager.Load(filename, Project);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MudDesigner.Editors.ToolkitLauncher());
        }
    }
}
