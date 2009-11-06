using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MudDesigner
{
    public partial class frmMain : Form
    {
        public const int VersionMajor = 1;
        public const int VersionMinor = 0;
        public const int VersionRevision = 0;
        public string version = VersionMajor.ToString() + "." + VersionMinor.ToString() + "." + VersionRevision.ToString();

        public frmMain()
        {
            InitializeComponent();
            this.Text = "Mud Designer Beta " + version;
        }

        private void btnProjectManager_Click(object sender, EventArgs e)
        {
            ExecuteApp("Project Manager.exe");
        }

        private void ExecuteApp(string appName)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            
            //If running in debug mode we need to hard-code the paths as during normal running of the apps
            //all of the apps are running within the Apps directory.
#if DEBUG
            info.FileName = @"C:\CodePlex\MudDesigner\Project Manager\bin\Debug\" + appName;
#else
            info.FileName = appName;
#endif
            info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            info.WorkingDirectory = Application.StartupPath;

            //If running in debug mode we dont want the apps saving stuff outside of its debug folder
#if DEBUG
            info.Arguments = ""; 
#else
            info.Arguments = Application.StartupPath + "\Data\";
#endif
            process.StartInfo = info;
            process.Start();
            this.Hide();
            process.WaitForExit();

            process = null;
            this.Show();
        }
    }
}
