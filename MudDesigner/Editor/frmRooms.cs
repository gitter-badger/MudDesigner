using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Properties;
using MudDesigner.Engine.Scripting;

namespace MudDesigner.Editor.Rooms
{
    public partial class frmRooms : Form
    {
        //Drag & drop related fields.
        private int indexOfItemUnderMouseToDrag;
        private Rectangle dragBoxFromMouseDown;
        private Point screenOffset;

        public frmRooms()
        {
            InitializeComponent();
        }

        private void frmRooms_Load(object sender, EventArgs e)
        {
            SetupEnvironment();

            RefreshRoomLabels(EngineEditor.CurrentRealm, EngineEditor.CurrentZone, EngineEditor.CurrentRoom);
        }

        private void SetupEnvironment()
        {
            roomsComRealms.Items.Clear();
            roomsComZones.Items.Clear();
            roomsLstExistingRooms.Items.Clear();

            //Build the collection of Realms and Zones first.
            foreach (IRealm realm in EngineEditor.Game.World.Realms.Values)
            {
                roomsComRealms.Items.Add(realm.Name);
            }

            //Make sure CurrentRealm is not null and that the CurrentRealm exists within the Combobox.
            if (EngineEditor.CurrentRealm != null && roomsComRealms.Items.Contains(EngineEditor.CurrentRealm.Name))
                roomsComRealms.SelectedItem = EngineEditor.CurrentRealm.Name;
            //If not, select the first Realm in the box if there are any items.
            else if (roomsComRealms.Items.Count > 0)
                roomsComRealms.SelectedIndex = 0;

            if (EngineEditor.CurrentRealm != null)
            {
                roomsLblRealmAndZone.Text = "Current Location: " + EngineEditor.CurrentRealm.Name;
            }
            else
                roomsLblRealmAndZone.Text = "No Location Loaded! New Rooms will be created within the Zone\nthat is selected under the Environments Tab.";

            if (EngineEditor.CurrentZone != null)
            {
                roomsLblRealmAndZone.Text += "->" + EngineEditor.CurrentZone.Name;
            }

            //Check if Current Environment references are null. If so, then set them.
            if (EngineEditor.CurrentRealm == null && roomsComRealms.SelectedIndex >= 0)
            {
                EngineEditor.CurrentRealm = EngineEditor.Game.World.GetRealm(roomsComRealms.SelectedItem.ToString());
            }

            //Setup the current zone to use what ever is selected, providing current realm was not set as null.
            //should never have a CurrentZone set with a null CurrentRealm
            if (EngineEditor.CurrentZone == null && roomsComZones.SelectedIndex >= 0 && EngineEditor.CurrentRealm != null)
            {
                EngineEditor.CurrentZone = EngineEditor.CurrentRealm.GetZone(roomsComZones.SelectedItem.ToString());
            }
        }

        private void RefreshDoorwayList()
        {
            roomsBtnNorth.Text = "North\n\r(Empty)";
            roomsBtnSouth.Text = "South\n\r(Empty)";
            roomsBtnEast.Text = "East\n\r(Empty)";
            roomsBtnWest.Text = "West\n\r(Empty)";

            if (EngineEditor.CurrentRoom.Doorways.Count > 0)
            {
                foreach (AvailableTravelDirections door in EngineEditor.CurrentRoom.Doorways.Keys)
                {
                    switch (door)
                    {
                        case AvailableTravelDirections.North:
                            North.Text = "North\n\r" + EngineEditor.CurrentRoom.Doorways[door].Arrival.Name;
                            break;
                        case AvailableTravelDirections.South:
                            South.Text = "South\n\r" + EngineEditor.CurrentRoom.Doorways[door].Arrival.Name;
                            break;
                        case AvailableTravelDirections.East:
                            East.Text = "East\n\r" + EngineEditor.CurrentRoom.Doorways[door].Arrival.Name;
                            break;
                        case AvailableTravelDirections.West:
                            West.Text = "West\n\r" + EngineEditor.CurrentRoom.Doorways[door].Arrival.Name;
                            break;
                    }
                }
            }
        }

