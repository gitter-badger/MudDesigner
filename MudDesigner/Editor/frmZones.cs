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
        }

        private void zonesBtnChangeRealm_Click(object sender, EventArgs e)
        {
            frmRealms realms = new frmRealms();
            realms.ChangingRealm = true;
            realms.ShowDialog();
            while (realms.Visible)
            {
                Application.DoEvents();
            }
            realms = null;

            if (Editor.CurrentRealm == null)
                return;
            else
                zonesGrpZones.Text = "Zones within the " + Editor.CurrentRealm.Name + " realm.";


            zonesLstExistingZones.Items.Clear();

            foreach (IZone zone in Editor.CurrentRealm.Zones)
            {
                zonesLstExistingZones.Items.Add(zone.Name);
            }
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
            if (Editor.CurrentRealm == null)
            {
                MessageBox.Show("You will not be able to create or edit any Zones until you load a Realm.  You may Load a Realm from within the Zone editor or use the Realm editor.", "Mud Designer Editor : Zones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                zonesGrpZones.Text = "Zones within the " + Editor.CurrentRealm.Name + " realm.";

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
    }
}
