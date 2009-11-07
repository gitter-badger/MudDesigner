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
            Program.ExecuteApp("Project Manager.exe");
        }

        private void btnCurrencyEditor_Click(object sender, EventArgs e)
        {
            Program.ExecuteApp("Currency Editor.exe");
        }

        private void btnRoomDesigner_Click(object sender, EventArgs e)
        {
            Program.ExecuteApp("Room Designer.exe");
        }
    }
}
