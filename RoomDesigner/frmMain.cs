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
                if (Display != "None")
                    this.lstDirections.Items.Add(Display);
            }

        }

        private void btnCloseEditor_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lstDirections_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool IsFound = false;

            foreach (Door newDoor in room.InstalledDoors)
            {
                if (newDoor.TravelDirection.ToString() == lstDirections.SelectedItem.ToString())
                {
                    door = newDoor;
                    IsFound = true;
                    break;
                }
            }

            if (!IsFound)
                door = new Door();

            propertyDoor.SelectedObject = door;
        }

        private void propertyDoor_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (door.DoorState == Door.AvailableDoorStates.Installed)
            {
                foreach(Door newDoor in room.InstalledDoors)
                {
                    if (newDoor.TravelDirection == door.TravelDirection)
                    {
                        DialogResult result = MessageBox.Show("Door already exists! Overwrite it?", "Room Designer", MessageBoxButtons.YesNo);
                        if (result == DialogResult.No)
                            return;

                        room.InstalledDoors.Remove(newDoor);
                        room.InstalledDoors.Add(door);
                        break;
                    }
                    else
                    {
                        room.InstalledDoors.Add(door);
                    }
                }
                //Incase there are no existing doors, the foreach loop gets skipped.
                if (room.InstalledDoors.Count == 0)
                {/*
                    foreach (Enum e in door.TravelDirection)
                    {
                        if (e.ToString() == lstDirections.SelectedItem.ToString())
                        {
                            door.TravelDirection = e;
                        }
                    }
                  */
                }
            }
        }
    }
}
