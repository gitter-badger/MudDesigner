using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ManagedScripting;
//MudEngine
using MudDesigner.MudEngine;
using MudDesigner.MudEngine.Attributes;
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.Objects;
using MudDesigner.MudEngine.Objects.Environment;


namespace MudDesigner.Editors
{
    public partial class RealmExplorer : Form
    {
        List<Zone> _AvailableZones;
        
        public RealmExplorer()
        {
            InitializeComponent();
            Program.Zone = new Zone();
            Program.Realm = new Realm();
            Program.Room = new Room();

            _AvailableZones = new List<Zone>();
            Program.ScriptEngine = new ScriptingEngine();
            Program.ScriptEngine.CompileStyle = ManagedScripting.Compilers.BaseCompiler.ScriptCompileStyle.CompileToMemory;
            Program.ScriptEngine.KeepTempFiles = false;

            propertyRealm.SelectedObject = Program.Realm;

            string[] existingRealms = System.IO.Directory.GetFiles(FileManager.GetDataPath(SaveDataTypes.Realms));
            foreach (string realm in existingRealms)
                lstRealms.Items.Add(System.IO.Path.GetFileNameWithoutExtension(realm));
        }

        private void btnNewRealm_Click(object sender, EventArgs e)
        {
            Program.Zone = new Zone();
            Program.Realm = new Realm();
            lstZones.Items.Clear();

            propertyRealm.SelectedObject = Program.Realm;
        }

        private void btnSaveRealm_Click(object sender, EventArgs e)
        {
            string path = FileManager.GetDataPath(SaveDataTypes.Realms);
            string realmFile = System.IO.Path.Combine(path, Program.Realm.Name + ".realm");
            if (System.IO.File.Exists(realmFile))
            {
                DialogResult result = MessageBox.Show("File exists!\nOverwrite?", "Realm Explorer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;
            }

            FileManager.Save(realmFile, Program.Realm);

            if (!lstRealms.Items.Contains(Program.Realm.Name))
            lstRealms.Items.Add(Program.Realm.Name);
        }

        private void lstRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRealms.SelectedIndex == -1)
                return;
            lstZones.Items.Clear();

            string path = FileManager.GetDataPath(SaveDataTypes.Realms);
            string realmFile = System.IO.Path.Combine(path, lstRealms.SelectedItem.ToString() + ".realm");
            Program.Realm = (Realm)FileManager.Load(realmFile, Program.Realm);

            propertyRealm.SelectedObject = Program.Realm;
            foreach (Zone zone in Program.Realm.Zones)
                lstZones.Items.Add(zone.Name);
        }

        private void btnDeleteRealm_Click(object sender, EventArgs e)
        {
            if (lstRealms.SelectedIndex == -1)
            {
                MessageBox.Show("Select a Realm to delete first!", "Realm Exporer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete the " + lstRealms.SelectedItem.ToString() + " Realm?",
                "Realm Explorer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            string path = FileManager.GetDataPath(SaveDataTypes.Realms);
            string realmFile = System.IO.Path.Combine(path, lstRealms.SelectedItem.ToString() + ".realm");
            System.IO.File.Delete(realmFile);
            lstRealms.Items.Remove(lstRealms.SelectedItem);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Script")
            {
                txtScript.Text = Program.Realm.Script;
            }
        }

        private void btnValidateScript_Click(object sender, EventArgs e)
        {
            Program.ScriptEngine.Compiler = ManagedScripting.ScriptingEngine.CompilerSelections.SourceCompiler;
            Program.ScriptEngine.AddReference(FileManager.GetDataPath(SaveDataTypes.Root) + "\\Mud Designer.exe");
            string code = "namespace MudDesigner.MudEngine.Objects.Environment\n"
                + "{\n"
                + "  public class " + Program.Realm.Name.Replace(" ", "") + " : Realm\n"
                + "  {\n"
                + "     " + txtScript.Text + "\n"
                + "  }\n"
                + "}\n";
            MessageBox.Show(Program.ScriptEngine.Compile(code), "Script Compiling", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBuildZone_Click(object sender, EventArgs e)
        {
            if (lstRealms.SelectedIndex == -1)
            {
                MessageBox.Show("Select a Realm to build a Zone for.", "Realm Explorer", MessageBoxButtons.OK);
                return;
            }
            
            ZoneBuilder form = new ZoneBuilder();

            if (btnBuildZone.Text == "Edit Selected Zone")
                form.IsEditingExisting = true;

            form.Show();
            this.Hide();
                
            while (form.Created)
                Application.DoEvents();

            form = null;

            this.Show();
        }

        private void btnMoveZone_Click(object sender, EventArgs e)
        {
            if (lstZones.SelectedIndex == -1)
            {
                MessageBox.Show("Select a Zone to transfer first.", "Realm Explorer", MessageBoxButtons.OK);
                return;
            }

            ExistingRealms form = new ExistingRealms(lstZones.SelectedItem.ToString());
            form.Show();
            this.Hide();

            while (form.Created)
                Application.DoEvents();

            this.Show();
        }

        private void btnUnselectZone_Click(object sender, EventArgs e)
        {
            lstZones.SelectedIndex = -1;
            btnBuildZone.Text = "Build A Zone";
        }

        private void btnDeleteZone_Click(object sender, EventArgs e)
        {
            if (lstZones.SelectedIndex == -1)
            {
                MessageBox.Show("No zone selected for deletion.", "Realm Explorer", MessageBoxButtons.OK);
                return;
            }

            Zone zone = Program.Realm.GetZone(lstZones.SelectedItem.ToString());
            if (zone == null)
            {
                MessageBox.Show("Error deleting Zone.", "Realm Exporer");
                return;
            }

            Program.Realm.Zones.Remove(zone);
            string filename = System.IO.Path.Combine(FileManager.GetDataPath(SaveDataTypes.Zones), lstZones.SelectedItem.ToString() + ".zone");

            if (System.IO.File.Exists(filename))
                System.IO.File.Delete(filename);

            filename = System.IO.Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), Program.Realm.Name + ".realm");
            lstZones.Items.Remove(lstZones.SelectedItem);
            FileManager.Save(filename, Program.Realm);
        }

        private void lstZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnBuildZone.Text = "Edit Selected Zone";
        }
    }
}
