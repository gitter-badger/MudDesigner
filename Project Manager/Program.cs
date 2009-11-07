using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
            MUDEngine.Engine.ValidateProjectPath(Application.StartupPath);
            MUDEngine.FileSystem.FileSystem.FileType = MUDEngine.FileSystem.FileSystem.OutputFormats.XML;

            project = new MUDEngine.ProjectInformation();

            //check if a project file exists, or use the new instance
            if (System.IO.File.Exists(Application.StartupPath + @"\Data\project.xml"))
            {
                project = (MUDEngine.ProjectInformation)MUDEngine.FileSystem.FileSystem.Load(Application.StartupPath + @"\Data\project.xml", project);
            }

            //run the app
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());

            MUDEngine.FileSystem.FileSystem.Save(Application.StartupPath + @"\Data\project.xml", project);
        }
    }
}
