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
        Zone _CurrentZone;
        Room _CurrentRoom;
        ScriptingEngine _ScriptEngine;

        public ZoneBuilder()
        {
            InitializeComponent();
            _CurrentRoom = new Room();
            _CurrentZone = new Zone();
            _ScriptEngine = new ScriptingEngine();
            _ScriptEngine.CompileStyle = ManagedScripting.Compilers.BaseCompiler.ScriptCompileStyle.CompileToMemory;
            _ScriptEngine.Compiler = ScriptingEngine.CompilerSelections.SourceCompiler;

            propertyZone.SelectedObject = _CurrentZone;
            txtScript.Text = _CurrentZone.Script;

            string[] rooms = System.IO.Directory.GetFiles(MudEngine.Engine.GetDataPath(Engine.SaveDataTypes.Rooms), "*.room");

            foreach (string room in rooms)
            {
                lstRooms.Items.Add(System.IO.Path.GetFileNameWithoutExtension(room));
            }
        }

        private void btnRoomEditor_Click(object sender, EventArgs e)
        {
            DialogResult result;
            string argument = "";

            if (lstRooms.SelectedItem != null)
            {
                result = MessageBox.Show("You have a room selected, are you wanting to edit it?",
                    "Zone Builder", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        argument += " \"room=" + lstRooms.SelectedItem.ToString() + ".room\"";
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            RoomDesigner form = new RoomDesigner();

            form.Show();

            MessageBox.Show("Broke.");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSaveZone_Click(object sender, EventArgs e)
        {
            string path = Engine.GetDataPath(Engine.SaveDataTypes.Zones);
            string filename = System.IO.Path.Combine(path, _CurrentZone.Name + ".zone");

            FileSystem.Save(filename, _CurrentZone);
        }

        private void btnNewZone_Click(object sender, EventArgs e)
        {
            _CurrentZone = new Zone();
            _CurrentRoom = new Room();
            propertyZone.SelectedObject = _CurrentZone;
        }

        private void btnValidateScript_Click(object sender, EventArgs e)
        {
            _ScriptEngine.Compiler = ManagedScripting.ScriptingEngine.CompilerSelections.SourceCompiler;
            _ScriptEngine.AddReference(Application.StartupPath + "/MUDEngine.dll");

            string code = "namespace MudDesigner.MudEngine.Objects.Environment\n"
                + "{\n"
                + "  public class " + _CurrentZone.Name.Replace(" ", "") + " : Zone\n"
                + "  {\n"
                + "     " + txtScript.Text + "\n"
                + "  }\n"
                + "}\n";
            MessageBox.Show(_ScriptEngine.Compile(code), "Script Compiling", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
