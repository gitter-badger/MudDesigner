using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorLib
{
    public partial class RoomControl: UserControl
    {
        bool isMoving = false;
        internal Point location;
        RoomControl control;
        public Canvas ParentCanvas { get; set; }

        public MudDesigner.Engine.Environment.IRoom SelectedRoom;

        public RoomControl()
        {
            InitializeComponent();
        }

        private void RoomControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
                return;

            location = e.Location;
            isMoving = true;
        }

        private void RoomControl_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
        }

        private void RoomControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMoving || e.Button != System.Windows.Forms.MouseButtons.Left)
                return;

            control = (RoomControl)sender;
            control.Left += e.X - location.X;
            control.Top += e.Y - location.Y;
        }
    }
}
