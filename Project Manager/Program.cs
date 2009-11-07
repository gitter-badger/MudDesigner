using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using MUDEngine;
using MUDEngine.Objects;
using MUDEngine.Objects.Environment;
using MUDEngine.FileSystem;

namespace Project_Manager
{
    static class Program
    {
        internal static MUDEngine.ProjectInformation project;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Make sure all our paths are created before we start working with the editor.
            Engine.ValidateProjectPath(Application.StartupPath);
            FileSystem.FileType = FileSystem.OutputFormats.XML;

            project = new ProjectInformation();

            //check if a project file exists, or use the new instance
            if (System.IO.File.Exists(Application.StartupPath + @"\Data\project.xml"))
            {
                project = (ProjectInformation)FileSystem.Load(Application.StartupPath + @"\Data\project.xml", project);
            }

            //run the app
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());

            FileSystem.Save(Application.StartupPath + @"\Data\project.xml", project);
        }
    }
}
