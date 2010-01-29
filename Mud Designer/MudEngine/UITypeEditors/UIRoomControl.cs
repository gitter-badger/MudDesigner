﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;

namespace MudDesigner.MudEngine.UITypeEditors
{
    public partial class UIRoomControl : Form
    {
        public bool IsSaved;
        Room _Room;
        Zone _Zone;
        string savePath = "";
        public List<Room> Rooms { get; set; }

        public UIRoomControl(Zone zone)
        {
            InitializeComponent();
            IsSaved = true;
            _Room = new Room();
            Rooms = new List<Room>();
            _Zone = zone;

            string projectPath = Path.Combine(Application.StartupPath, "Project");
            string zonesPath = Path.Combine(projectPath, "Zones");
            string realmsPath = Path.Combine(projectPath, "Realms");
            savePath = "";

            if (zone.Realm == "No Realm Associated.")
            {
                //Project/Zones/ZoneName
                savePath = Path.Combine(zonesPath, zone.Name);
                //Project/Zones/ZoneName/Rooms
                savePath = Path.Combine(savePath, "Rooms");
            }
            else
            {
                //Project/Realms/RealmName
                savePath = Path.Combine(realmsPath, zone.Realm);
                //Project/Realms/RealmName/Zones
                savePath = Path.Combine(savePath, "Zones");
                //Project/Realms/RealmName/Zones/ZoneName
                savePath = Path.Combine(savePath, zone.Name);
                //Project/Realms/RealmName/Zones/ZoneName/Rooms
                savePath = Path.Combine(savePath, "Rooms");
            }

            Program.CurrentEditor = this;
        }

        private bool CheckSavedState()
        {
            if (IsSaved)
                return true;

            DialogResult result = MessageBox.Show(_Room.Name + " has not been saved! Do you wish to save it?", "Mud Designer", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.No)
                return true;
            else if (result == DialogResult.Cancel)
                return false;
            else
                SaveSelected();

            return true;
        }

        public void SaveSelected()
        {
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            _Room = (Room)propertyRoom.SelectedObject;

            string filename = Path.Combine(savePath, _Room.Filename);
            FileManager.Save(filename, _Room);

            if (!lstRooms.Items.Contains(_Room.Filename))
                lstRooms.Items.Add(_Room.Filename);

            Rooms.Add(_Room);
            IsSaved = true;
        }

        internal void RefreshRoomList()
        {
            if (Directory.Exists(savePath))
            {
                lstRooms.Items.Clear();
                string[] files = Directory.GetFiles(savePath);

                foreach (string file in files)
                {
                    lstRooms.Items.Add(Path.GetFileName(file));
                }
            }
        }

        private void propertyRoom_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            IsSaved = false;
            if (propertyRoom.SelectedObject is BaseObject)
            {
                BaseObject obj = (BaseObject)propertyRoom.SelectedObject;

                //Don't auto-save if we haven't assigned a valid name
                if (obj.Name == "New " + obj.GetType().Name)
                    return;

                if (e.ChangedItem.Label == "Name")
                {
                    if (e.ChangedItem.Value != e.OldValue)
                    {
                        if (File.Exists(Path.Combine(savePath, e.ChangedItem.Value + ".Room")))
                        {
                            MessageBox.Show("A Room with that name already exists!", "Mud Designer");
                            _Room = (Room)propertyRoom.SelectedObject;
                            _Room.Name = e.OldValue.ToString();
                            propertyRoom.Refresh();
                            return;
                        }
                        StringBuilder filename = new StringBuilder();
                        filename.Append(e.OldValue);
                        filename.Append(".room");
                        string fullFilename = Path.Combine(savePath, filename.ToString());
                        if (File.Exists(fullFilename))
                        {
                            File.Delete(Path.Combine(savePath, fullFilename));
                            lstRooms.Items.RemoveAt(lstRooms.Items.IndexOf(e.OldValue + ".Room"));
                        }

                        //loop through each doorway in the current room
                        //we will need to update the connected Rooms incase this room changes
                        //its roomname
                        foreach (Door d in _Room.Doorways)
                        {
                            //Load the connected room
                            Room r = new Room();
                            fullFilename = Path.Combine(savePath, d.ConnectedRoom + ".room");
                            r = (Room)r.Load(fullFilename);
                            //Loop though each of its doorways to see if any of them are attached
                            //to are old room name
                            foreach (Door renamedDoor in r.Doorways)
                            {
                                if (renamedDoor.ConnectedRoom == e.OldValue.ToString())
                                {
                                    //Change the old room name to the new room name
                                    renamedDoor.ConnectedRoom = _Room.Name;
                                }
                            }
                            //All the doorways for this room have been corrected, now save it.
                            r.Save(fullFilename);
                        }
                    }
                }

            }
            SaveSelected();
        }

        private void btnNewRoom_Click(object sender, EventArgs e)
        {
            if (CheckSavedState())
            {
                _Room = new Room();
                _Room.Zone = _Zone.Name;
                _Room.Realm = _Zone.Realm;
                propertyRoom.SelectedObject = _Room;

                IsSaved = false;
            }
        }

        private void UIRoomControl_Load(object sender, EventArgs e)
        {
            RefreshRoomList();
        }

        private void lstRooms_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstRooms.SelectedIndex == -1)
                return;

            if (!CheckSavedState())
            {
                return;
            }

            string roomName = lstRooms.SelectedItem.ToString();
            string zonePath = "";
            string zoneRoomPath = "";
            string roomFile = "";

            if (_Zone.Realm == "No Realm Associated.")
            {
                zonePath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Zones), _Zone.Name);
            }
            else
            {
                zonePath = FileManager.GetDataPath(_Zone.Realm, _Zone.Name);
            }
            zoneRoomPath = Path.Combine(zonePath, "Rooms");
            roomFile = Path.Combine(zoneRoomPath, roomName);
            _Room = new Room();
            _Room = (Room)_Room.Load(roomFile);
            propertyRoom.SelectedObject = _Room;
        }

        private void UIRoomControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CheckSavedState())
                e.Cancel = true;
        }
    }
}
