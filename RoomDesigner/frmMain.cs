using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MUDEngine.Objects.Environment;
using MUDEngine.Objects;
using MUDEngine;
using MUDEngine.FileSystem;

namespace RoomDesigner
{
    public partial class frmMain : Form
    {
        Room room;
        Door door;
        public frmMain()
        {
            InitializeComponent();

            room = new Room();
            door = new Door();
            propertyRoom.SelectedObject = room;
            AvailableTravelDirections type = new AvailableTravelDirections();

            Array Values = System.Enum.GetValues(type.GetType());

            foreach (int Value in Values)
            {
                string Display = Enum.GetName(type.GetType(), Value);
                this.lstDirections.Items.Add(Display);
            }

        }

        private void btnCloseEditor_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lstDirections_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyDoor.SelectedObject = door;
        }
    }
}
