using System;
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
        List<Zone> _Zones = new List<Zone>();

        public ZoneBuilder()
        {
            InitializeComponent();
            //Reinstance all of our environments
            Program.Realm = new Realm();
            Program.Zone = new Zone();
            Program.Room = new Room();
        }

        private void btnNewRealm_Click(object sender, EventArgs e)
        {
            if (!IsRealmLoaded)
            {
                MessageBox.Show("You need to select a Realm to create a Zone in first.",
                    "Zone Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Program.Zone = new Zone();
            Program.Zone.Realm = Program.Realm.Name;
            propertyZone.SelectedObject = Program.Zone;
        }

        private void btnSaveRealm_Click(object sender, EventArgs e)
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

            //realm is loaded, now clear out the list of zones and show the zones contained
            //within the new realm
            lstZones.Items.Clear();
            _Zones.Clear();
            string[] files = Directory.GetFiles(realmPath, "*.zone", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                string filename = Path.GetFileName(file);
                if (Program.Realm.Zones.Contains(filename))
                {
                    Zone zone = new Zone();
                    zone = (Zone)FileManager.Load(file, zone);
                    _Zones.Add(zone);
                    lstZones.Items.Add(zone.Name);
                }
                else
                    continue;
            }
        }

        private void btnLoadRealm_Click(object sender, EventArgs e)
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

            //Loop through the collection we generated when we selected our realm
            //and find the zone that the user selected to load
            foreach (Zone zone in _Zones)
            {
                if (zone.Name == lstZones.SelectedItem.ToString())
                {
                    Program.Zone = zone;
                    break;
                }
            }

            propertyZone.SelectedObject = Program.Zone;
        }
    }
}
