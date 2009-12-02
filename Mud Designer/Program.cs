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
        public static ProjectInformation Project{ get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Engine.ValidateDataPaths();
            FileSystem.FileType = FileSystem.OutputFormats.XML;
            Project = new ProjectInformation();

            string filename = System.IO.Path.Combine(Engine.GetDataPath(Engine.SaveDataTypes.Root), "Project.Xml");
            if (System.IO.File.Exists(filename))
                Project = (ProjectInformation)FileSystem.Load(filename, Project);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MudDesigner.Editors.ToolkitLauncher());
        }
    }
}
