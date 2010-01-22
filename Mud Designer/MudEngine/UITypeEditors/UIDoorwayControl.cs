using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;
using MudDesigner.MudEngine.GameManagement;
using MudDesigner.MudEngine.FileSystem;

namespace MudDesigner.MudEngine.UITypeEditors
{
    public partial class UIDoorwayControl : Form
    {
        Room _Room;
        Door _Door;

        public UIDoorwayControl(Room room)
        {
            InitializeComponent();

            _Room = new Room();
            _Room = room;

            foreach (Door d in _Room.Doorways)
            {
                lstInstalledDoors.Items.Add(d.TravelDirection.ToString());
            }
        }

        private void btnAddDoorway_Click(object sender, EventArgs e)
        {
            _Door = new Door();
            Array directions = Enum.GetValues(typeof(AvailableTravelDirections));
            bool IsAvailableDirection = true;

            foreach (int direction in directions)
            {
                //None value; Don't use the None value when linking rooms.
                if (direction == 0)
                    continue;

                string availableDirection = Enum.GetName(typeof(AvailableTravelDirections), direction);

                if (_Room.DoorwayExist(availableDirection))
                    IsAvailableDirection = false;
                else
                {
                    IsAvailableDirection = true;

                    _Door.TravelDirection = (AvailableTravelDirections)Enum.Parse(typeof(AvailableTravelDirections), availableDirection);
                    lstInstalledDoors.Items.Add(_Door.TravelDirection.ToString());
                    _Room.Doorways.Add(_Door);
                    propertyDoorway.SelectedObject = _Door;
                    break;
                }
            }

            if (!IsAvailableDirection)
            {
                MessageBox.Show("There are no available doorways remaining to add.", "Mud Designer");
                return;
            }
        }

        private void propertyDoorway_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "TravelDirection")
            {
                if (e.ChangedItem.Value.ToString() == "None")
                {
                    MessageBox.Show("This is not a valid Direction.", "Mud Designer");
                    _Door = (Door)propertyDoorway.SelectedObject;
                    _Door.TravelDirection = (AvailableTravelDirections)e.OldValue;
                    propertyDoorway.Refresh();
                    return;
                }
                else if (_Room.DoorwayExist(e.ChangedItem.Value.ToString()))
                {
                    MessageBox.Show("This direction has already been installed into the room. Please select another direction.", "Mud Designer");
                    _Door = (Door)propertyDoorway.SelectedObject;
                    _Door.TravelDirection = (AvailableTravelDirections)e.OldValue;
                    propertyDoorway.Refresh();
                    return;
                }
            }
            else if (e.ChangedItem.Label == "ConnectedRoom")
            {
                string zonePath = "";

                if (_Room.Realm == "No Realm Associated.")
                {
                    zonePath = FileManager.GetDataPath(SaveDataTypes.Zones);
                }
                else
                    zonePath = FileManager.GetDataPath(_Room.Realm, _Room.Zone);

                zonePath = Path.Combine(zonePath, _Room.Zone);
                string roomPath = Path.Combine(zonePath, "Rooms");
                string roomFile = e.ChangedItem.Value.ToString() + ".Room";
                string filePath = Path.Combine(roomPath, roomFile);

                if (!File.Exists(filePath))
                {
                    DialogResult result = 
                        MessageBox.Show("Warning! The supplied Room does not exists, would you like the Designer to automatically generate it for you?", "Mud Designer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                        _Door = (Door)propertyDoorway.SelectedObject;
                        _Door.ConnectedRoom = (string)e.OldValue;
                        propertyDoorway.Refresh();
                        return;
                    }
                    else
                    {
                        Room r = new Room();
                        r.Name = e.ChangedItem.Value.ToString();
                        r.Realm = _Room.Realm;
                        r.Zone = _Room.Zone;
                        r.Save(Path.Combine(roomPath, r.Filename));
                    }

                }
            }
        }

        private void lstInstalledDoors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstInstalledDoors.SelectedIndex == -1)
                return;

            foreach (Door door in _Room.Doorways)
            {
                if (door.TravelDirection.ToString() == lstInstalledDoors.SelectedItem.ToString())
                {
                    _Door = door;
                    propertyDoorway.SelectedObject = _Door;
                    break;
                }
            }
        }
    }
}