        private void roomsLstExistingRooms_MouseDown(object sender, MouseEventArgs e)
        {
            if (roomsLstExistingRooms.SelectedIndex == -1)
                return;

            // Get the index of the item the mouse is below.
            indexOfItemUnderMouseToDrag = roomsLstExistingRooms.IndexFromPoint(e.X, e.Y);

            if (indexOfItemUnderMouseToDrag != ListBox.NoMatches)
            {

                // Remember the point where the mouse down occurred. The DragSize indicates
                // the size that the mouse can move before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void Room_DragOver(object sender, DragEventArgs e)
        {
            //No drag & drop if we have no room loaded.
            if (EngineEditor.CurrentRoom == null)
                return;
            else
                e.Effect = DragDropEffects.Link;
        }

        private void Room_DragDrop(object sender, DragEventArgs e)
        {
            if (EngineEditor.CurrentRoom == null)
                return; //Don't do any drag & drop if we have no room loaded.

            var data = e.Data.GetData(DataFormats.Text);
            IRoom room = (IRoom)EngineEditor.CurrentZone.GetRoom(data.ToString());

            Button btnDirection = (Button)sender;
            string[] values = btnDirection.Text.Split('\n');
            //Trims out the trailing \r the editor button has.
            string direction = values[0];

            AvailableTravelDirections travelDirection = TravelDirections.GetTravelDirectionValue(direction);
            EngineEditor.CurrentRoom.AddDoorway(travelDirection, room, true, true);
            
            roomsLstExistingRooms.SelectedItem = EngineEditor.CurrentRoom.Name;

            RefreshDoorwayList();
        }

        private void roomsLstExistingRooms_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Reset the drag rectangle when the mouse button is raised.
            dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void roomsLstExistingRooms_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {

                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {

                    // The screenOffset is used to account for any desktop bands 
                    // that may be at the top or left side of the screen when 
                    // determining when to cancel the drag drop operation.
                    screenOffset = SystemInformation.WorkingArea.Location;

                    // Proceed with the drag and drop, passing in the list item.
                    string linkedRoom = roomsLstExistingRooms.Items[indexOfItemUnderMouseToDrag].ToString();

                    if (linkedRoom == null)
                        return;

                    DragDropEffects dropEffect = roomsLstExistingRooms.DoDragDrop(linkedRoom, DragDropEffects.All | DragDropEffects.Link);
                }
            }
        }

        private void roomsLstExistingRooms_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            // Cancel the drag if the mouse moves off the form.
            ListBox lb = sender as ListBox;

            if (lb != null)
            {

                Form f = lb.FindForm();

                // Cancel the drag if the mouse moves off the form. The screenOffset
                // takes into account any desktop bands that may be at the top or left
                // side of the screen.
                if (((Control.MousePosition.X - screenOffset.X) < f.DesktopBounds.Left) ||
                    ((Control.MousePosition.X - screenOffset.X) > f.DesktopBounds.Right) ||
                    ((Control.MousePosition.Y - screenOffset.Y) < f.DesktopBounds.Top) ||
                    ((Control.MousePosition.Y - screenOffset.Y) > f.DesktopBounds.Bottom))
                {

                    e.Action = DragAction.Cancel;
                }
            }
        }

        private void roomsComRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (roomsComRealms.SelectedIndex == -1)
                return;

            IRealm realm = null;

            //If CurrentRealm is not null and we have the CurrentRealm selected, set that as our reference.
            //Saves us from having to loop through all of the other realms via World.GetRealm()
            if (EngineEditor.CurrentRealm != null && roomsComRealms.SelectedItem.ToString() == EngineEditor.CurrentRealm.Name)
                realm = EngineEditor.CurrentRealm;
            else
            //CurrentRealm is not the Realm we have selected, so lets find it.
                realm = (IRealm)EngineEditor.Game.World.GetRealm(roomsComRealms.SelectedItem.ToString());
            
            //In the event that GetRealm can't find the Realm selected for some reason, bail.
            if (realm == null)
                return;

            //Loop through each Zone in the selected Realm and build the combo box collection.
            foreach (IZone zone in realm.Zones.Values)
                roomsComZones.Items.Add(zone.Name);

            //If the CurrentRealm matches the currently selected realm, then check if the CurrentZone is null.
            //If not, then automatically select the current Zone out of the collection.  If CurrentZone is null
            //or does not exist within CurrentRealm, select the first item in the collection
            if (EngineEditor.CurrentRealm != null && EngineEditor.CurrentRealm.Name == roomsComRealms.SelectedItem.ToString())
            {
                if (EngineEditor.CurrentZone != null && roomsComZones.Items.Contains(EngineEditor.CurrentZone.Name))
                {
                    roomsComZones.SelectedItem = EngineEditor.CurrentZone.Name;
                }
            }
            else if (roomsComZones.Items.Count > 0)
                roomsComZones.SelectedIndex = 0;
            
        }

