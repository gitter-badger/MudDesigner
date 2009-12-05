using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//MudEngine
using MudDesigner.MudEngine;
using MudDesigner.MudEngine.Attributes;
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.Objects;
using MudDesigner.MudEngine.Objects.Environment;

using ManagedScripting;

namespace MudDesigner.Editors
{
    public partial class ZoneBuilder : Form
    {
        internal bool IsEditingExisting = false;

        public ZoneBuilder()
        {
            InitializeComponent();
        }

        private void btnRoomEditor_Click(object sender, EventArgs e)
        {
            DialogResult result;
            RoomDesigner form = new RoomDesigner();
            Program.Room = new Room();

            //Check if we have a room selected, if so we are going to ask if the user wants to edit it.
            if (lstRooms.SelectedItem != null)
            {
                result = MessageBox.Show("You have a room selected, are you wanting to edit it?",
                    "Zone Builder", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        form = new RoomDesigner(lstRooms.SelectedItem.ToString());
                        string filename = System.IO.Path.Combine(FileManager.GetDataPath(SaveDataTypes.Rooms), lstRooms.SelectedItem.ToString());
                        Program.Room = (Room)FileManager.Load(filename, Program.Room);
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            form.Show();
            this.Hide();
            while (form.Created)
                Application.DoEvents();

            form = null;

            this.Show();
        }

        private void btnSaveZone_Click(object sender, EventArgs e)
        {
            //build the save file name
            string path = FileManager.GetDataPath(SaveDataTypes.Zones);
            string zoneFile = System.IO.Path.Combine(path, Program.Zone.Name + ".zone");
            path = FileManager.GetDataPath(SaveDataTypes.Realms);
            string realmFile = System.IO.Path.Combine(path, Program.Realm.Name + ".realm");

            //get a copy of the currently running (but hidden) realm explorer
            RealmExplorer form = (RealmExplorer)Program.CurrentEditor;

            //Check if the currently selected realm currently contains the zone already
            //in its lists of zones. It could already be there and the user is just editing it.
            if (!form.lstZones.Items.Contains(Program.Zone.Name))
                form.lstZones.Items.Add(Program.Zone.Name);

            //Set the zones owning realm to the current realm
            Program.Zone.Realm = Program.Realm.Name;

            //Add the zone to the realms zone collection
            if (!Program.Realm.Zones.Contains(Program.Realm.GetZone(Program.Zone.Name)))
                Program.Realm.Zones.Add(Program.Zone);

            //Save the zone and modified realm.
            FileManager.Save(zoneFile, Program.Zone);
            FileManager.Save(realmFile, Program.Realm);

            //Reset the zone and room
            Program.Zone = new Zone();
            Program.Room = new Room();
            propertyZone.SelectedObject = Program.Zone;

            this.Close();
        }

        private void btnNewZone_Click(object sender, EventArgs e)
        {
            Program.Zone = new Zone();
            Program.Room = new Room();
            propertyZone.SelectedObject = Program.Zone;
        }

        private void btnValidateScript_Click(object sender, EventArgs e)
        {
            Program.ScriptEngine.Compiler = ManagedScripting.ScriptingEngine.CompilerSelections.SourceCompiler;
            Program.ScriptEngine.AddReference(Application.StartupPath + "/MUDEngine.dll");

            string code = "namespace MudDesigner.MudEngine.Objects.Environment\n"
                + "{\n"
                + "  public class " + Program.Zone.Name.Replace(" ", "") + " : Zone\n"
                + "  {\n"
                + "     " + txtScript.Text + "\n"
                + "  }\n"
                + "}\n";
            MessageBox.Show(Program.ScriptEngine.Compile(code), "Script Compiling", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ZoneBuilder_Load(object sender, EventArgs e)
        {
                Program.Room = new Room();
                Program.Zone = new Zone();
                Program.ScriptEngine = new ScriptingEngine();
                Program.ScriptEngine.CompileStyle = ManagedScripting.Compilers.BaseCompiler.ScriptCompileStyle.CompileToMemory;
                Program.ScriptEngine.Compiler = ScriptingEngine.CompilerSelections.SourceCompiler;

                if (IsEditingExisting)
                {
                    string path = FileManager.GetDataPath(SaveDataTypes.Zones);
                    RealmExplorer form = (RealmExplorer)Program.CurrentEditor;
                    string zoneFile = form.lstZones.SelectedItem.ToString() + ".zone";
                    string fullFilePath = System.IO.Path.Combine(path, zoneFile);

                    Program.Zone = (Zone)FileManager.Load(fullFilePath, Program.Zone);
                }

                propertyZone.SelectedObject = Program.Zone;
                txtScript.Text = Program.Zone.Script;

                string[] rooms = System.IO.Directory.GetFiles(FileManager.GetDataPath(SaveDataTypes.Rooms), "*.room");

                foreach (string room in rooms)
                {
                    lstRooms.Items.Add(System.IO.Path.GetFileNameWithoutExtension(room));
                }

        }
    }
}
