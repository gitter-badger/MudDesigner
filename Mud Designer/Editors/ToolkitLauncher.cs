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
            this.Text = "Mud Designer Beta " + version;

            MessageBox.Show("Please note that the Zone Builder and Room Designers will be removed from this editor as a point of access here pretty soon.\n"
                + "If you need to access one of these editors you will need to use the Realm Explorer.", "Mud Designer");
        }
        
        private void btnProjectSettings_Click(object sender, EventArgs e)
        {
            ProjectSettings form = new ProjectSettings();

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

            form.Show();
            this.Hide();
            while (form.Created)
                Application.DoEvents();

            form = null;

            this.Show();
        }
        
        private void btnRoomDesigner_Click(object sender, EventArgs e)
        {
            RoomDesigner form = new RoomDesigner();

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

            form.Show();
            this.Hide();
            while (form.Created)
                Application.DoEvents();

            form = null;

            this.Show();
        }
    }
}
