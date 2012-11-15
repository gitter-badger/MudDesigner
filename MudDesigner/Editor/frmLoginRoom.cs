using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Properties;

namespace MudDesigner.Editor
{
    public partial class frmLoginRoom : Form
    {
        IRealm SelectedRealm { get; set; }
        IZone SelectedZone { get; set; }
        IRoom SelectedRoom { get; set; }

        public frmLoginRoom()
        {
            InitializeComponent();
        }

        private void frmLoginRoom_Load(object sender, EventArgs e)
        {
            IWorld world = Editor.Game.World;
            string[] values = null;
            bool validPath = false;

            if (!String.IsNullOrEmpty(EngineSettings.Default.InitialRoom))
            {
                values = EngineSettings.Default.InitialRoom.Split('>');

                if (values.Length == 3)
                {
                    validPath = true;
                }
            }

            IRealm[] realms = world.GetRealms();

            if (realms == null)
                return;

            foreach (IRealm realm in realms)
            {
                comRealms.Items.Add(realm.Name);

                if (validPath)
                {
                    if (values[0] == realm.Name)
                        comRealms.SelectedItem = realm.Name;
                }
            }

            if (comRealms.Items.Count > 0 && comRealms.SelectedIndex == -1)
            {
                comRealms.SelectedIndex = 0;
            }
        }

        private void comRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comRealms.SelectedIndex == -1)
                return;

            IRealm realm = Editor.Game.World.GetRealm(comRealms.SelectedItem.ToString());
            string[] values = null;
            bool validPath = false;

            if (!String.IsNullOrEmpty(EngineSettings.Default.InitialRoom))
            {
                values = EngineSettings.Default.InitialRoom.Split('>');

                if (values.Length == 3)
                {
                    validPath = true;
                }
            }

            if (realm == null)
            {
                MessageBox.Show("There was an error retreiving the specified realm.", this.Text);
                return;
            }
            else
                //Must be set prior to setting the index of the combo box
                SelectedRealm = realm;

            IZone[] zones = realm.GetZones();

            if (zones == null)
                return;

            foreach (IZone zone in zones)
            {
                comZones.Items.Add(zone.Name);

                if (validPath)
                {
                    if (values[1] == zone.Name)
                        comZones.SelectedItem = zone.Name;
                }
            }

            if (comZones.Items.Count > 0)
                comZones.SelectedIndex = 0;
        }

        private void comZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comZones.SelectedIndex == -1)
                return;

            IZone zone = SelectedRealm.GetZone(comZones.SelectedItem.ToString());
            string[] values = null;
            bool validPath = false;

            if (!String.IsNullOrEmpty(EngineSettings.Default.InitialRoom))
            {
                values = EngineSettings.Default.InitialRoom.Split('>');

                if (values.Length == 3)
                {
                    validPath = true;
                }
            }

            if (zone == null)
            {
                MessageBox.Show("There was an error retreiving the specified Zone.", this.Text);
                return;
            }
            else
                //Must be set prior to changing the list box index
                SelectedZone = zone;

            IRoom[] rooms = zone.GetRooms();

            foreach (IRoom room in rooms)
            {
                lstRooms.Items.Add(room.Name);

                if (validPath)
                {
                    if (values[2] == room.Name)
                        lstRooms.SelectedItem = room.Name;
                }
            }

            if (lstRooms.Items.Count > 0)
                lstRooms.SelectedIndex = 0;
        }

        private void lstRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRooms.SelectedIndex == -1)
                return;

            IRoom room = SelectedZone.GetRoom(lstRooms.SelectedItem.ToString());

            if (room == null)
            {
                MessageBox.Show("There was an error retreiving the specified room.", this.Text);
                return;
            }
            else
                SelectedRoom = room;
        }

        private void frmLoginRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SelectedRealm != null && SelectedZone != null && SelectedRoom != null)
            {
                EngineSettings.Default.InitialRoom = SelectedRealm.Name + ">" + SelectedZone.Name + ">" + SelectedRoom.Name;
            }
            else
            {
                MessageBox.Show("You did not set any initial locations!", this.Text);
                return;
            }

            EngineSettings.Default.Save();
        }
    }
}
