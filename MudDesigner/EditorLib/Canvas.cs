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
    public partial class Canvas : UserControl
    {
        bool isMoving = false;
        internal Point location;
        RoomControl control;

        public Canvas()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
                return;

            location = e.Location;
            isMoving = true;
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                foreach (RoomControl roomControl in this.Controls)
                {
                    Point location = roomControl.location;

                    this.ParentForm.Text = location.X.ToString() + " : " + location.Y.ToString();
                }
            }
        }
    }
}
