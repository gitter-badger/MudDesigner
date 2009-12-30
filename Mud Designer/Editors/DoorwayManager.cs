using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameManagement;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;

namespace MudDesigner.Editors
{
    public partial class DoorwayManager : Form
    {
        internal List<Room> rooms;
        internal List<Zone> zones;
        internal List<Realm> realms;
        internal Room linkedRoom = new Room();
        internal Zone linkedZone = new Zone();
        internal Realm linkedRealm = new Realm();
        internal AvailableTravelDirections TravelDirection = AvailableTravelDirections.None;

        public DoorwayManager(AvailableTravelDirections TravelDirection)
        {
            InitializeComponent();
            realms = new List<Realm>();
            rooms = new List<Room>();
            zones = new List<Zone>();

            //set the window title to show the room being connected to
            //along with the travel direction
            this.Text += ": " + Program.Room.Name + "->" + TravelDirection.ToString();

            //get the realm paths, and find all the realm files
            string realmRoot = FileManager.GetDataPath(SaveDataTypes.Realms);
            string[] realmFiles = Directory.GetFiles(realmRoot, "*.realm", SearchOption.AllDirectories);

            //loop through each realm file found, load the realm 
            ///and place it in the realm collection
            foreach (string file in realmFiles)
            {
                Realm r = new Realm();
                r = (Realm)FileManager.Load(file, r);
                realms.Add(r);
                lstRealms.Items.Add(r.Name);
            }
        }

        private void lstRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRealms.SelectedIndex == -1)
                return;

            string realmPath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), lstRealms.SelectedItem.ToString());
            string[] files = Directory.GetFiles(realmPath, "*.zone", SearchOption.AllDirectories);

            //only show the progress bar if there is a large
            //number of zones to load.
            progressBar1.Value = 0;
            if (files.Length > 0)
                progressBar1.Visible = true;

            progressBar1.Maximum = files.Length * 2;

            foreach (string file in files)
            {
                Zone z = new Zone();
                z = (Zone)FileManager.Load(file, z);
                zones.Add(z);
                lstZones.Items.Add(z.Name);
                progressBar1.Increment(1);
            }

            foreach (Realm realm in realms)
            {
                if (realm.Name == lstRealms.SelectedItem.ToString())
                {
                    linkedRealm = realm;
                    break;
                }
                progressBar1.Increment(1);
            }

            progressBar1.Visible = false;
        }

        private void lstZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstZones.SelectedIndex == -1)
                return;

            string realmPath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), lstRealms.SelectedItem.ToString());
            string zonePath = Path.Combine(realmPath, lstZones.SelectedItem.ToString());
            string[] files = Directory.GetFiles(zonePath, "*.room");

            //only show the progress bar if there is a large number
            //of rooms to load
            progressBar1.Value = 0;
            if (files.Length > 0)
                progressBar1.Visible = true;

            progressBar1.Maximum = files.Length * 2;

            foreach (string file in files)
            {
                Room r = new Room();
                r = (Room)FileManager.Load(file, r);
                rooms.Add(r);
                lstRooms.Items.Add(r.Name);
                progressBar1.Increment(1);
            }

            foreach (Zone zone in zones)
            {
                if (zone.Name == lstZones.SelectedItem.ToString())
                {
                    linkedZone = zone;
                    break;
                }
                progressBar1.Increment(1);
            }
            progressBar1.Visible = false;
        }

        private void lstRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRooms.SelectedIndex == -1)
                return;

            foreach (Room room in rooms)
            {
                if (room.Name == lstRooms.SelectedItem.ToString())
                {
                    linkedRoom = room;
                    break;
                }
            }

            propertyRoom.SelectedObject = linkedRoom;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            TravelDirection = TravelDirections.GetTravelDirectionValue(button.Text);
            label1.Text = "Selected Doorway: " + TravelDirection.ToString();
        }

        private void btnSelectDoorway_Click(object sender, EventArgs e)
        {
            if (TravelDirection == AvailableTravelDirections.None)
            {
                MessageBox.Show("You must select a doorway to link with.",
                    "Doorway Manager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return; 
            }

            //Make sure we have all of our environments selected
            if (lstRooms.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a Realm, Zone and Room prior to selecting this doorway.",
                    "Doorway Manager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //Delete the door if it already exists, so we can re-install
            //the new door for this direction
            foreach (Door d in linkedRoom.InstalledDoors)
            {
                if (d.TravelDirection == TravelDirection)
                {
                    linkedRoom.InstalledDoors.Remove(d);
                    break;
                }
            }

            //Create a link to the currently loaded room within the Zone Builder
            Door.ConnectedRoom connected = new Door.ConnectedRoom();
            connected.Realm = Program.Realm.Name;
            connected.Zone = Program.Zone.Name;
            connected.Room = Program.Room.Name;
            connected.TravelDirection = TravelDirection;

            //Create a new door, add our link and set its travel direction
            Door door = new Door();
            door.TravelRoom = connected;
            door.TravelDirection = TravelDirections.GetTravelDirectionValue(TravelDirection.ToString());
            
            //install the door
            linkedRoom.InstalledDoors.Add(door);
            //save the linked room
            string realmPath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), linkedRealm.Name);
            string zonePath = Path.Combine(realmPath, linkedZone.Name);
            string roomFile = Path.Combine(zonePath, linkedRoom.Filename);
            FileManager.Save(roomFile, linkedRoom);
            this.Close();
        }
    }
}
