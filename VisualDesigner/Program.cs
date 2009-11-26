using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VisualDesigner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MUDEngine.Engine.ValidateDataPaths();
            MUDEngine.FileSystem.FileSystem.FileType = MUDEngine.FileSystem.FileSystem.OutputFormats.XML;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
