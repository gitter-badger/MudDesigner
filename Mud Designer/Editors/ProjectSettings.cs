using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//MudEngine
using MudDesigner.MudEngine;
using MudDesigner.MudEngine.Attributes;
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.Objects;
using MudDesigner.MudEngine.Objects.Environment;


namespace MudDesigner.Editors
{
    public partial class ProjectSettings : Form
    {
        List<Zone> zones;
        List<Room> rooms;

        public ProjectSettings()
        {
            InitializeComponent();
            zones = new List<Zone>();
            rooms = new List<Room>();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Get all of the realms currently created.
            string[] files = System.IO.Directory.GetFiles(Engine.GetDataPath(Engine.SaveDataTypes.Realms), "*.realm");

            //Aquire the Project settings and show them.
            propertyGrid1.SelectedObject = Program.Project;
            txtStory.Text = Program.Project.Story;

            //Add each realm found into the combo box of available realms.
            foreach (string realm in files)
            {
                //Instance a new realm
                Realm newRealm = new Realm();
                //De-serialize the current realm.
                newRealm = (Realm)FileSystem.Load(realm, newRealm);
                //Add it to the available realms combo box.
                comRealms.Items.Add(newRealm.Name);
            }

            //If the Project already has a starting realm, then select it.
            if (Program.Project.InitialLocation.Realm != null)
            {
                comRealms.SelectedIndex = comRealms.Items.IndexOf(Program.Project.InitialLocation.Realm.Name);
            }
                //If there is no starting realm, but a realm does exist, select the first one in the list.
            else if (comRealms.Items.Count != 0)
            {
                comRealms.SelectedIndex = 0;
            }
        }//End frmMain_Load

        private void comRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstZones.Items.Clear();

            //Check if we have any realms first.
            if (comRealms.Items.Count == 0)
                return;

            string[] files = System.IO.Directory.GetFiles(Application.StartupPath + @"\Data\Zones");

            //Add each zone found into the list box.
            foreach (string zone in files)
            {
                Zone newZone = new Zone();
                //De-serialize the current zone.
                newZone = (Zone)FileSystem.Load(zone, newZone);
                //Add it to the available zones list box
                lstZones.Items.Add(newZone.Name);
                zones.Add(newZone);
            }

            //Check if we have an existing realm that's set as our startup.
            if (Program.Project.InitialLocation.Realm != null)
            {
                //Check if we have the Initial realm selected, if so we need to check the initial Zone as well
                if (comRealms.SelectedItem.ToString() == Program.Project.InitialLocation.Realm.Name)
                {
                    //We have an initial zone, so lets check it in the list box
                    if (Program.Project.InitialLocation.Zone != null)
                    {
                        if (lstZones.Items.Contains(Program.Project.InitialLocation.Zone.Name))
                        {
                            lstZones.SelectedIndex = lstZones.Items.IndexOf(Program.Project.InitialLocation.Zone.Name);
                        }
                    }
                }
            }
        }//End comRealms

        private void lstZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            string realm = comRealms.SelectedItem.ToString();
            string zone = lstZones.SelectedItem.ToString();

            lstRooms.Items.Clear();

            //Check if we have any realms first.
            if (comRealms.Items.Count == 0)
                return;

            string[] files = System.IO.Directory.GetFiles(Application.StartupPath + @"\Data\Rooms");

            //Add each room found into the list box.
            foreach (string room in files)
            {
                Room newRoom = new Room();
                //De-serialize the current Room.
                newRoom = (Room)FileSystem.Load(room, newRoom);
                //Add it to the available rooms list box
                lstRooms.Items.Add(newRoom.Name);
                rooms.Add(newRoom);
            }

            //Now select the initial room if its listed.
            string selectedRealm = comRealms.SelectedItem.ToString();
            string selectedZone = lstZones.SelectedItem.ToString();
            string initialRealm = Program.Project.InitialLocation.Realm.Name;
            string initialZone = Program.Project.InitialLocation.Zone.Name;

            //The realm and zone that matches the initial are selected, so lets select the initial room next.
            if ((initialRealm == selectedRealm) && (initialZone == selectedZone))
            {
                foreach (Room room in rooms)
                {
                    if (lstRooms.Items.Contains(room.Name))
                        lstRooms.SelectedIndex = lstRooms.Items.IndexOf(room.Name);
                }
            }
        }

        private void txtStory_TextChanged(object sender, EventArgs e)
        {
            Program.Project.Story = txtStory.Text;
        }

        private void ProjectSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            string filename = System.IO.Path.Combine(Engine.GetDataPath(Engine.SaveDataTypes.Root), "Project.xml");
            FileSystem.Save(filename, Program.Project);
        }
    }
}
