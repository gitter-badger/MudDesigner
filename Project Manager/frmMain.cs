using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project_Manager
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            propertyGrid1.SelectedObject = Program.project;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Get all of the realms currently created.
            string[] realms = System.IO.Directory.GetFiles(Application.StartupPath + @"\Data\Realms");

            //Add each realm found into the combo box of available realms.
            foreach (string realm in realms)
            {
                //Instance a new realm
                MUDEngine.Environment.Realm newRealm = new MUDEngine.Environment.Realm();
                //De-serialize the current realm.
                newRealm = (MUDEngine.Environment.Realm)MUDEngine.FileSystem.FileSystem.Load(realm, newRealm);
                //Add it to the available realms combo box.
                comRealms.Items.Add(newRealm.Name);
            }

            //If the project already has a starting realm, then select it.
            if (Program.project.InitialLocation.Realm != null)
            {
                comRealms.SelectedIndex = comRealms.Items.IndexOf(Program.project.InitialLocation.Realm.Name);
            }
                //If there is no starting realm, but a realm does exist, select the first one in the list.
            else if (comRealms.Items.Count != 0)
            {
                comRealms.SelectedIndex = 0;
            }
        }

        private void comRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstZones.Items.Clear();

            //Check if we have any realms first.
            if (comRealms.Items.Count == 0)
                return;

            string[] zones = System.IO.Directory.GetFiles(Application.StartupPath + @"\Data\Zones");

            //Add each zone found into the list box.
            foreach (string zone in zones)
            {
                MUDEngine.Environment.Zone newZone = new MUDEngine.Environment.Zone();
                //De-serialize the current zone.
                newZone = (MUDEngine.Environment.Zone)MUDEngine.FileSystem.FileSystem.Load(zone, newZone);
                //Add it to the available zones list box
                lstZones.Items.Add(newZone.Name);
            }

            //Check if we have an existing realm that's set as our startup.
            if (Program.project.InitialLocation.Realm != null)
            {
                //Check if we have the Initial realm selected, if so we need to check the initial Zone as well
                if (comRealms.SelectedItem.ToString() == Program.project.InitialLocation.Realm.Name)
                {
                    //We have an initial zone, so lets check it in the list box
                    if (Program.project.InitialLocation.Zone != null)
                    {
                        if (lstZones.Items.Contains(Program.project.InitialLocation.Zone.Name))
                        {
                            lstZones.SelectedIndex = lstZones.Items.IndexOf(Program.project.InitialLocation.Zone.Name);
                        }
                    }
                }
            }
        }
    }
}
