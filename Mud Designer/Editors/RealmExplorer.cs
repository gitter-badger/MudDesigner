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
            _AvailableZones = new List<Zone>();
            Program.ScriptEngine = new ScriptingEngine();
            Program.ScriptEngine.CompileStyle = ManagedScripting.Compilers.BaseCompiler.ScriptCompileStyle.CompileToMemory;
            Program.ScriptEngine.KeepTempFiles = false;

            BuildZoneLists();

            SetupScript();

            propertyRealm.SelectedObject = Program.Realm;

            string[] existingRealms = System.IO.Directory.GetFiles(FileManager.GetDataPath(SaveDataTypes.Realms));
            foreach (string realm in existingRealms)
                lstRealms.Items.Add(System.IO.Path.GetFileNameWithoutExtension(realm));
        }

        private void BuildZoneLists()
        {
            string[] zones = System.IO.Directory.GetFiles(FileManager.GetDataPath(SaveDataTypes.Zones), "*.zone");
            bool available = true;
            lstAvailableZones.Items.Clear();
            lstZonesInRealm.Items.Clear();

            foreach (string zone in zones)
            {
                string[] realms = System.IO.Directory.GetFiles(FileManager.GetDataPath(SaveDataTypes.Realms), "*.realm");

                foreach (string realm in realms)
                {
                    Realm r = new Realm();
                    r = (Realm)ManagedScripting.XmlSerialization.Load(realm, r);

                    foreach (Zone z in r.Zones)
                    {
                        if (z.Name == System.IO.Path.GetFileNameWithoutExtension(zone))
                        {
                            available = false;
                            break;
                        }
                    }
                    if (!available)
                        break;
                }

                if (!available)
                    break;
                else
                {
                    lstAvailableZones.Items.Add(System.IO.Path.GetFileNameWithoutExtension(zone));
                }
            }
        }

        private void SetupScript()
        {
            //Check if the realm script is empty. If so then generate a standard script for it.
            if (String.IsNullOrEmpty(Program.Realm.Script))
            {
                //Instance a new method helper class
                ManagedScripting.CodeBuilding.MethodSetup method = new ManagedScripting.CodeBuilding.MethodSetup();
                string script = "";
                //Setup our method. All objects inheriting from BaseObject will have the standard
                //methods created for them.
                string[] names = new string[] { "OnCreate", "OnDestroy", "OnEnter", "OnExit" };
                foreach (string name in names)
                {
                    method = new ManagedScripting.CodeBuilding.MethodSetup();
                    method.Name = name;
                    method.ReturnType = "void";
                    method.IsOverride = true;
                    method.Modifier = ManagedScripting.CodeBuilding.ClassGenerator.Modifiers.Public;
                    method.Code = new string[] { "base." + method.Name + "();" };
                    script = script.Insert(Program.Realm.Script.Length, method.Create() + "\n");
                }
                Program.Realm.Script = script;
            }
        }

        private void btnNewRealm_Click(object sender, EventArgs e)
        {
            Program.Zone = new Zone();
            Program.Realm = new Realm();
            SetupScript();

            propertyRealm.SelectedObject = Program.Realm;
            lstZonesInRealm.Items.Clear();
        }

        private void btnSaveRealm_Click(object sender, EventArgs e)
        {
            string path = FileManager.GetDataPath(SaveDataTypes.Realms);
            string filename = System.IO.Path.Combine(path, Program.Realm.Name + ".realm");
            if (System.IO.File.Exists(filename))
            {
                DialogResult result = MessageBox.Show("File exists!\nOverwrite?", "Realm Explorer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;
            }

            FileManager.Save(filename, Program.Realm);

            if (!lstRealms.Items.Contains(Program.Realm.Name))
            lstRealms.Items.Add(Program.Realm.Name);
        }

        private void lstRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRealms.SelectedIndex == -1)
                return;

            string path = FileManager.GetDataPath(SaveDataTypes.Realms);
            string filename = System.IO.Path.Combine(path, lstRealms.SelectedItem.ToString() + ".realm");
            Program.Realm = (Realm)FileManager.Load(filename, Program.Realm);

            propertyRealm.SelectedObject = Program.Realm;
            lstZonesInRealm.Items.Clear();

            foreach (string file in System.IO.Directory.GetFiles(FileManager.GetDataPath(SaveDataTypes.Zones), "*.zone"))
            {
                Zone zone = new Zone();
                zone = (Zone)FileManager.Load(file, zone);

                if (zone.Realm == Program.Realm.Name)
                    lstZonesInRealm.Items.Add(zone.Name);
                else if (String.IsNullOrEmpty(zone.Realm))
                {
                    lstAvailableZones.Items.Add(zone.Name);
                }
            }
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
            string filename = System.IO.Path.Combine(path, lstRealms.SelectedItem.ToString() + ".realm");
            System.IO.File.Delete(filename);
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
            Program.ScriptEngine.AddReference(Application.StartupPath + "/MUDEngine.dll");
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
            ZoneBuilder form = new ZoneBuilder();
            form.Show();
            this.Hide();
            while (form.Created)
                Application.DoEvents();

            form = null;

            this.Show();
        }

        private void btnPlaceZone_Click(object sender, EventArgs e)
        {
            if (lstAvailableZones.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Zone to add!", "Realm Explorer");
                return;
            }

            string path = FileManager.GetDataPath(SaveDataTypes.Zones);
            string filename = System.IO.Path.Combine(path, lstAvailableZones.SelectedItem.ToString() + ".zone");
            Zone zone = new Zone();
            zone = (Zone)FileManager.Load(filename, zone);
            zone.Realm = Program.Realm.Name;
            FileManager.Save(filename, zone);

            Program.Realm.Zones.Add(zone);
            lstZonesInRealm.Items.Add(zone.Name);
        }
    }
}
