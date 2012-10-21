using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Scripting;

namespace MudDesigner.Editor
{
    public partial class Editor : Form
    {
        Game game;

        Realm currentRealm;
        Zone currentZone;
        Room currentRoom;

        //Drag & drop related fields.
        private int indexOfItemUnderMouseToDrag;
        private int indexOfItemUnderMouseToDrop;

        private Rectangle dragBoxFromMouseDown;
        private Point screenOffset;

        public Editor()
        {
            InitializeComponent();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            //Compile the game scripts
            CompileEngine.AddAssemblyReference("MudDesigner.Engine.dll");
            CompileEngine.Compile(MudDesigner.Engine.Properties.Engine.Default.ScriptsPath);

            //Add the engine assembly to the Script Factory
            ScriptFactory.AddAssembly(Assembly.GetExecutingAssembly());
            //Add the compiled scripts assembly to the Script Factory
            ScriptFactory.AddAssembly(CompileEngine.CompiledAssembly);

            //Load the Engine assembly
            Assembly assem = Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, "MudDesigner.Engine.dll"));
            ScriptFactory.AddAssembly(assem);
            assem = Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, "MudDesigner.Scripts.dll"));
            ScriptFactory.AddAssembly(assem);

            Type[] gameObjects = ScriptFactory.FindInheritedTypes("MudDesigner.Engine.Core.BaseGameObject");

            if (gameObjects.Length > 0)
            {
                foreach (Type t in gameObjects)
                {
                    if (t.IsAbstract || t.IsEnum || t.IsInterface || t.IsValueType)
                        continue;
                    objectBrowser.Items.Add(t.Name);
                }
            }

            game = (Game)ScriptFactory.FindInheritedScripted("MudDesigner.Engine.Core.Game", null);
            game.Initialize(null); //Don't need a server.

            //TODO - Make sure the game is loaded properly.
        }

        /// <summary>
        /// Game Explorer Object Propertiers Pane
        /// Handles changes to the objects properties that need to be reflected onto the
        /// GUI in some manor.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void objectProperties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Name")
            {
                if (GameExplorer.SelectedTab.Text == "Environment")
                {
                    switch (EnvironmentOptions.SelectedTab.Text)
                    {
                        case "Realms":
                            //Remove the old name from the listbox and add the new one
                            AvailableRealms.Items.Remove(e.OldValue);
                            AvailableRealms.Items.Add(currentRealm.Name);
                            game.World.AddRealm(currentRealm, true); //Overwrite the previous Realm entry in order to update it's Key.
                            AvailableRealms.SelectedItem = currentRealm.Name;
                            break;
                        case "Zones":
                            AvailableZones.Items.Remove(e.OldValue);
                            AvailableZones.Items.Add(e.ChangedItem.Value.ToString());
                            currentRealm.AddZone(currentZone, true);
                            AvailableZones.SelectedItem = currentZone.Name;
                            break;
                        case "Rooms":
                            AvailableRooms.Items.Remove(e.OldValue);
                            AvailableRooms.Items.Add(e.ChangedItem.Value.ToString());
                            currentZone.AddRoom(currentRoom, true);
                            AvailableRooms.SelectedItem = currentRoom.Name;
                            break;
                    }
                    objectProperties_SelectedObjectsChanged(null, null);
                }
            }

            //Save the game.
            //game.Save();
        }

        #region MenuStrip items
        /// <summary>
        /// File->Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //game.Save();
        }

        /// <summary>
        /// Environment->New Room
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //We have to have a Zone selected to create the room within.
            if (AvailableZones.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a Zone to create the Room within first.", "Mud Designer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            bool validName = false;
            int value = 1;
            string newName = "New Room" + value.ToString();

            while (!validName)
            {
                if (currentZone.GetRoom(newName) != null)
                {
                    value++;
                    newName = "New Room" + value.ToString();
                }
                else
                    validName = true;
            }

            Room newRoom = (Room)ScriptFactory.GetScript(MudDesigner.Engine.Properties.Engine.Default.RoomType, newName, currentZone, false);

            if (newRoom == null || currentZone == null)
            {
                MessageBox.Show("Failed to instance the specified user script '" + MudDesigner.Engine.Properties.Engine.Default.RoomType + "'", "Mud Designer");
                return;
            }

            currentZone.AddRoom(newRoom, false);

            objectProperties.SelectedObject = newRoom;
            statusSelectedObject.Text = "Selected: " + newRoom.ToString();
            AvailableRooms.Items.Add(newRoom.Name);
            AvailableRooms.SelectedItem = newRoom.Name;

            if (GameExplorer.SelectedTab.Text != "Environment")
                GameExplorer.SelectedIndex = 0; //"Environment"
            if (EnvironmentOptions.SelectedTab.Text != "Rooms")
                EnvironmentOptions.SelectedIndex = 2; //"Rooms"
        }

        /// <summary>
        /// Environment->New Zone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AvailableRealms.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a Realm to create the Zone within first.", "Mud Designer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //We need to make sure we never have a duplicate name.
            int value = 1;
            string newName = "New Zone" + value.ToString();
            bool validName = false;

            IRealm r = game.World.GetRealm(AvailableRealms.SelectedItem.ToString());
            if (r == null)
                return;

            while (!validName)
            {
                if (r.Zones.ContainsKey(newName))
                {
                    value++;
                    newName = "New Zone" + value.ToString();
                }
                else
                    validName = true;
            }

            Zone zone = (Zone)ScriptFactory.GetScript(MudDesigner.Engine.Properties.Engine.Default.ZoneType, newName, r);
            r.AddZone(zone, true);

            AvailableZones.Items.Add(zone.Name);
            AvailableZones.SelectedItem = zone.Name;

            //Open the Realms collection for display.
            if (GameExplorer.SelectedTab.Text != "Environment")
                GameExplorer.SelectedIndex = 0; //"Environment"
            if (EnvironmentOptions.SelectedTab.Text != "Zones")
                EnvironmentOptions.SelectedIndex = 1; //"Zones"
        }

        /// <summary>
        /// Environment->New Realm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newRealmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //We need to make sure we never have a duplicate name.
            int value = 1;
            string newName = "New Realm" + value.ToString();
            bool validName = false;

            while (!validName)
            {
                if (game.World.Realms.ContainsKey(newName))
                {
                    value++;
                    newName = "New Realm" + value.ToString();
                }
                else
                    validName = true;
            }

            Realm realm = (Realm)ScriptFactory.GetScript(MudDesigner.Engine.Properties.Engine.Default.RealmType, newName);
            game.World.AddRealm(realm);

            AvailableRealms.Items.Add(realm.Name);
            AvailableRealms.SelectedItem = realm.Name;

            if (GameExplorer.SelectedTab.Text != "Environment")
                GameExplorer.SelectedIndex = 0; //"Environment"
            if (EnvironmentOptions.SelectedTab.Text != "Realms")
                EnvironmentOptions.SelectedIndex = 0; //"Realms"
        }
        #endregion

        /// <summary>
        /// Available Realms Collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AvailableRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AvailableRealms.SelectedIndex == -1)
                return;

            currentRealm = (Realm)game.World.GetRealm(AvailableRealms.SelectedItem.ToString());

            if (currentRealm == null)
            {
                MessageBox.Show("Failed to retrieve the selected Realm.", "Mud Designer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; //For some reason, this Realm does not exist...
            }

            objectProperties.SelectedObject = currentRealm;
            statusSelectedObject.Text = "Selected: " + currentRealm.ToString();
        }

        private void AvailableZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AvailableZones.SelectedIndex == -1)
                return;

            if (currentRealm == null)
            {
                MessageBox.Show("Please select the Realm that this Zone belongs to.", "Mud Designer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            currentZone = (Zone)currentRealm.GetZone(AvailableZones.SelectedItem.ToString());

            if (currentZone == null)
            {
                MessageBox.Show("Failed to retrieve the selected Zone.", "Mud Designer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objectProperties.SelectedObject = currentZone;
            statusSelectedObject.Text = "Selected: " + currentZone.ToString();
        }

        private void objectProperties_SelectedObjectsChanged(object sender, EventArgs e)
        {
            dynamic value = objectProperties.SelectedObject;
            SelectedObjectLabel.Text = objectProperties.SelectedObject.GetType().FullName + "  (" + value.Name + ")";
        }

        private void AvailableRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AvailableRooms.SelectedIndex == -1)
                return;

            if (currentRealm == null || currentZone == null)
            {
                MessageBox.Show("You must select both the owning Realm and Zone to this Room first.", "Mud Designer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            currentRoom = (Room)currentZone.GetRoom(AvailableRooms.SelectedItem.ToString());
            objectProperties.SelectedObject = currentRoom;
            statusSelectedObject.Text = "Selected: " + currentRoom.ToString();

            RefreshDoorwayList();
        }

        private void RefreshDoorwayList()
        {
            North.Text = "North\n\r(Empty)";
            South.Text = "South\n\r(Empty)";
            East.Text = "East\n\r(Empty)";
            West.Text = "West\n\r(Empty)";

            if (currentRoom.Doorways.Count > 0)
            {
                foreach (AvailableTravelDirections door in currentRoom.Doorways.Keys)
                {
                    switch (door)
                    {
                        case AvailableTravelDirections.North:
                            North.Text = "North\n\r" + currentRoom.Doorways[door].Arrival.Name;
                            break;
                        case AvailableTravelDirections.South:
                            South.Text = "South\n\r" + currentRoom.Doorways[door].Arrival.Name;
                            break;
                        case AvailableTravelDirections.East:
                            East.Text = "East\n\r" + currentRoom.Doorways[door].Arrival.Name;
                            break;
                        case AvailableTravelDirections.West:
                            West.Text = "West\n\r" + currentRoom.Doorways[door].Arrival.Name;
                            break;
                    }
                }
            }

            lblRoomName.Text = currentRoom.Name;
        }

        private void AvailableRooms_MouseDown(object sender, MouseEventArgs e)
        {
            if (AvailableRooms.SelectedIndex == -1)
                return;

            // Get the index of the item the mouse is below.
            indexOfItemUnderMouseToDrag = AvailableRooms.IndexFromPoint(e.X, e.Y);

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
            e.Effect = DragDropEffects.Link;
        }

        private void Room_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.Text);
            Room room = (Room)currentZone.GetRoom(data.ToString());

            Button btnDirection = (Button)sender;
            string[] values = btnDirection.Text.Split('\n');
            //Trims out the trailing \r the editor button has.
            string direction = values[0];

            AvailableTravelDirections travelDirection = TravelDirections.GetTravelDirectionValue(direction);
            currentRoom.AddDoorway(travelDirection, room, true, true);
            btnDirection.Text = travelDirection.ToString() + "\n\r" + room.Name;
            AvailableRooms.SelectedItem = currentRoom.Name;
        }

        private void AvailableRooms_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Reset the drag rectangle when the mouse button is raised.
            dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void AvailableRooms_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
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
                    string linkedRoom = AvailableRooms.Items[indexOfItemUnderMouseToDrag].ToString();
      
                    if (linkedRoom == null)
                        return;

                    DragDropEffects dropEffect = AvailableRooms.DoDragDrop(linkedRoom, DragDropEffects.All | DragDropEffects.Link);
                }
            }
        }

        private void AvailableRooms_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
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

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripDropDownItem menuItem = (ToolStripDropDownItem)sender;
            ContextMenuStrip menu = (ContextMenuStrip)menuItem.Owner;
            Button selectedButton = (Button)menu.SourceControl;

            string[] values = selectedButton.Text.Split('\n');
            //index 0 should be the direction
            string direction = values[0];

            foreach (AvailableTravelDirections doorway in currentRoom.Doorways.Keys)
            {
                if (doorway.ToString() == direction)
                {
                    currentRoom.RemoveDoorway(doorway, true);
                    RefreshDoorwayList();
                    break;
                }
            }
        }
    }
}
