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

            propertyZone.SelectedObject = _CurrentZone;
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
