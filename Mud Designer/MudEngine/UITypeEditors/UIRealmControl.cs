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
        
        public List<string> Zones
        {
            get
            {
                return _Realm.Zones;
            }
        }


        public UIRealmControl(Realm realm)
        {
            InitializeComponent();

            //Store the supplied Realm loaded by the user
            _Realm = realm;

            //Setup our paths
            projectPath = FileManager.GetDataPath(SaveDataTypes.Root);
            realmPath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), _Realm.Name);
            string realmZones = Path.Combine(realmPath, "Zones");

            //Check if the Realm path exist, if not then the Realm has not been saved and 
            //we cannot go any further.
            if (!Directory.Exists(realmPath))
            {
                MessageBox.Show("You must save the Realm prior to adding Zones to it. The Realm will Auto-Save after the default name has been changed.", "Mud Designer");
                this.Close();
                return;
            }

            //If the Zones directory has not been created yet within the Realm, we need to create it.
            if (!Directory.Exists(realmZones))
                Directory.CreateDirectory(realmZones);
        }

        private void UIRealmControl_Load(object sender, EventArgs e)
        {
            //Place all of the Realms existing Zones into the Realm Members
            //list box.
            foreach (string zone in _Realm.Zones)
            {
                lstRealmMembers.Items.Add(zone);
            }

            //Create the Root Zone directory if it does not exist
            string zonePath = FileManager.GetDataPath(SaveDataTypes.Zones);
            if (!Directory.Exists(zonePath))
                Directory.CreateDirectory(zonePath);

            //Add all of the existing Un-owned Zones into the Available Zones listbox
            foreach (string zone in Directory.GetFiles(zonePath, "*.zone", SearchOption.AllDirectories))
            {
                //place the filename into the Available Zones.
                lstAvailableZones.Items.Add(Path.GetFileName(zone));
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lstAvailableZones.SelectedIndex == -1)
            {
                MessageBox.Show("Select a Zone to add first!", "Mud Designer");
                return;
            }

            //Store the name of the current Zone being added to the World.
            string zoneName = Path.GetFileNameWithoutExtension(lstAvailableZones.SelectedItem.ToString());
            
            //Store the original Folder & Filename locations of the Zone we are moving
            string originalZonePath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Zones), zoneName);
            string originalFilename = Path.Combine(originalZonePath, lstAvailableZones.SelectedItem.ToString());
            string originalZoneRooms = Path.Combine(originalZonePath, "Rooms");

            //Create our new paths
            string newZonePath = FileManager.GetDataPath(_Realm.Name, zoneName);
            string newFilename = Path.Combine(newZonePath, lstAvailableZones.SelectedItem.ToString());
            string newZoneRooms = Path.Combine(newZonePath, "Rooms");

            //Create the new Zone directory within it's new Realm.
            //Checking and Creating the Rooms directory will force the
            //Zone directory to be created at the same time.
            if (!Directory.Exists(newZoneRooms))
                Directory.CreateDirectory(newZoneRooms);

            //Load the original Zone and assign it to the new Realm.
            Zone zone = new Zone();
            zone = (Zone)zone.Load(originalFilename);
            zone.Realm = _Realm.Name;
            //Add the zone to the Realm collection
            _Realm.Zones.Add(zone.Filename);
            //Save the Zone changes into the new Realm path.
            //We are done with modifying the Zone so we are free to delete it
            //after we are done moving all of its Rooms.
            zone.Save(newFilename);

            //Check if the original Zone contained Rooms.
            string[] rooms = Directory.GetFiles(originalZoneRooms, "*.room");

            //If we find Rooms, we need to copy them.
            if (rooms.Length != 0)
            {
                //Loop through the collection, copying each Room file into the 
                //new Rooms directory within the Realms Room path
                foreach (string room in rooms)
                {
                    //Get the current filename and it's new Path
                    string roomFile = Path.GetFileName(room);
                    string roomPath = newZoneRooms;
                    //Copy the current file into the new path.
                    File.Copy(room, Path.Combine(roomPath, roomFile), true);
                }
            }

            //All of our Rooms are copied, Zone has been updated.
            //Update the Realm to give it the Zone, then add it to the
            //List box, then remove it from the Available listbox
            lstRealmMembers.Items.Add(zone.Filename);
            lstAvailableZones.Items.Remove(zone.Filename);

            //Done with what we needed to do, let's delete the original Zone
            //and all of its files.
            Directory.Delete(originalZonePath, true);
        }

        private void UIRealmControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.Designer.SaveObject();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstRealmMembers.SelectedIndex == -1)
            {
                MessageBox.Show("You will need to select a Zone to remove from this Realm first.", "Mud Designer");
                    return;
            }

            //Get the current zones name
            string zoneName = Path.GetFileNameWithoutExtension(lstRealmMembers.SelectedItem.ToString());

            //Get our current storage information
            string currentZonePath = FileManager.GetDataPath(_Realm.Name, zoneName);
            string currentZoneRooms = Path.Combine(currentZonePath, "Rooms");
            string currentFilename = Path.Combine(currentZonePath, lstRealmMembers.SelectedItem.ToString());

            //create our new storage info
            string newZonePath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Zones), zoneName);
            string newFilename = Path.Combine(newZonePath, lstRealmMembers.SelectedItem.ToString());
            string newZoneRooms = Path.Combine(newZonePath, "Rooms");

            //Create the Zone directory within the Root Zones path
            if (!Directory.Exists(newZoneRooms))
                Directory.CreateDirectory(newZoneRooms);

            //Load the Zone and remove its link to the Realm
            Zone z = new Zone();
            z = (Zone)z.Load(currentFilename);
            z.Realm = "";
            //Remove the Zone from the Realm collection
            _Realm.Zones.Remove(z.Filename);

            //Save the modified Zone into the new path
            z.Save(newFilename);

            //Check if the original Zone contained Rooms.
            string[] rooms = Directory.GetFiles(currentZoneRooms, "*.room");

            //If we find Rooms, we need to copy them.
            if (rooms.Length != 0)
            {
                //Loop through the collection, copying each Room file into the 
                //new Rooms directory within the Realms Room path
                foreach (string room in rooms)
                {
                    //Get the current filename and it's new Path
                    string roomFile = Path.GetFileName(room);
                    string roomPath = newZoneRooms;
                    //Copy the current file into the new path.
                    File.Copy(room, Path.Combine(roomPath, roomFile), true);
                }
            }

            lstAvailableZones.Items.Add(z.Filename);
            lstRealmMembers.Items.Remove(z.Filename);
            Directory.Delete(currentZonePath, true);
        }
    }
}
