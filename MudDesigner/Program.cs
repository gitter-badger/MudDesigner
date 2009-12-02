using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MudDesigner
{
    static class Program
    {
        static frmMain MudHUB;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MUDEngine.Engine.ValidateDataPaths();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool bExit = false;

            foreach (string arg in Environment.GetCommandLineArgs())
            {
                if (arg.ToLower().StartsWith("run="))
                {
                    string app = arg.Substring("Run=".Length);
                    if (!app.EndsWith(".exe"))
                        app += ".exe";

                    ExecuteApp(app);
                    bExit = true;
                }
            }

            if (!bExit)
            {
                MudHUB = new frmMain();
                Application.Run(MudHUB);
            }
        }

        internal static void ExecuteApp(string appName)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();

            info.FileName = System.IO.Path.Combine(MUDEngine.Engine.GetDataPath(MUDEngine.Engine.SaveDataTypes.Root), appName);

            info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            info.WorkingDirectory = Application.StartupPath;

            process.StartInfo = info;
            try
            {
                process.Start();
                if (MudHUB != null)
                    MudHUB.Hide();

                process.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:\n" + ex.Message + "\nPlease change MUDEngine.Engine._InstallLocation to the Examples directory found inside of your downloaded source code folder.", "Editor HUB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                process = null;
                //It will be null if the HUB was launched from another editor using it's arguments
                //to launch another editor.
                if (MudHUB != null)
                    MudHUB.Show();
            }
        }
    }
}
