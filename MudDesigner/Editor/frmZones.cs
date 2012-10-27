using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Properties;

namespace MudDesigner.Editor
{
    public partial class frmZones : Form
    {
        public frmZones()
        {
            InitializeComponent();
        }

        private void zonesBtnAddZone_Click(object sender, EventArgs e)
        {
            //Ensure we have a Realm loaded.
            if (EngineEditor.CurrentRealm == null)
            {
                bool loaded = false;

                while (!loaded)
                {
                    DialogResult results = MessageBox.Show("There is currently no Realm loaded.  Would you like to load one?", "Mud Designer Editor : Zones", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (results == System.Windows.Forms.DialogResult.No)
                        return;
                    else
                    {
                        frmRealms realms = new frmRealms();
                        realms.ShowDialog();
                        while (realms.Visible)
                        {
                            Application.DoEvents();
                        }
                        realms = null;
                        if (EngineEditor.CurrentRealm != null)
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
                if (EngineEditor.CurrentRealm.Zones.Count == 0)
                    validName = true;

                foreach (var z in EngineEditor.CurrentRealm.Zones)
                {
                    if (z.Value.Name == newName)
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

            IZone zone = (IZone)ScriptFactory.GetScript(EngineSettings.Default.ZoneType, null);

            if (zone == null)
            {
                MessageBox.Show("There are currently no Zone scripts that exist.  Please create a script that inherits from MudDesigner.Engine.Environment.BaseZone", "Mud Designer Editor : Zone", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            zone.Name = newName;
            EngineEditor.CurrentZone = zone;
            EngineEditor.CurrentRealm.AddZone(zone, true);

            zonesLstExistingZones.Items.Add(zone.Name);
            zonesLstExistingZones.SelectedItem = zone.Name;
        }

        private void zonesLstExistingZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (zonesLstExistingZones.SelectedIndex == -1)
                return;

            var zone = EngineEditor.CurrentRealm.GetZone(zonesLstExistingZones.SelectedItem.ToString());

            if (zone == null)
                return;

            zoneProperties.SelectedObject = zone;
            EngineEditor.CurrentZone = zone;
        }

        private void zonesBtnChangeRealm_Click(object sender, EventArgs e)
        {
            frmRealms realms = new frmRealms();
            realms.ShowDialog();
            while (realms.Visible)
            {
                Application.DoEvents();
            }
            realms = null;

            if (EngineEditor.CurrentRealm == null)
                return;

            zonesLstExistingZones.Items.Clear();

            foreach (IZone zone in EngineEditor.CurrentRealm.Zones.Values)
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

            EngineEditor.CurrentRealm.RemoveZone(EngineEditor.CurrentZone);
            zonesLstExistingZones.Items.Remove(EngineEditor.CurrentZone.Name);
            zoneProperties.SelectedObject = null;
            EngineEditor.CurrentZone = null;
        }

        private void frmZones_Load(object sender, EventArgs e)
        {
            if (EngineEditor.CurrentRealm == null)
            {
                MessageBox.Show("You will not be able to create or edit any Zones until you load a Realm.  You may Load a Realm from within the Zone editor or use the Realm editor.", "Mud Designer Editor : Zones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (IZone zone in EngineEditor.CurrentRealm.Zones.Values)
            {
                zonesLstExistingZones.Items.Add(zone.Name);
            }
        }
    }
}
