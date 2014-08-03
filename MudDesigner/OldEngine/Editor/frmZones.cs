/* frmZones
 * Product: Mud Designer Editor
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Allows creating and editing Zones within a specified Realm.
 */

//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Properties;

namespace MudDesigner.Editor
{
    public partial class frmZones : Form
    {
        /// <summary>
        /// Gets or Sets if the double-click to load and auto-close the editor needs to be enabled.
        /// </summary>
        internal bool ChangingZone { get; set; }

        public frmZones()
        {
            InitializeComponent();
        }

        private void zonesBtnAddZone_Click(object sender, EventArgs e)
        {
            //Ensure we have a Realm loaded.
            if (Editor.CurrentRealm == null)
            {
                //Will become true when a Realm is selected
                bool loaded = false;

                while (!loaded)
                {
                    //If the user doesn't want to load a Realm, we abort.
                    DialogResult results = MessageBox.Show("There is currently no Realm loaded.  Would you like to load one?", "Mud Designer Editor : Zones", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (results == System.Windows.Forms.DialogResult.No)
                        return;
                        //Otherwise we display the Realms form and let them choose one.
                    else
                    {
                        frmRealms realms = new frmRealms();
                        realms.ChangingRealm = true;
                        realms.ShowDialog();
                        while (realms.Visible)
                        {
                            Application.DoEvents();
                        }
                        realms = null;
                        //If we now have a Realm, consider it true.
                        if (Editor.CurrentRealm != null)
                            loaded = true;
                    }
                }
            }

            //We need to make sure we never have a duplicate name.
            int value = 1;
            string newName = "New Zone" + value;
            bool validName = false;

            while (!validName)
            {
                //In the event this is the first Zone.
                //Prevents infinit loop
                if (Editor.CurrentRealm.Zones.Count == 0)
                    validName = true;

                foreach (var z in Editor.CurrentRealm.Zones)
                {
                    if (z.Name == newName)
                    {
                        value++;
                        newName = "New Zone" + value;
                    }
                    else
                    {
                        validName = true;
                    }
                }
            }

            IZone zone = (IZone)ScriptFactory.GetScript(EngineSettings.Default.ZoneScript, null);

            if (zone == null)
            {
                MessageBox.Show("There are currently no Zone scripts that exist.  Please create a script that inherits from MudDesigner.Engine.Environment.BaseZone", "Mud Designer Editor : Zone", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            zone.Name = newName;
            Editor.CurrentZone = zone;
            Editor.CurrentRealm.AddZone(zone, true);

            zonesLstExistingZones.Items.Add(zone.Name);
            zonesLstExistingZones.SelectedItem = zone.Name;
        }

        private void zonesLstExistingZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (zonesLstExistingZones.SelectedIndex == -1)
                return;

            var zone = Editor.CurrentRealm.GetZone(zonesLstExistingZones.SelectedItem.ToString());

            if (zone == null)
                return;

            zoneProperties.SelectedObject = zone;
            Editor.CurrentZone = zone;

            UpdateUI();
        }

        void UpdateUI()
        {
            if (Editor.CurrentZone != null)
            {
                grpZoneProperties.Text = "Zone Properties (" + Editor.CurrentZone.GetType().Name + ")";
            }
            if (Editor.CurrentRealm != null)
            {
                zonesGrpRealm.Text = string.Format("Zones within the {0} realm", Editor.CurrentRealm.Name) ;
            }
        }

        private void zonesBtnChangeRealm_Click(object sender, EventArgs e)
        {
            //Creates a new instance of the Realms editor
            frmRealms realms = new frmRealms();

            //We want to make sure and remove this Zone from its current Relam
            if (Editor.CurrentRealm != null)
                Editor.CurrentRealm.RemoveZone(Editor.CurrentZone);
            //Flag changing zone as true so users can double-click and close the editor
            realms.ChangingRealm = true;

            //Show it as a dialog. This prevents other editors from starting at the same time.
            //Ensures that the static Editor Type will only be accessed by one editor at a time.
            realms.ShowDialog();

            //While the editor is visible, just keep the App responsive.
            while (realms.Visible)
            {
                Application.DoEvents();
            }

            //Null the reference we have
            realms = null;

            //if no Realm was selected, warn the user that the Zone will be lost when the editor
            //is closed or a new room is loaded.
            if (Editor.CurrentRealm == null && Editor.CurrentZone != null)
            {
                MessageBox.Show("You have not selected a Realm to place this Zone within. It will not be saved if a Realm is not choosen from the Realm editor!", this.Text);
                return;
            }

            //Add the Room to the selected Zone.
            Editor.CurrentRealm.AddZone(Editor.CurrentZone, true);

            comRealms.SelectedItem = Editor.CurrentRealm.Name;
        }

        private void zonessBtnDeleteZone_Click(object sender, EventArgs e)
        {
            if (zonesLstExistingZones.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a Zone to delete first.", "Mud Designer Editor : Zones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Editor.CurrentRealm.RemoveZone(Editor.CurrentZone);
            zonesLstExistingZones.Items.Remove(Editor.CurrentZone.Name);
            zoneProperties.SelectedObject = null;
            Editor.CurrentZone = null;
        }

        private void frmZones_Load(object sender, EventArgs e)
        {
            IRealm[] realms = Editor.Game.World.GetRealms();

            foreach (IRealm realm in realms)
            {
                comRealms.Items.Add(realm.Name);
            }
            if (comRealms.Items.Count > 0)
            {
                if (comRealms.Items.Contains(Editor.CurrentRealm.Name))
                    comRealms.SelectedItem = Editor.CurrentRealm.Name;
                else
                    comRealms.SelectedIndex = 0;
            }
            if (Editor.CurrentRealm == null)
            {
                MessageBox.Show("You will not be able to create or edit any Zones until you create a Realm.  You may create a Realm from the Realm editor.", "Mud Designer Editor : Zones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                zonesGrpRealm.Text = "Zones within the " + Editor.CurrentRealm.Name + " realm.";

            foreach (IZone zone in Editor.CurrentRealm.Zones)
            {
                zonesLstExistingZones.Items.Add(zone.Name);
            }

            UpdateUI();
        }

        private void zonesLstExistingZones_DoubleClick(object sender, EventArgs e)
        {
            if (ChangingZone)
            {
                zonesLstExistingZones_SelectedIndexChanged(sender, e);
                this.Close();
            }
        }

        private void zoneProperties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Name")
            {
                zonesLstExistingZones.Items[zonesLstExistingZones.Items.IndexOf(e.OldValue)] = Editor.CurrentZone.Name;
            }
        }

        private void zonesLstExistingZones_DoubleClick_1(object sender, EventArgs e)
        {
            //If we are just changing the Zone (launched from another editor)
            //we want to run the Zone selection code real-quick and close ourself
            if (ChangingZone)
            {
                zonesLstExistingZones_SelectedIndexChanged(sender, e);
                this.Close();
            }
        }

        private void comRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            zonesLstExistingZones.Items.Clear();

            IRealm realm = Editor.Game.World.GetRealm(comRealms.SelectedItem.ToString());
            if (realm == null)
            {
                MessageBox.Show("Realm does not exist!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Editor.CurrentRealm = realm;

            IZone[] zones = realm.GetZones();
            if (zones != null)
            {

                foreach (IZone zone in zones)
                {
                    zonesLstExistingZones.Items.Add(zone.Name);
                }
            }

            UpdateUI();
        }
    }
}
