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
            //Aquire the Project settings and show them.
            propertyGrid1.SelectedObject = Program.Project;
            txtStory.Text = Program.Project.Story;

            string realmPath = FileManager.GetDataPath(SaveDataTypes.Realms);
            string[] realms = System.IO.Directory.GetFiles(realmPath, "*.realm");
            foreach (string file in realms)
            {
                Realm realm = new Realm();
                realm = (Realm)FileManager.Load(file, realm);
                comRealms.Items.Add(realm.Name);
            }

            if (comRealms.Items.Count != 0)
                comRealms.SelectedIndex = 0;
        }

        private void comRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comRealms.SelectedIndex == -1)
                return;

            string realmPath = FileManager.GetDataPath(SaveDataTypes.Realms);
            string realmFile = System.IO.Path.Combine(realmPath, comRealms.SelectedItem.ToString() + ".realm");
            Realm realm = new Realm();
            realm = (Realm)FileManager.Load(realmFile, realm);
            foreach (Zone zone in realm.Zones)
            {
                lstZones.Items.Add(zone.Name);
            }
        }

        private void lstZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstZones.SelectedIndex == -1)
                return;

            string zonePath = FileManager.GetDataPath(SaveDataTypes.Zones);
            string zoneFile = System.IO.Path.Combine(zonePath, lstZones.SelectedItem.ToString() + ".zone");
            Zone zone = new Zone();
            zone = (Zone)FileManager.Load(zoneFile, zone);
            foreach (Room room in zone.Rooms)
            {
                lstRooms.Items.Add(room.Name);
            }
        }

        private void txtStory_TextChanged(object sender, EventArgs e)
        {
            Program.Project.Story = txtStory.Text;
        }

        private void ProjectSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            string filename = System.IO.Path.Combine(FileManager.GetDataPath(SaveDataTypes.Root), "Project.xml");
            FileManager.Save(filename, Program.Project);
        }

        private void lstRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            string roomPath = FileManager.GetDataPath(SaveDataTypes.Rooms);
            string zonePath = FileManager.GetDataPath(SaveDataTypes.Zones);
            string realmPath = FileManager.GetDataPath(SaveDataTypes.Realms);

            string roomFile = System.IO.Path.Combine(roomPath, lstRooms.SelectedItem.ToString() + ".room");
            string zoneFile = System.IO.Path.Combine(zonePath, lstZones.SelectedItem.ToString() + ".zone");
            string realmFile = System.IO.Path.Combine(realmPath, comRealms.SelectedItem.ToString() + ".realm");

            Room room = new Room();
            Zone zone = new Zone();
            Realm realm = new Realm();
            room = (Room)FileManager.Load(roomFile, room);
            zone = (Zone)FileManager.Load(zoneFile, zone);
            realm = (Realm)FileManager.Load(realmFile, realm);

            //TODO: Fix broken InitialLocation
        }
    }
}
