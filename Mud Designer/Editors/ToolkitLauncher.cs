using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MudDesigner.Editors
{
    public partial class ToolkitLauncher : Form
    {
        public const int VersionMajor = 1;
        public const int VersionMinor = 0;
        public const int VersionRevision = 0;
        public string version = VersionMajor.ToString() + "." + VersionMinor.ToString() + "." + VersionRevision.ToString();

        public ToolkitLauncher()
        {
            InitializeComponent();
            this.Text = "Mud Designer Toolkit " + Settings.GetVersion();

            Settings settings = new Settings();
        }
        
        private void btnProjectSettings_Click(object sender, EventArgs e)
        {
            ProjectSettings form = new ProjectSettings();
            Program.CurrentEditor = form;

            form.Show();
            this.Hide();
            while (form.Created)
                Application.DoEvents();

            form = null;

            this.Show();
        }

        private void btnCurrencyEditor_Click(object sender, EventArgs e)
        {
            CurrencyEditor form = new CurrencyEditor();
            Program.CurrentEditor = form;

            form.Show();
            this.Hide();
            while (form.Created)
                Application.DoEvents();

            form = null;

            this.Show();
        }
        
        private void btnRealmExplorer_Click(object sender, EventArgs e)
        {
            RealmExplorer form = new RealmExplorer();
            Program.CurrentEditor = form;

            form.Show();
            this.Hide();
            while (form.Created)
                Application.DoEvents();

            form = null;

            this.Show();
        }

        private void btnZoneBuilder_Click(object sender, EventArgs e)
        {
            ZoneBuilder form = new ZoneBuilder();
            Program.CurrentEditor = form;

            form.Show();
            this.Hide();
            while (form.Created)
                Application.DoEvents();

            form = null;

            this.Show();
        }
    }
}
