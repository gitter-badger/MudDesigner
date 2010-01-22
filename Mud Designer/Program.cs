using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Design;

//MudEngine
using MudDesigner.MudEngine;
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
        public static Designer Designer{ get; set; }
        internal static Form CurrentEditor { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Setup default application properties
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Run the toolkit
            Designer = new Designer();
            Application.Run(Designer);
        }
    }
}
