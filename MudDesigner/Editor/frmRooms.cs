/* frmRooms
 * Product: Mud Designer Editor
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Allows creating and editing Rooms within a specified Zone.
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
using System.Text.RegularExpressions;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Properties;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Mobs;

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
            //Setup our UI environment by populating the combo boxes with Realms and Zones
            //that currently exists as well as making sure the currently loaded Realm and Zone
            //are displayed first.
            SetupEnvironment();

            //Now that everything is setup properly, refresh our UI elements to make sure
            //they reflect what Realm and Zone we are in.
            RefreshRoomLabels(Editor.CurrentRealm, Editor.CurrentZone, Editor.CurrentRoom);
        }

        private void SetupEnvironment()
        {
            //Wipe out our current collections
            roomsComRealms.Items.Clear();
            roomsComZones.Items.Clear();
            roomsLstExistingRooms.Items.Clear();

            //Build the collection of Realms and Zones first.
            foreach (IRealm realm in Editor.Game.World.GetRealms())
            {
                roomsComRealms.Items.Add(realm.Name);
            }

            //Make sure CurrentRealm is not null and that the CurrentRealm exists within the Combobox.
            if (Editor.CurrentRealm != null && roomsComRealms.Items.Contains(Editor.CurrentRealm.Name))
                roomsComRealms.SelectedItem = Editor.CurrentRealm.Name;
            //If not, select the first Realm in the box if there are any items.
            else if (roomsComRealms.Items.Count > 0)
                roomsComRealms.SelectedIndex = 0;

            if (Editor.CurrentRealm != null)
            {
                roomsLblRealmAndZone.Text = "Current Location: " + Editor.CurrentRealm.Name;
            }
            else
                roomsLblRealmAndZone.Text = "No Location Loaded! New Rooms will be created within the Zone\nthat is selected under the Environments Tab.";

            if (Editor.CurrentZone != null)
            {
                roomsLblRealmAndZone.Text += "->" + Editor.CurrentZone.Name;
            }

            //Check if Current Environment references are null. If so, then set them.
            if (Editor.CurrentRealm == null && roomsComRealms.SelectedIndex >= 0)
            {
                Editor.CurrentRealm = Editor.Game.World.GetRealm(roomsComRealms.SelectedItem.ToString());
            }

            //Setup the current zone to use what ever is selected, providing current realm was not set as null.
            //should never have a CurrentZone set with a null CurrentRealm
            if (Editor.CurrentZone == null && roomsComZones.SelectedIndex >= 0 && Editor.CurrentRealm != null)
            {
                Editor.CurrentZone = Editor.CurrentRealm.GetZone(roomsComZones.SelectedItem.ToString());
            }
        }

        private void RefreshDoorwayList()
        {
            //Reset the door buttons to their defualts
            roomsBtnNorth.Text = "North\n(Empty)";
            roomsBtnSouth.Text = "South\n(Empty)";
            roomsBtnEast.Text = "East\n(Empty)";
            roomsBtnWest.Text = "West\n(Empty)";

            //Loop through each doorway within our currently loaded room
            if (Editor.CurrentRoom.Doorways.Count > 0)
            {
                foreach (AvailableTravelDirections door in Editor.CurrentRoom.Doorways.Keys)
                {
                    //Update the button text to reflect the doorway direction and path.
                    switch (door)
                    {
                        case AvailableTravelDirections.North:
                            roomsBtnNorth.Text = "North\n" + Editor.CurrentRoom.Doorways[door].Arrival.Name;
                            break;
                        case AvailableTravelDirections.South:
                            roomsBtnSouth.Text = "South\n" + Editor.CurrentRoom.Doorways[door].Arrival.Name;
                            break;
                        case AvailableTravelDirections.East:
                            roomsBtnEast.Text = "East\n" + Editor.CurrentRoom.Doorways[door].Arrival.Name;
                            break;
                        case AvailableTravelDirections.West:
                            roomsBtnWest.Text = "West\n" + Editor.CurrentRoom.Doorways[door].Arrival.Name;
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
            if (Editor.CurrentRoom == null)
                return;
            else
                e.Effect = DragDropEffects.Link;
        }

        private void Room_DragDrop(object sender, DragEventArgs e)
        {
            if (Editor.CurrentRoom == null)
                return; //Don't do any drag & drop if we have no room loaded.

            var data = e.Data.GetData(DataFormats.Text);
            IRoom room = (IRoom)Editor.CurrentZone.GetRoom(data.ToString());

            Button btnDirection = (Button)sender;
            string[] values = btnDirection.Text.Split('\n');
            //Trims out the trailing \r the editor button has.
            string direction = values[0];

            AvailableTravelDirections travelDirection = TravelDirections.GetTravelDirectionValue(direction);
            Editor.CurrentRoom.AddDoorway(travelDirection, room, true, true);
            
            roomsLstExistingRooms.SelectedItem = Editor.CurrentRoom.Name;

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
            if (Editor.CurrentRealm != null && roomsComRealms.SelectedItem.ToString() == Editor.CurrentRealm.Name)
                realm = Editor.CurrentRealm;
            else
            //CurrentRealm is not the Realm we have selected, so lets find it.
                realm = (IRealm)Editor.Game.World.GetRealm(roomsComRealms.SelectedItem.ToString());
            
            //In the event that GetRealm can't find the Realm selected for some reason, bail.
            if (realm == null)
                return;

            //Loop through each Zone in the selected Realm and build the combo box collection.
            foreach (IZone zone in realm.Zones)
                roomsComZones.Items.Add(zone.Name);

            //If the CurrentRealm matches the currently selected realm, then check if the CurrentZone is null.
            //If not, then automatically select the current Zone out of the collection.  If CurrentZone is null
            //or does not exist within CurrentRealm, select the first item in the collection
            if (Editor.CurrentRealm != null && Editor.CurrentRealm.Name == roomsComRealms.SelectedItem.ToString())
            {
                if (Editor.CurrentZone != null && roomsComZones.Items.Contains(Editor.CurrentZone.Name))
                {
                    roomsComZones.SelectedItem = Editor.CurrentZone.Name;
                }
            }
            else if (roomsComZones.Items.Count > 0)
                roomsComZones.SelectedIndex = 0;
            
        }

        private void roomsBtnCreateRoom_Click(object sender, EventArgs e)
        {
            //if we have no Zone or Realm selected, abort creating a Room.
            if (roomsComRealms.SelectedIndex == -1 || roomsComZones.SelectedIndex == -1)
            {
                MessageBox.Show("You have not specified a Realm or a Zone for this Room to be created in.\n"
                    + "You can either select an existing Realm and Zone from the Environments tab, or use the Realm and Zone editors.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //We need to make sure we never have a duplicate name.
            int value = 1;
            string newName = "New Room" + value; //New Room1

            //Once we have a Room with a unique name, this will become true
            bool validName = false;

            //Loop until we have a valid Room name that is not duplicated.
            while (!validName)
            {
                //In the event this is the first Realm.
                //Prevents infinit loop
                //Since we have zero Rooms that exists, of course the first one will be valid.
                if (Editor.CurrentZone.Rooms.Count == 0)
                    validName = true;

                //If GetRoom() returns null, then the current name doesn't exist.
                if (Editor.CurrentZone.GetRoom(newName) != null)
                {
                    value++;
                    newName = "New Room" + value.ToString();
                }
                else
                    validName = true;
            }

            //Grab a new instance of the default IRoom specified in the engine settings
            IRoom room = (IRoom)ScriptFactory.GetScript(EngineSettings.Default.RoomScript, null);

            //In the event that the script specified by Engine Settings is missing.
            if (room == null)
            {
                //No setting found
                if (String.IsNullOrEmpty(EngineSettings.Default.RoomScript))
                    MessageBox.Show("The engine setting '" + EngineSettings.Default.RoomScript + "' does not have a script name specified. Instancing a new Room failed.",
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Setting exists but the script was missing.
                else
                    MessageBox.Show("Mud Designer Engine failed to instance the specified Room Type '" + EngineSettings.Default.RoomScript + "'.  Please make sure that the script exists.",
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            //Set the rooms new name.
            room.Name = newName;

            //Get the currently selected Realm and Zones and store a reference
            //to them within the static Game Type
            Editor.CurrentRealm = Editor.Game.World.GetRealm(roomsComRealms.SelectedItem.ToString());
            Editor.CurrentZone = Editor.CurrentRealm.GetZone(roomsComZones.SelectedItem.ToString());
            Editor.CurrentRoom = room;

            //Add the new room to the currently loaded zone.
            Editor.CurrentZone.AddRoom(room);
            roomsLstExistingRooms.Items.Add(room.Name);
            roomsLstExistingRooms.SelectedItem = room.Name;

            //Refresh the UI labels.
            RefreshRoomLabels(Editor.CurrentRealm, Editor.CurrentZone, room);
            RefreshDoorwayList();

            //Select the Room for editing.
            roomsPropertiesRoom.SelectedObject = room;
        }

        private void roomsBtnChangeZone_Click(object sender, EventArgs e)
        {
            //If we have no room loaded, abort.
            if (Editor.CurrentRoom == null)
            {
                MessageBox.Show("You must have a Room loaded prior to trying to change the Zone it belongs to.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Creates a new instance of the Zones editor
            frmZones zones = new frmZones();

            //We want to make sure and remove this Room from its current Zone
            if (Editor.CurrentZone != null)
                Editor.CurrentZone.RemoveRoom(Editor.CurrentRoom);

            //Flag changing zone as true so users can double-click and close the editor
            zones.ChangingZone = true;

            //Show it as a dialog. This prevents other editors from starting at the same time.
            //Ensures that the static Editor Type will only be accessed by one editor at a time.
            zones.ShowDialog();

            //While the editor is visible, just keep the App responsive.
            while (zones.Visible)
            {
                Application.DoEvents();
            }

            //Null the reference we have
            zones = null;

            //if no Zone was selected, warn the user that the Room will be lost when the editor
            //is closed or a new room is loaded.
            if (Editor.CurrentZone == null && Editor.CurrentRoom != null)
            {
                MessageBox.Show("You have not selected a Zone to place this Room within. It will not be saved if a Zone is not choosen from the Zone editor!", this.Text);
                return;
            }
            
            //Add the Room to the selected Zone.
            Editor.CurrentZone.AddRoom(Editor.CurrentRoom);

            //Re-setup our UI controls, selecting the currently selected Realm/Zone that this
            //Room belongs to.
            SetupEnvironment();
        }

        private void roomsComZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (roomsComZones.SelectedIndex == -1)
                return;

            //Since we are changing Zones, clear out our Rooms list.
            roomsLstExistingRooms.Items.Clear();

            //Get the current Realm that we have selected.
            IRealm realm = (IRealm)Editor.Game.World.GetRealm(roomsComRealms.SelectedItem.ToString());

            //If the realm doesn't exist, abort.
            if (realm == null)
                return;

            //Get the current Zone we have selected
            IZone zone = realm.GetZone(roomsComZones.SelectedItem.ToString());
            if (zone == null)
                return;

            //Loop through each Room in the current Zone and place it in our list collection
            foreach (IRoom room in zone.Rooms)
                roomsLstExistingRooms.Items.Add(room.Name);

            //Check if EngineEditor.CurrentRoom is one of the Rooms within our List. If so, select it.
            if(Editor.CurrentRealm == null || Editor.CurrentZone == null || Editor.CurrentRoom == null)
                return; //Bail, we don't need to go any further.

            if (Editor.CurrentRealm.Name == roomsComRealms.SelectedItem.ToString() 
                && Editor.CurrentZone.Name == roomsComZones.SelectedItem.ToString())
            {
                if (roomsLstExistingRooms.Items.Contains(Editor.CurrentRoom.Name))
                    roomsLstExistingRooms.SelectedItem = Editor.CurrentRoom.Name;
            }
        }

        private void roomsBtnLoadRoom_Click(object sender, EventArgs e)
        {
            //Can't load a room if nothing is selected
            if (roomsLstExistingRooms.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a Room from within the Available Rooms list.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //If a Realm or Zone isn't selected, abort.
            if (roomsComRealms.SelectedIndex == -1 || roomsComZones.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a Realm and a Zone from the Existing Realm and Existing Zone drop down menus.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Our instance variables
            IRealm realm = null;
            IZone zone = null;
            IRoom room = null;

            //Get a reference to the currently selected Realm.
            realm = Editor.Game.World.GetRealm(roomsComRealms.SelectedItem.ToString());
            if (realm == null)
            {
                MessageBox.Show(roomsComRealms.SelectedItem.ToString() + " was not found within the games World collection!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                //If the Realm exists, we need to get a reference to the currently selected Zone too.
            else
                zone = realm.GetZone(roomsComZones.SelectedItem.ToString());

            if (zone == null)
            {
                MessageBox.Show(roomsComZones.SelectedItem.ToString() + " was not found within the " + realm.Name + " zone collection!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                //If the Zone exists, we need to get a reference to the currently selected Room
            else
                room = zone.GetRoom(roomsLstExistingRooms.SelectedItem.ToString());

            //Check if the Room is null
            if (room == null)
            {
                MessageBox.Show("The selected Room '" + roomsLstExistingRooms.SelectedItem.ToString() + "' was not located within the " + zone.Name + " room collection!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                //Room isn't null, set the static Editor types Current properties. 
                //Lets us modify them with-out having to call a Get() method again.
            else
            {
                Editor.CurrentRealm = realm;
                Editor.CurrentZone = zone;
                Editor.CurrentRoom = room;
            }

            //Refresh our UI
            RefreshDoorwayList();
            RefreshRoomLabels(realm, zone, room);

            //Select the Room object in the property grid for editing.
            roomsPropertiesRoom.SelectedObject = room;
        }

        private void RefreshRoomLabels(IRealm realm, IZone zone, IRoom room)
        {
            //If the room is null, tell the UI
            if (room == null)
                roomsLblCurrentRoom.Text = "Current Room: None Loaded";
                //Otherwise, display the room name along with it's Type.
            else
                roomsLblCurrentRoom.Text = "Current Room: " + room.Name + " (" + room.GetType().Name + ")";

            //If the Realm and Zone are null, warn
            if (realm == null || zone == null)
                roomsLblRealmAndZone.Text = "No Location Loaded! New Rooms will be created within the Zone\nthat is selected under the Environments Tab.";
                //Otherwise display the current Environment path for the loaded Room
            else
                roomsLblRealmAndZone.Text = "Current Zone: " + realm.Name + "->" + zone.Name;
        }

        private void roomsPropertiesRoom_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //If a rooms name is changed, we need to update the UI
            if (e.ChangedItem.Label == "Name")
            {
                roomsLstExistingRooms.Items[roomsLstExistingRooms.Items.IndexOf(e.OldValue)] = Editor.CurrentRoom.Name;
                RefreshRoomLabels(Editor.CurrentRealm, Editor.CurrentZone, Editor.CurrentRoom);

                if (Editor.Game.Server.Enabled)
                {
                    //Move all of the characters that are within this room to the new room.
                    //For some reason, their Location property does not get updated when a Name change happens
                    foreach (IMob mob in Editor.CurrentRoom.Occupants)
                    {
                        mob.Move(Editor.CurrentRoom);
                    }
                }
            }
        }

        private void roomsBtnDeleteRoom_Click(object sender, EventArgs e)
        {
            //Make sure all of the required UI elements are selected
            if (roomsLstExistingRooms.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a Room first.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (roomsComRealms.SelectedIndex == -1 || roomsComZones.SelectedIndex == -1)
            {
                MessageBox.Show("You must select both a Realm and a Zone first.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //Grab a reference to the Realm.
            IRealm realm = Editor.Game.World.GetRealm(roomsComRealms.SelectedItem.ToString());

            if (realm == null)
            {
                MessageBox.Show("Failed to locate the selected Realm within the system for an unknown reason!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //Get a reference to the zone the selected Room belongs to.
            IZone zone = realm.GetZone(roomsComZones.SelectedItem.ToString());
            if (zone == null)
            {
                MessageBox.Show("Failed to locate the selected Zone within the system for an unknown reason!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //Get a reference to the room that the user wants to delete.
            IRoom room = zone.GetRoom(roomsLstExistingRooms.SelectedItem.ToString());
            if (room == null)
            {
                MessageBox.Show("Failed to locate the selected Room within the system for an unknown reason!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //Remove the room from our list collection
            if (Editor.CurrentRoom.ToString() == string.Format("{0}>{1}>{2}", roomsComRealms.SelectedItem.ToString(), roomsComZones.SelectedItem.ToString(), room.Name))
                Editor.CurrentRoom = null;

            roomsLstExistingRooms.Items.Remove(room.Name);
            //Remove the room from the Zone
            zone.RemoveRoom(room);
            //Null the reference.
            room = null;
            RefreshRoomLabels(Editor.CurrentRealm, Editor.CurrentZone, Editor.CurrentRoom);
        }

        private void roomsBtnCloseEditor_Click(object sender, EventArgs e)
        {
            //Close the editor
            this.Close();
        }

        private void mnuClearDoorway_Click(object sender, EventArgs e)
        {
            if (Editor.CurrentRoom == null)
            {
                MessageBox.Show("You need to load a room first!", this.Text);
                return;
            }

            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            ContextMenuStrip strip = (ContextMenuStrip)menu.Owner;

            Button doorButton = (Button)strip.SourceControl;

            string[] content = Regex.Split(doorButton.Text, "\n");

            if (content.Length != 2)
            {
                MessageBox.Show("You need to load a room! If a room is loaded, please make sure it has a Name set.", this.Text);
                return;
            }

            if (content[1] == "Empty")
            {
                MessageBox.Show("There are no doorways for this direction.", this.Text);
                return;
            }
            else
            {
                AvailableTravelDirections direction = TravelDirections.GetTravelDirectionValue(content[0]);

                if (Editor.CurrentRoom.DoorwayExists(direction))
                {
                    Editor.CurrentRoom.RemoveDoorway(direction, true);

                    RefreshDoorwayList();
                }
            }
        }

        private void mnuLoadRoom_Click(object sender, EventArgs e)
        {
            if (Editor.CurrentRoom == null)
            {
                MessageBox.Show("You need to load a room first!", this.Text);
                return;
            }

            //Get the button text that we are over
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            ContextMenuStrip strip = (ContextMenuStrip)menu.Owner;

            Button doorButton = (Button)strip.SourceControl;

            //Split the "North\nRoomName" text up
            string[] content = Regex.Split(doorButton.Text, "\n");

            //Check if we have two entries. If not then the Button is just "North" meaning no room is loaded
            if (content.Length != 2)
            {
                MessageBox.Show("You need to load a room! If a room is loaded, please make sure it has a Name set.", this.Text);
                return;
            }

            //"North\nEmpty" meaning there is no doorway
            if (content[1] == "Empty")
            {
                MessageBox.Show("There are no doorways for this direction.", this.Text);
                return;
            }
                //Otherwise it will be "North\MyRoom"
            else
            {
                //Get the travel direction for the doorway selected
                AvailableTravelDirections direction = TravelDirections.GetTravelDirectionValue(content[0]);

                //Check if the room has a door for the selected travel direction
                if (Editor.CurrentRoom.DoorwayExists(direction))
                {
                    IRoom r = Editor.CurrentRoom.GetDoorway(direction).Arrival;
                    Editor.CurrentRoom = r;

                    //Select the Room for editing.
                    roomsPropertiesRoom.SelectedObject = r;

                    //Select the matching Realm, Zone and Room for the UI combobox and listbox
                    if (roomsComRealms.SelectedItem.ToString() != r.Zone.Realm.Name)
                    {
                        if (roomsComRealms.Items.Contains(r.Zone.Realm.Name))
                            roomsComRealms.SelectedItem = r.Zone.Realm.Name;
                    }

                    if (roomsComZones.SelectedItem.ToString() != r.Zone.Name)
                    {
                        if (roomsComZones.Items.Contains(r.Zone.Name))
                            roomsComZones.SelectedItem = r.Zone.Name;
                    }

                    if (roomsLstExistingRooms.Items.Contains(r.Name))
                        roomsLstExistingRooms.SelectedItem = r.Name;

                    Editor.CurrentRealm = r.Zone.Realm;
                    Editor.CurrentZone = r.Zone;

                    //Refresh the UI labels.
                    RefreshRoomLabels(Editor.CurrentRealm, Editor.CurrentZone, Editor.CurrentRoom);
                    RefreshDoorwayList();
                }
            }
        }
    }
}
