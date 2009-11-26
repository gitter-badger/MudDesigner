using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CurrencyEditor
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

namespace Project2
{
    public class MyClass
    {
        public string DoSomething(string argument1, string argument2)
        {
            if (!argument1.Equals(argument2))
                return "The two arguments are different";
            else
                return "The two arguments are the same";
        }
    }
}