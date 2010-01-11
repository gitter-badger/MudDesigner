using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using MudDesigner.MudEngine.GameManagement;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;
using MudDesigner.MudEngine.FileSystem;

namespace MudDesigner.MudEngine.UITypeEditors
{
    public partial class UIRealmControl : Form
    {
        private Realm _Realm;
        private string realmPath = "";
        string projectPath = "";
        private List<string> zones;

        public List<string> Zones
        {
            get
            {
                return zones;
            }
        }


        public UIRealmControl(Realm realm)
        {
            InitializeComponent();

            _Realm = realm;
            zones = new List<string>();

            projectPath = Path.Combine(Application.StartupPath, "Project");
            realmPath = Path.Combine(projectPath, "Realms");
            realmPath = Path.Combine(realmPath, _Realm.Name);
            string realmZones = Path.Combine(realmPath, "Zones");

            if (!Directory.Exists(realmZones))
            {
                MessageBox.Show("You must save the Realm prior to adding Zones to it.", "Mud Designer");
                this.Close();
            }

            foreach (string zone in _Realm.Zones)
            {
                zones.Add(zone);
            }
        }

        private void UIRealmControl_Load(object sender, EventArgs e)
        {
            string zonePath = Path.Combine(projectPath, "Zones");
            foreach (string zone in _Realm.Zones)
            {
                lstRealmMembers.Items.Add(zone);
            }

            if (!Directory.Exists(zonePath))
                Directory.CreateDirectory(zonePath);

            string[] zones = Directory.GetFiles(zonePath, "*.zone", SearchOption.AllDirectories);

            foreach (string zone in zones)
            {
                Zone z = new Zone();
                z = (Zone)FileManager.Load(zone, z);
                lstAvailableZones.Items.Add(z.Filename);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lstAvailableZones.SelectedIndex == -1)
            {
                MessageBox.Show("Select a Zone to add first!", "Mud Designer");
                return;
            }

            Zone zone = new Zone();
            //Set the paths
            string zoneRoot = Path.Combine(projectPath, "Zones");
            string realmZones = Path.Combine(realmPath, "Zones");
            string[] files = Directory.GetFiles(zoneRoot, "*.zone", SearchOption.AllDirectories);
            string originalFile = "";
            if (files.Length == 0)
                zone = null;

            foreach (string file in files)
            {
                if (Path.GetFileName(file) == lstAvailableZones.SelectedItem.ToString())
                {
                    zone = (Zone)FileManager.Load(file, zone);
                    originalFile = file;
                    zone.Realm = _Realm.Name;
                    FileManager.Save(file, zone);
                }
                else
                    zone = null;
            }

            //check if we have a zone
            if (zone == null)
            {
                MessageBox.Show("Unable to locate the zone specified!", "Mud Designer");
                return;
            }
            //get the new path for the zone within the realm directory
            string zonePath = Path.Combine(realmZones, zone.Name);
            if (!Directory.Exists(zonePath))
                Directory.CreateDirectory(zonePath);


            zones.Add(lstAvailableZones.SelectedItem.ToString());
            lstRealmMembers.Items.Add(lstAvailableZones.SelectedItem);
            
            //copy the file
            string newFile = Path.Combine(zonePath, lstAvailableZones.SelectedItem.ToString());
            File.Copy(originalFile, newFile, true);
            //copy all of its rooms if they exist
            string originalPath = originalFile.Substring(0, originalFile.Length - Path.GetFileName(originalFile).Length);
            string roomPath = Path.Combine(originalPath, "Rooms");
            string[] rooms = Directory.GetFiles(roomPath);

            if (rooms.Length != 0)
            {
                string newPath = newFile.Substring(0, newFile.Length - Path.GetFileName(newFile).Length);
                string newRooms = Path.Combine(newPath, "Rooms");
                if (!Directory.Exists(newRooms))
                    Directory.CreateDirectory(newRooms);

                foreach (string room in rooms)
                {
                    newFile = Path.Combine(newRooms, Path.GetFileName(room));
                    File.Copy(room, newFile);
                    File.Delete(room);
                }
            }
            File.Delete(originalFile);
            Directory.Delete(Path.Combine(zoneRoot, zone.Name), true);
            lstAvailableZones.Items.Remove(lstAvailableZones.SelectedItem);
        }

        private void UIRealmControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            Designer form = (Designer)Program.CurrentEditor;
            form.SaveSelected();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstRealmMembers.SelectedIndex == -1)
            {
                MessageBox.Show("You will need to select a Zone to remove from this Realm first.", "Mud Designer");
                    return;
            }

            string projectPath = Path.Combine(Application.StartupPath, "Project");
            //Project/Realms
            string realmPath = Path.Combine(projectPath, "Realms");
            //Project/Realms/RealmName
            string realmRoot = Path.Combine(realmPath, _Realm.Name);
            string[] zones = Directory.GetFiles(realmRoot, "*.zone", SearchOption.AllDirectories);
            bool IsFound = false;

            //Find the zone that we need to remove from the realm
            foreach(string zone in zones)
            {
                Zone z = new Zone();
                z = (Zone)FileManager.Load(zone, z);

                //We found the zone, remove it from the realm
                //the zone file gets placed back in the Zones directory
                //now that it is no longer part of a realm
                if (z.Filename == lstRealmMembers.SelectedItem.ToString())
                {
                    IsFound = true;
                    z.Realm = "";
                    _Realm.Zones.Remove(z.Filename);
                    FileManager.Save(zone, z);

                    string zonePath = Path.Combine(projectPath, "Zones");
                    string zoneRoot = Path.Combine(zonePath, z.Name);
                    string newFile = Path.Combine(zoneRoot, Path.GetFileName(zone));

                    if (!Directory.Exists(zoneRoot))
                        Directory.CreateDirectory(zoneRoot);

                    //Copy all of the rooms assigned to this zone.
                    string oldZonePath = Path.GetFullPath(zone).Substring(0, Path.GetFullPath(zone).Length - Path.GetFileName(zone).Length);

                    if (Directory.Exists(Path.Combine(oldZonePath, "Rooms")))
                    {
                        if (!Directory.Exists(Path.Combine(zoneRoot, "Rooms")))
                            Directory.CreateDirectory(Path.Combine(zoneRoot, "Rooms"));

                        string[] rooms = Directory.GetFiles(Path.Combine(oldZonePath, "Rooms"), "*.room");
                        
                        foreach (string room in rooms)
                        {
                            string roomFile = Path.Combine(zoneRoot, "Rooms");
                            roomFile = Path.Combine(roomFile, Path.GetFileName(room));
                            File.Copy(room, roomFile);
                            File.Delete(room);
                        }
                    }

                    //finally copy the zone file
                    File.Copy(zone, newFile);
                    File.Delete(zone);
                    Directory.Delete(oldZonePath, true);
                    lstAvailableZones.Items.Add(lstRealmMembers.SelectedItem);
                    lstRealmMembers.Items.Remove(lstRealmMembers.SelectedItem);
                    break;
                }
            }//End of foreach
            if (!IsFound)
            {
                _Realm.Zones.Remove(lstRealmMembers.SelectedItem.ToString());
                string filename = Path.Combine(realmRoot, _Realm.Filename);
                FileManager.Save(filename, _Realm);
                lstAvailableZones.Items.Add(lstRealmMembers.SelectedItem);
                lstRealmMembers.Items.Remove(lstRealmMembers.SelectedItem);
            }
        }
    }
}
