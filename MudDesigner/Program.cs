using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MudDesigner
{
    static class Program
    {
        static frmMain MudHUB;

        internal static string _InstallLocation = @"C:\CodePlex\MudDesigner\";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MudHUB = new frmMain();
            Application.Run(MudHUB);
        }

        internal static void ExecuteApp(string appName)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();

            //If running in debug mode we need to hard-code the paths as during normal running of the apps
            //all of the apps are running within the Apps directory.
#if DEBUG
            string[] apps = new string[] { };
            try
            {
                apps = System.IO.Directory.GetFiles(_InstallLocation, "*.exe", System.IO.SearchOption.AllDirectories);
            }
            catch
            {
                MessageBox.Show("Error: The directory '" + _InstallLocation + "' does not exist!\n\n"
                    + "The engine has a #DEBUG constant defined in the project properties."
                    + "\nIf you are wanting to run the Designer from within Visual Studio, please change the\n"
                    + "MudDesigner.Program._InstallLocation Field to the current ROOT directory of your source code."
                    + "\n\nIf you are running a Release build of the engine, with all of the editors contained within the"
                    + " same directory, then edit the MudDesigner project properties to remove the #DEBUG constant."
                    + "\n" + appName + " will not load.",
                    "Mud Designer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<string> legalApps = new List<string>();

            foreach (string app in apps)
            {
                if ((!app.EndsWith(".vshost.exe"))
                    && (!app.EndsWith(".vshost.exe.manifest"))
                    && System.IO.Directory.GetParent(app).Name == "Debug"
                    && System.IO.Directory.GetParent(app).Parent.Name == "bin")
                {
                    legalApps.Add(app);
                }
            }

            string filename = "";
            foreach (string app in legalApps)
            {
                if (System.IO.Path.GetFileName(app).ToLower() == appName.ToLower())
                {
                    filename = app;
                    break;
                }
            }

            info.FileName = filename;
#else
            info.FileName = appName;
#endif

            info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            info.WorkingDirectory = Application.StartupPath;

            process.StartInfo = info;
            try
            {
                process.Start();
                MudHUB.Hide();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:\n" + ex.Message, "Editor HUB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                process = null;
                MudHUB.Show();
            }
        }
    }
}
