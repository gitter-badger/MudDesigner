using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MUDEngine;
using MUDEngine.Objects.Environment;
using ManagedScripting;
using MUDEngine;
using MUDEngine.Objects.Environment;
using MUDEngine.FileSystem;

namespace ZoneBuilder
{
    public partial class frmMain : Form
    {
        Zone _CurrentZone;
        Room _CurrentRoom;

        public frmMain()
        {
            InitializeComponent();
            _CurrentRoom = new Room();
            _CurrentZone = new Zone();

            SetupScript();
        }

        private void SetupScript()
        {
            //Check if the realm script is empty. If so then generate a standard script for it.
            if (String.IsNullOrEmpty(_CurrentZone.Script))
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
                    script = script.Insert(_CurrentZone.Script.Length, method.Create() + "\n");
                }
                _CurrentZone.Script = script;
            }
        }

        private void btnRoomEditor_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();

            info.Arguments = "\"Run=Room Designer.exe\"";
            info.Domain = "Room Designer";
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
