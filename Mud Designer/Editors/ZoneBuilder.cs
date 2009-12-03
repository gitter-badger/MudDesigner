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
        public ZoneBuilder()
        {
            InitializeComponent();
            Program.Room = new Room();
            Program.Zone = new Zone();
            Program.ScriptEngine = new ScriptingEngine();
            Program.ScriptEngine.CompileStyle = ManagedScripting.Compilers.BaseCompiler.ScriptCompileStyle.CompileToMemory;
            Program.ScriptEngine.Compiler = ScriptingEngine.CompilerSelections.SourceCompiler;

            propertyZone.SelectedObject = Program.Zone;
            txtScript.Text = Program.Zone.Script;

            string[] rooms = System.IO.Directory.GetFiles(FileManager.GetDataPath(SaveDataTypes.Rooms), "*.room");

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
            string path = FileManager.GetDataPath(SaveDataTypes.Zones);
            string filename = System.IO.Path.Combine(path, Program.Zone.Name + ".zone");

            FileManager.Save(filename, Program.Zone);
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
    }
}
