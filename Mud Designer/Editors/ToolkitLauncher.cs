using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MudDesigner.MudEngine.GameObjects.Environment;
using MudDesigner.MudEngine.FileSystem;

namespace MudDesigner.Editors
{
    public partial class ToolkitLauncher : Form
    {
        bool IsStartup = true;

        public ToolkitLauncher()
        {
            InitializeComponent();
            this.Text = "Mud Designer Toolkit " + Program.Settings.GetVersion();

            if (Program.Settings.DefaultRealm != null)
            {
                lblCurrentRealm.Text = "Current Realm: " + Program.Settings.DefaultRealm.Name;
                chkDefaultRealm.Checked = true;
            }

            //done starting up. This prevents checkbox change events from firing during startup
            IsStartup = false;
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

        private void chkDefaultRealm_CheckedChanged(object sender, EventArgs e)
        {
            if (IsStartup)
                return;

            if (!chkDefaultRealm.Checked)
            {
                Program.Settings.DefaultRealm = null;
                lblCurrentRealm.Text = "Current Realm: None";
                SaveSettings();
            }
            else
            {
                Realm realm = GetRealm();
                if (realm != null)
                {
                    Program.Settings.DefaultRealm = realm;
                    SaveSettings();
                }
            }
        }

        private void btnChangeRealm_Click(object sender, EventArgs e)
        {
            Realm realm = GetRealm();
            if (realm != null)
            {
                Program.Settings.DefaultRealm = realm;
                SaveSettings();
            }
        }

        public Realm GetRealm()
        {
            ExistingRealms form = new ExistingRealms();
            form.Show();
            this.Hide();

            while (form.Created)
                Application.DoEvents();

            this.Show();

            Realm realm = new Realm();
            string[] files = Directory.GetFiles(FileManager.GetDataPath(SaveDataTypes.Realms), "*.realm", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                realm = (Realm)FileManager.Load(file, realm);
                if (realm.Name == form.lstRealms.SelectedItem.ToString())
                {
                    Program.Settings.DefaultRealm = realm;
                    lblCurrentRealm.Text = "Current Realm: " + realm.Name;
                    return realm;
                }
            }

            return null;
        }

        public void SaveSettings()
        {
            string savePath = Path.Combine(Application.StartupPath, "Toolkit.xml");
            FileManager.Save(savePath, Program.Settings);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://muddesigner.dailyforum.net");
        }
    }
}
