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
        Realm _Realm;
        List<Zone> _AvailableZones;
        ScriptingEngine _ScriptEngine;

        public frmMain()
        {
            InitializeComponent();
            _Zone = new Zone();
            _Realm = new Realm();
            _AvailableZones = new List<Zone>();
            _ScriptEngine = new ScriptingEngine();
            _ScriptEngine.CompileStyle = ManagedScripting.Compilers.BaseCompiler.ScriptCompileStyle.CompileToMemory;
            _ScriptEngine.KeepTempFiles = false;

            BuildZoneLists();

            SetupScript();

            propertyRealm.SelectedObject = _Realm;

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
            if (String.IsNullOrEmpty(_Realm.Script))
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
                    script = script.Insert(_Realm.Script.Length, method.Create() + "\n");
                }
                _Realm.Script = script;
            }
        }

        private void btnNewRealm_Click(object sender, EventArgs e)
        {
            _Zone = new Zone();
            _Realm = new Realm();
            SetupScript();

            propertyRealm.SelectedObject = _Realm;
            lstZonesInRealm.Items.Clear();
        }

        private void btnSaveRealm_Click(object sender, EventArgs e)
        {
            string path = Engine.GetDataPath(Engine.SaveDataTypes.Realms);
            string filename = System.IO.Path.Combine(path, _Realm.Name + ".realm");
            if (System.IO.File.Exists(filename))
            {
                DialogResult result = MessageBox.Show("File exists!\nOverwrite?", "Realm Explorer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;
            }

            MUDEngine.FileSystem.FileSystem.Save(filename, _Realm);

            if (!lstRealms.Items.Contains(_Realm.Name))
            lstRealms.Items.Add(_Realm.Name);
        }

        private void lstRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRealms.SelectedIndex == -1)
                return;

            string path = Engine.GetDataPath(Engine.SaveDataTypes.Realms);
            string filename = System.IO.Path.Combine(path, lstRealms.SelectedItem.ToString() + ".realm");
            _Realm = (Realm)MUDEngine.FileSystem.FileSystem.Load(filename, _Realm);

            propertyRealm.SelectedObject = _Realm;
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
                txtScript.Text = _Realm.Script;
            }
        }

        private void btnValidateScript_Click(object sender, EventArgs e)
        {
            _ScriptEngine.Compiler = ManagedScripting.ScriptingEngine.CompilerSelections.SourceCompiler;
            _ScriptEngine.AddReference(Application.StartupPath + "/MUDEngine.dll");
            string code = "namespace MUDEngine.Objects.Environment\n"
                + "{\n"
                + "  public class " + _Realm.Name.Replace(" ", "") + " : Realm\n"
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
#if DEBUG
            info.FileName = @"E:\Codeplex\MudDesigner\MudDesigner\bin\Debug\Mud Designer.exe";
#else
            info.FileName = "Mud Designer.exe";
#endif
            info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

            process.StartInfo = info;
            process.Start();
            this.Hide();
            process.WaitForExit();
            this.Show();
            process = null;
        }
    }
}
