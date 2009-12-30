﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//MudEngine
using MudDesigner.MudEngine;
using MudDesigner.MudEngine.Attributes;
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;

using ManagedScripting;

namespace MudDesigner.Editors
{
    public partial class ZoneBuilder : Form
    {
        internal bool IsEditingExisting = false;
        bool IsRealmLoaded = false;
        bool IsZoneLoaded = false;
        bool IsCreatingZone = false;
        bool IsRoomLoaded = false;
        bool IsCreatingRoom = false;

        List<Zone> _Zones = new List<Zone>();

        public ZoneBuilder()
        {
            InitializeComponent();
            //Reinstance all of our environments
            Program.Realm = new Realm();

            //If we have a default Realm selected, lets load it
            if (Program.Settings.DefaultRealm != null)
            {
                //Load it
                string realmPath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), Program.Settings.DefaultRealm.Name);
                string realmFile = Path.Combine(realmPath, Program.Settings.DefaultRealm.Filename);
                Program.Realm = (Realm)FileManager.Load(realmFile, Program.Realm);

                this.Text = "Zone Builder: (" + Program.Realm.Name + ")";
                IsRealmLoaded = true;

                //realm is loaded, now clear out the list of zones & rooms and show the zones contained
                //within the new realm
                lstZones.Items.Clear();
                lstRooms.Items.Clear();
                _Zones.Clear();
                string[] files = Directory.GetFiles(realmPath, "*.zone", SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    string filename = Path.GetFileName(file);
                    if (Program.Realm.Zones.Contains(filename))
                    {
                        Zone zone = new Zone();
                        zone = (Zone)FileManager.Load(file, zone);
                        zone.RefreshRoomList();
                        _Zones.Add(zone);
                        lstZones.Items.Add(zone.Name);
                    }
                    else
                        continue;
                }
            }
        }

        private void btnNewZone_Click(object sender, EventArgs e)
        {
            //Check if a realm is loaded
            if (!IsRealmLoaded)
            {
                MessageBox.Show("You need to select a Realm to create a Zone in first.",
                    "Zone Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (IsCreatingZone)
            {
                DialogResult result = MessageBox.Show("You are currently editing a new Zone, are you sure you want to re-create a new Zone?",
                    "Zone Builder", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;
            }

            //re-instance our zone and room
            Program.Zone = new Zone();

            //Assign the realm to the zone
            Program.Zone.Realm = Program.Realm.Name;

            //Assign to the property view
            propertyZone.SelectedObject = Program.Zone;

            //Zone is in 'create' mode, and has not been saved/loaded fully yet.
            IsZoneLoaded = false;
            IsCreatingZone = true;
            //Reset our room, as we are creating a new zone, there will be no rooms.
            propertyRoom.SelectedObject = null;
            lstRooms.Items.Clear();
        }

        private void btnSaveZone_Click(object sender, EventArgs e)
        {
            //Get the realm and zone path setup first
            string realmPath = System.IO.Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), Program.Realm.Name);
            string zonePath = Path.Combine(realmPath, Program.Zone.Name);
            string zoneFile = Path.Combine(zonePath, Program.Zone.Filename);

            if (!Directory.Exists(zonePath))
                Directory.CreateDirectory(zonePath);

            //adjust our realm
            if (!Program.Realm.Zones.Contains(Program.Zone.Filename))
                Program.Realm.Zones.Add(Program.Zone.Filename);

            //save the Zone
            FileManager.Save(zoneFile, Program.Zone);

            //Re-save the realm, as we have changed it's Zone collection
            FileManager.Save(Path.Combine(realmPath, Program.Realm.Filename), Program.Realm);

            //add it to the list box if it isn't already there
            if (!lstZones.Items.Contains(Program.Zone.Name))
                lstZones.Items.Add(Program.Zone.Name);

            //Store it in our collection.
            foreach (Zone zone in _Zones)
            {
                //Check if we have a zone that exists already
                //with that name, incase the user is just editing it.
                if (zone.Name == Program.Zone.Name)
                {
                    //remove it.
                    _Zones.Remove(zone);
                    break;
                }
            }
            //Add the zone to our collection
            _Zones.Add(Program.Zone);
            IsZoneLoaded = true;
            IsCreatingZone = false;
        }

        private void btnSelectRealm_Click(object sender, EventArgs e)
        {
            //instance a form displaying all of the realms
            ExistingRealms form = new ExistingRealms();
            form.Text = "Zones owning Realm.";
            //show the form
            form.Show();
            this.Hide();

            //wait for it to be closed
            while (form.Created)
                Application.DoEvents();

            //Restore the zone builder
            this.Show();

            //Check if we have selected a realm or not
            //if not, then cancel creating the zone
            if (form.lstRealms.SelectedIndex == -1)
                return;

            //Load it
            string realmPath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), form._RealmName);
            string realmFile = Path.Combine(realmPath, form._RealmFilename);
            Program.Realm = (Realm)FileManager.Load(realmFile, Program.Realm);

            this.Text = "Zone Builder: (" + Program.Realm.Name + ")";
            IsRealmLoaded = true;
            IsZoneLoaded = false;
            IsRoomLoaded = false;

            //realm is loaded, now clear out the list of zones & rooms and show the zones contained
            //within the new realm
            lstZones.Items.Clear();
            lstRooms.Items.Clear();
            _Zones.Clear();
            string[] files = Directory.GetFiles(realmPath, "*.zone", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                string filename = Path.GetFileName(file);
                if (Program.Realm.Zones.Contains(filename))
                {
                    Zone zone = new Zone();
                    zone = (Zone)FileManager.Load(file, zone);
                    zone.RefreshRoomList();
                    _Zones.Add(zone);
                    lstZones.Items.Add(zone.Name);
                }
                else
                    continue;
            }
        }

        private void btnLoadZone_Click(object sender, EventArgs e)
        {
            if (!IsRealmLoaded)
            {
                MessageBox.Show("You must first select a realm in order to view Zones for loading.",
                    "Zone Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (lstZones.SelectedIndex == -1)
            {
                MessageBox.Show("Select a Zone to load first.", "Zone Builder", MessageBoxButtons.OK);
                return;
            }

            if (IsCreatingZone)
            {
                DialogResult result = MessageBox.Show("You are currently editing a new Zone, are you sure you want to re-create a new Zone?",
                    "Zone Builder", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;
            }

            //Loop through the collection we generated when we selected our realm
            //and find the zone that the user selected to load
            foreach (Zone zone in _Zones)
            {
                if (zone.Name == lstZones.SelectedItem.ToString())
                {
                    Program.Zone = zone;
                    Program.Zone.RefreshRoomList();
                    break;
                }
            }

            //Loop through the zones collection of rooms and add them to the
            //room list.
            lstRooms.Items.Clear();
            foreach (Room room in Program.Zone.Rooms)
            {
                lstRooms.Items.Add(room.Name);
            }

            propertyZone.SelectedObject = Program.Zone;
            IsZoneLoaded = true;
            IsCreatingZone = false;
            IsRoomLoaded = false;
        }

        private void btnNewRoom_Click(object sender, EventArgs e)
        {
            if (!IsZoneLoaded)
            {
                string msg = "";
                if (IsCreatingZone)
                {
                    msg = "You will need to save the Zone prior to creating a new Room.";
                }
                else
                {
                    msg = "You will need to load a Zone prior to creating a new Room.";
                }
                    MessageBox.Show(msg,
                        "Zone Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
            }

            Program.Room = new Room();
            Program.Room.Zone = Program.Zone.Name;
            propertyRoom.SelectedObject = Program.Room;
            IsRoomLoaded = false;
            IsCreatingRoom = true;
        }

        private void btnSaveRoom_Click(object sender, EventArgs e)
        {
            if (!IsRoomLoaded)
            {
                if (!IsCreatingRoom)
                {
                    MessageBox.Show("You must create a new Room in order to save.", "Zone Builder", MessageBoxButtons.OK);
                    return;
                }
            }
            //Get the realm, zone & room path setup first
            string realmPath = System.IO.Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), Program.Realm.Name);
            string zonePath = Path.Combine(realmPath, Program.Zone.Name);
            string roomFile = Path.Combine(zonePath, Program.Room.Filename);

            //adjust our Zone. Zones are added to the Zone.Rooms
            //collection when the Zone is instanced, anything created
            //after the Zone is instanced will need to be added manually.
            //Zone does not need to be re-saved like Realms would need to be
            //as Zones load all of the Rooms it contains on its own when instanced.
            if (Program.Zone.GetRoom(Program.Room.Name) == null)
                Program.Zone.Rooms.Add(Program.Room);

            //save the Room
            FileManager.Save(roomFile, Program.Room);

            //add it to the list box if it isn't already there
            if (!lstRooms.Items.Contains(Program.Room.Name))
                lstRooms.Items.Add(Program.Room.Name);

            IsRoomLoaded = true;
            IsCreatingRoom = false;
        }

        private void btnLoadRoom_Click(object sender, EventArgs e)
        {
            if (!IsZoneLoaded)
            {
                MessageBox.Show("You must first load a Zone in order to aquire Rooms for loading.",
                    "Zone Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (lstRooms.SelectedIndex == -1)
            {
                MessageBox.Show("Select a Room to load first.", "Zone Builder", MessageBoxButtons.OK);
                return;
            }

            if (IsCreatingZone)
            {
                MessageBox.Show("You are currently editing a new Zone, you must save the Zone prior to attempting to load a Room.",
                    "Zone Builder", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                return;
            }

            bool found = false;
            foreach (Room room in Program.Zone.Rooms)
            {
                if (room.Name == lstRooms.SelectedItem.ToString())
                {
                    Program.Room = room;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                MessageBox.Show("Failed loading room. Unable to locate the selected room within the Zone.",
                    "Zone Builder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            propertyRoom.SelectedObject = Program.Room;
            IsRoomLoaded = true;
            IsCreatingRoom = false;

            //assign the doorways to the UI
            foreach (Door door in Program.Room.InstalledDoors)
            {
                AvailableTravelDirections travelDirection = door.TravelDirection;

                Control[] controls = this.Controls.Find("btn" + travelDirection.ToString(), true);

                controls[0].Text = travelDirection.ToString() + "\nInstalled";

                doorwayHelp.SetToolTip(controls[0], travelDirection.ToString() + " Doorway Installed.\n\n"
                    + door.TravelRoom.Realm + "->" + door.TravelRoom.Zone + "->" + door.TravelRoom.Room + "->" + door.TravelRoom.TravelDirection.ToString());

            }
        }

        private void doorwayMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (sender is ContextMenuStrip)
            {
                ContextMenuStrip mnu = (ContextMenuStrip)sender;
                Button btn = (Button)mnu.SourceControl;
                mnuInstallDoor.Text = "Install " + btn.Text + " doorway.";
                
            }
        }

        private void installDoorwayToAnotherZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsRoomLoaded)
            {
                MessageBox.Show("You must load a Room prior to any doorway installation attempts.",
                    "Zone Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //Split the menus text into an array, since the menu
            //is dynamically generating it's text, depending on the
            //button it is right clicked over.
            string[] menuWords = mnuInstallDoor.Text.Split(' ');

            //Create an array of all available travel directions
            AvailableTravelDirections travelDirection = new AvailableTravelDirections();
            Array values = Enum.GetValues(typeof(AvailableTravelDirections));

            //Loop through each word in the menu, until we find a word
            //that matches a travel direction.
            foreach (string word in menuWords)
            {
                foreach (int value in values)
                {
                    //get a copy of the current travel direction in the array
                    string displayName = Enum.GetName(typeof(AvailableTravelDirections), value);

                    //check if the current travel direction matches the current word in
                    //the context menu
                    if (displayName == word)
                    {
                        //If we have a match, store the travel direction so
                        //we can use it on our Doorway manager
                        travelDirection = (AvailableTravelDirections)Enum.Parse(typeof(AvailableTravelDirections), displayName);
                    }
                }
            }

            //Instance a new Doorway manager
            DoorwayManager form = new DoorwayManager(travelDirection);
            form.Show();
            this.Hide();

            while (form.Created)
                Application.DoEvents();

            this.Show();

            Control[] controls = this.Controls.Find("btn" + travelDirection.ToString(), true);

            controls[0].Text = travelDirection.ToString() + "\nInstalled";
            doorwayHelp.SetToolTip(controls[0], travelDirection.ToString() + " Doorway Installed.\n\n"
                + form.linkedRealm.Name + "->" + form.linkedZone.Name + "->" + form.linkedRoom.Name + "->" + form.TravelDirection.ToString());

            Door door = new Door(travelDirection);
            Door.ConnectedRoom d = new Door.ConnectedRoom();
            d.Realm = form.linkedRealm.Name;
            d.Room = form.linkedRoom.Name;
            d.Zone = form.linkedZone.Name;
            door.TravelRoom = d;
            foreach (Door obj in Program.Room.InstalledDoors)
            {
                if (obj.TravelDirection == door.TravelDirection)
                {
                    Program.Room.InstalledDoors.Remove(obj);
                    break;
                }
            }
            Program.Room.InstalledDoors.Add(door);
        }
    }
}