        private void roomsBtnCreateRoom_Click(object sender, EventArgs e)
        {
            if (roomsComRealms.SelectedIndex == -1 || roomsComZones.SelectedIndex == -1)
            {
                MessageBox.Show("You have not specified a Realm or a Zone for this Room to be created in.\n"
                    + "You can either select an existing Realm and Zone from the Environments tab, or use the Realm and Zone editors.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            bool validName = false;
            int value = 1;
            string newName = "New Room" + value.ToString();

            while (!validName)
            {
                if (EngineEditor.CurrentZone.GetRoom(newName) != null)
                {
                    value++;
                    newName = "New Room" + value.ToString();
                }
                else
                    validName = true;
            }

            IRoom room = (IRoom)ScriptFactory.GetScript(EngineSettings.Default.RoomType, null);

            if (room == null)
            {
                if (String.IsNullOrEmpty(EngineSettings.Default.RoomType))
                    MessageBox.Show("The engine setting '" + EngineSettings.Default.RoomType + "' does not have a script name specified. Instancing a new Room failed.",
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Mud Designer Engine failed to instance the specified Room Type '" + EngineSettings.Default.RoomType + "'.  Please make sure that the script exists.",
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            //Set the rooms new name.
            room.Name = newName;

            EngineEditor.CurrentRealm = EngineEditor.Game.World.GetRealm(roomsComRealms.SelectedItem.ToString());
            EngineEditor.CurrentZone = EngineEditor.CurrentRealm.GetZone(roomsComZones.SelectedItem.ToString());
            EngineEditor.CurrentRoom = room;

            //Add the new room to the currently loaded zone.
            EngineEditor.CurrentZone.AddRoom(room);
            roomsLstExistingRooms.Items.Add(room.Name);
            roomsLstExistingRooms.SelectedItem = room.Name;

            //Refresh the UI labels.
            RefreshRoomLabels(EngineEditor.CurrentRealm, EngineEditor.CurrentZone, room);
            RefreshDoorwayList();

            roomsPropertiesRoom.SelectedObject = room;
        }

        private void roomsBtnChangeZone_Click(object sender, EventArgs e)
        {
            frmZones zones = new frmZones();
            zones.ChangingZone = true;
            zones.ShowDialog();

            while (zones.Visible)
            {
                Application.DoEvents();
            }
            zones = null;

            SetupEnvironment();
        }

        private void roomsComZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (roomsComZones.SelectedIndex == -1)
                return;

            roomsLstExistingRooms.Items.Clear();

            IRealm realm = (IRealm)EngineEditor.Game.World.GetRealm(roomsComRealms.SelectedItem.ToString());

            if (realm == null)
                return;

            IZone zone = realm.GetZone(roomsComZones.SelectedItem.ToString());
            if (zone == null)
                return;

            foreach (IRoom room in zone.Rooms.Values)
                roomsLstExistingRooms.Items.Add(room.Name);

            //Check if EngineEditor.CurrentRoom is one of the Rooms within our List. If so, select it.
            if(EngineEditor.CurrentRealm == null || EngineEditor.CurrentZone == null || EngineEditor.CurrentRoom == null)
                return; //Bail, we don't need to go any further.

            if (EngineEditor.CurrentRealm.Name == roomsComRealms.SelectedItem.ToString() 
                && EngineEditor.CurrentZone.Name == roomsComZones.SelectedItem.ToString())
            {
                if (roomsLstExistingRooms.Items.Contains(EngineEditor.CurrentRoom.Name))
                    roomsLstExistingRooms.SelectedItem = EngineEditor.CurrentRoom.Name;
            }
        }

        private void roomsBtnLoadRoom_Click(object sender, EventArgs e)
        {
            if (roomsLstExistingRooms.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a Room from within the Available Rooms list.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (roomsComRealms.SelectedIndex == -1 || roomsComZones.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a Realm and a Zone from the Existing Realm and Existing Zone drop down menus.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            IRealm realm = null;
            IZone zone = null;
            IRoom room = null;

            realm = EngineEditor.Game.World.GetRealm(roomsComRealms.SelectedItem.ToString());
            if (realm == null)
            {
                MessageBox.Show(roomsComRealms.SelectedItem.ToString() + " was not found within the games World collection!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                zone = realm.GetZone(roomsComZones.SelectedItem.ToString());

            if (zone == null)
            {
                MessageBox.Show(roomsComZones.SelectedItem.ToString() + " was not found within the " + realm.Name + " zone collection!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                room = zone.GetRoom(roomsLstExistingRooms.SelectedItem.ToString());

            if (room == null)
            {
                MessageBox.Show("The selected Room '" + roomsLstExistingRooms.SelectedItem.ToString() + "' was not located within the " + zone.Name + " room collection!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                EngineEditor.CurrentRealm = realm;
                EngineEditor.CurrentZone = zone;
                EngineEditor.CurrentRoom = room;
            }

            RefreshDoorwayList();
            RefreshRoomLabels(realm, zone, room);

            roomsPropertiesRoom.SelectedObject = room;
        }

        private void RefreshRoomLabels(IRealm realm, IZone zone, IRoom room)
        {
            if (room == null)
                roomsLblCurrentRoom.Text = "Current Room: None Loaded";
            else
                roomsLblCurrentRoom.Text = "Current Room: " + room.Name;

            if (realm == null || zone == null)
                roomsLblRealmAndZone.Text = "No Location Loaded! New Rooms will be created within the Zone\nthat is selected under the Environments Tab.";
            else
                roomsLblRealmAndZone.Text = "Current Zone: " + realm.Name + "->" + zone.Name;
        }

        private void roomsPropertiesRoom_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Name")
            {
                roomsLstExistingRooms.Items[roomsLstExistingRooms.Items.IndexOf(e.OldValue)] = EngineEditor.CurrentRoom.Name;
                RefreshRoomLabels(EngineEditor.CurrentRealm, EngineEditor.CurrentZone, EngineEditor.CurrentRoom);
            }
        }

        private void roomsBtnDeleteRoom_Click(object sender, EventArgs e)
        {
            if (roomsLstExistingRooms.SelectedIndex == -1)
            {

            }
        }
    }
}
