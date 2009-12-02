using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ManagedScripting;
using MUDEngine.Objects.Environment;
using MUDEngine;

namespace RealmExplorer
{
    public partial class frmMain : Form
    {
        Zone _Zone;
        Realm _CurrentRealm;
        List<Zone> _AvailableZones;
        ScriptingEngine _ScriptEngine;

        public frmMain()
        {
            InitializeComponent();
            _Zone = new Zone();
            _CurrentRealm = new Realm();
            _AvailableZones = new List<Zone>();
            _ScriptEngine = new ScriptingEngine();
            _ScriptEngine.CompileStyle = ManagedScripting.Compilers.BaseCompiler.ScriptCompileStyle.CompileToMemory;
            _ScriptEngine.KeepTempFiles = false;

            BuildZoneLists();

            SetupScript();

            propertyRealm.SelectedObject = _CurrentRealm;

            string[] existingRealms = System.IO.Directory.GetFiles(Engine.GetDataPath(Engine.SaveDataTypes.Realms));
            foreach (string realm in existingRealms)
                lstRealms.Items.Add(System.IO.Path.GetFileNameWithoutExtension(realm));
        }

        private void BuildZoneLists()
        {
            string[] zones = System.IO.Directory.GetFiles(Engine.GetDataPath(Engine.SaveDataTypes.Zones), "*.zone");
            bool available = true;
            lstAvailableZones.Items.Clear();
            lstZonesInRealm.Items.Clear();

            foreach (string zone in zones)
            {
                string[] realms = System.IO.Directory.GetFiles(Engine.GetDataPath(Engine.SaveDataTypes.Realms), "*.realm");

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
            if (String.IsNullOrEmpty(_CurrentRealm.Script))
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
                    script = script.Insert(_CurrentRealm.Script.Length, method.Create() + "\n");
                }
                _CurrentRealm.Script = script;
            }
        }

        private void btnNewRealm_Click(object sender, EventArgs e)
        {
            _Zone = new Zone();
            _CurrentRealm = new Realm();
            SetupScript();

            propertyRealm.SelectedObject = _CurrentRealm;
            lstZonesInRealm.Items.Clear();
        }

        private void btnSaveRealm_Click(object sender, EventArgs e)
        {
            string path = Engine.GetDataPath(Engine.SaveDataTypes.Realms);
            string filename = System.IO.Path.Combine(path, _CurrentRealm.Name + ".realm");
            if (System.IO.File.Exists(filename))
            {
                DialogResult result = MessageBox.Show("File exists!\nOverwrite?", "Realm Explorer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;
            }

            MUDEngine.FileSystem.FileSystem.Save(filename, _CurrentRealm);

            if (!lstRealms.Items.Contains(_CurrentRealm.Name))
            lstRealms.Items.Add(_CurrentRealm.Name);
        }

        private void lstRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRealms.SelectedIndex == -1)
                return;

            string path = Engine.GetDataPath(Engine.SaveDataTypes.Realms);
            string filename = System.IO.Path.Combine(path, lstRealms.SelectedItem.ToString() + ".realm");
            _CurrentRealm = (Realm)MUDEngine.FileSystem.FileSystem.Load(filename, _CurrentRealm);

            propertyRealm.SelectedObject = _CurrentRealm;
            lstZonesInRealm.Items.Clear();

            foreach (string file in System.IO.Directory.GetFiles(Engine.GetDataPath(Engine.SaveDataTypes.Zones), "*.zone"))
            {
                Zone zone = new Zone();
                zone = (Zone)MUDEngine.FileSystem.FileSystem.Load(file, zone);

                if (zone.Realm == _CurrentRealm.Name)
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

            string path = Engine.GetDataPath(Engine.SaveDataTypes.Realms);
            string filename = System.IO.Path.Combine(path, lstRealms.SelectedItem.ToString() + ".realm");
            System.IO.File.Delete(filename);
            lstRealms.Items.Remove(lstRealms.SelectedItem);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Script")
            {
                txtScript.Text = _CurrentRealm.Script;
            }
        }

        private void btnValidateScript_Click(object sender, EventArgs e)
        {
            _ScriptEngine.Compiler = ManagedScripting.ScriptingEngine.CompilerSelections.SourceCompiler;
            _ScriptEngine.AddReference(Application.StartupPath + "/MUDEngine.dll");
            string code = "namespace MUDEngine.Objects.Environment\n"
                + "{\n"
                + "  public class " + _CurrentRealm.Name.Replace(" ", "") + " : Realm\n"
                + "  {\n"
                + "     " + txtScript.Text + "\n"
                + "  }\n"
                + "}\n";
            MessageBox.Show(_ScriptEngine.Compile(code), "Script Compiling", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBuildZone_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();

            info.Arguments = "\"Run=Zone Builder.exe\"";
            info.Domain = "Zone Builder";
            info.FileName = "Zone Builder.exe";
            info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

            process.StartInfo = info;
            process.Start();
            this.Hide();
            process.WaitForExit();
            this.Show();
            process = null;
        }

        private void btnPlaceZone_Click(object sender, EventArgs e)
        {
            if (lstAvailableZones.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Zone to add!", "Realm Explorer");
                return;
            }

            string path = Engine.GetDataPath(Engine.SaveDataTypes.Zones);
            string filename = System.IO.Path.Combine(path, lstAvailableZones.SelectedItem.ToString() + ".zone");
            Zone zone = new Zone();
            zone = (Zone)MUDEngine.FileSystem.FileSystem.Load(filename, zone);
            zone.Realm = _CurrentRealm.Name;
            MUDEngine.FileSystem.FileSystem.Save(filename, zone);

            _CurrentRealm.Zones.Add(zone);
            lstZonesInRealm.Items.Add(zone.Name);
        }
    }
}
