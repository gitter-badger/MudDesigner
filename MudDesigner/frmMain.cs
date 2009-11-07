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

        private void btnCurrencyEditor_Click(object sender, EventArgs e)
        {
            ExecuteApp("Currency Editor.exe");
        }

        private void ExecuteApp(string appName)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            
            //If running in debug mode we need to hard-code the paths as during normal running of the apps
            //all of the apps are running within the Apps directory.
#if DEBUG
            string[] apps = System.IO.Directory.GetFiles(@"C:\CodePlex\MudDesigner\", "*.exe", System.IO.SearchOption.AllDirectories);
            List<string> legalApps = new List<string>();

            foreach(string app in apps)
            {
                if ((!app.EndsWith(".vshost.exe")) 
                    && (!app.EndsWith(".vshost.exe.manifest"))
                    && System.IO.Directory.GetParent(app).Name == "Debug"
                    && System.IO.Directory.GetParent(app).Parent.Name == "bin")
                {
                    legalApps.Add(app);
                }
            }
            string filename = "" ;
            foreach(string app in legalApps)
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
                this.Hide();
                process.WaitForExit();
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR:\n" + ex.Message, "Editor HUB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                process = null;
                this.Show();
            }
        }
    }
}
