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

        BaseRealm currentRealm;
        BaseZone currentZone;
        BaseRoom currentRoom;

        //Drag & drop related fields.
        private int indexOfItemUnderMouseToDrag;
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

            foreach (string reference in MudDesigner.Engine.Properties.EngineSettings.Default.ScriptLibrary)
                CompileEngine.AddAssemblyReference(System.Environment.CurrentDirectory + "\\" + reference);

            CompileEngine.Compile(MudDesigner.Engine.Properties.EngineSettings.Default.ScriptsPath);

            //Add the engine assembly to the Script Factory
            ScriptFactory.AddAssembly(Assembly.GetExecutingAssembly());
            //Add the compiled scripts assembly to the Script Factory
            ScriptFactory.AddAssembly(CompileEngine.CompiledAssembly);

            //Load the Engine assembly
            Assembly assem = null;
            if (File.Exists("MudDesigner.Engine.dll"))
            {
                assem = Assembly.LoadFile(Path.Combine(System.Environment.CurrentDirectory, "MudDesigner.Engine.dll"));
                ScriptFactory.AddAssembly(assem);
            }
            else
            {
                MessageBox.Show("The MudDesigner.Engine.dll is missing!  This is a core component of the editor.  The editor will shut down.", "Mud Designer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (File.Exists("MudDesigner.Scripts.dll"))
            {
                assem = Assembly.LoadFile(Path.Combine(System.Environment.CurrentDirectory, "MudDesigner.Scripts.dll"));
                ScriptFactory.AddAssembly(assem);
            }

            Type[] gameObjects = ScriptFactory.FindInheritedTypes("MudDesigner.Engine.Objects.BaseItem");

            if (gameObjects.Length > 0)
            {
                foreach (Type t in gameObjects)
                {
                    if (t.IsAbstract || t.IsEnum || t.IsInterface || t.IsValueType)
                        continue;
                    if (t.BaseType.Name == "BaseItem")
                    {
                        treeStaticObjects.Nodes.Add(t.Name);
                    }
                    else
                    {
                        if (treeStaticObjects.Nodes.Count == 0)
                        {
                            string baseType = t.BaseType.Name;
                            List<string> items = new List<string>();

                            while (baseType != "BaseItem")
                            {
                                items.Add(baseType);
                                Type parentType = t.BaseType;
                                baseType = parentType.BaseType.Name;
                            }

                            foreach (string parent in items)
                            {
                                treeStaticObjects.Nodes.Add(new TreeNode(parent));
                            }
                        }
                        else
                        {
                            try
                            {
                                foreach (TreeNode collection in treeStaticObjects.Nodes)
                                {
                                    TreeNode n = SearchNode(collection, t.BaseType.Name);
                                    if (n == null)
                                        continue;
                                    else
                                    {
                                        n.Nodes.Add(new TreeNode(t.Name));
                                        break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine(ex.Message);
                            }
                        }
                    }
                }
            }

            game = (Game)ScriptFactory.FindInheritedScript("MudDesigner.Engine.Core.Game", null);
            game.Initialize(null); //Don't need a server.
            game.RestoreWorld();
            foreach (var r in game.World.Realms.Values)
            {
                AvailableRealms.Items.Add(r.Name);
            }
            //TODO - Make sure the game is loaded properly.
        }

        private TreeNode SearchNode(TreeNode node, string search)
        {
            if (node.Text == search)
                return node;

            foreach (TreeNode n in node.Nodes)
            {
                if (n.Text == search)
                    return n;
                else
                {
                    return SearchNode(n, search);
                }
            }

            return null;
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
                            //Replace the old name with the new one in the list box.
                            AvailableRealms.Items[AvailableRealms.Items.IndexOf(e.OldValue)] = currentRealm.Name;
                            break;
                        case "Zones":
                            AvailableZones.Items[AvailableZones.Items.IndexOf(e.OldValue)] = currentZone.Name;
                            break;
                        case "Rooms":
                            AvailableRooms.Items[AvailableRooms.Items.IndexOf(e.OldValue)] = currentRoom.Name;
                            break;
                    }
                    objectProperties_SelectedObjectsChanged(null, null);
                }

                lblSaveStatus.Text = "Not Saved.";
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
            game.SaveWorld();
            lblSaveStatus.Text = "Saved";
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

            BaseRoom newRoom = (BaseRoom)ScriptFactory.GetScript(MudDesigner.Engine.Properties.EngineSettings.Default.RoomType);
            newRoom.Name = newName;
            newRoom.Zone= currentZone;
            

            if (newRoom == null || currentZone == null)
            {
                MessageBox.Show("Failed to instance the specified user script '" + MudDesigner.Engine.Properties.EngineSettings.Default.RoomType + "'", "Mud Designer");
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

            
            if (currentRealm == null)
                return;

            while (!validName)
            {
                if (currentRealm.GetZone(newName) != null)
                {
                    value++;
                    newName = "New Zone" + value.ToString();
                }
                else
                    validName = true;
            }

            BaseZone zone = (BaseZone)ScriptFactory.GetScript(MudDesigner.Engine.Properties.EngineSettings.Default.ZoneType);

            zone.Name = newName;
            zone.Realm = currentRealm;
            currentRealm.AddZone(zone, true);

            AvailableZones.Items.Add(zone.Name);
            AvailableZones.SelectedItem = zone.Name;

            //Open the Realms collection for display.
            if (GameExplorer.SelectedTab.Text != "Environment")
                GameExplorer.SelectedIndex = 0; //"Environment"
            else if (EnvironmentOptions.SelectedTab.Text != "Zones")
                EnvironmentOptions.SelectedIndex = 1; //"Zones"
        }

        /// <summary>
        /// Environment->New Realm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newRealmToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

            currentRealm = (BaseRealm)game.World.GetRealm(AvailableRealms.SelectedItem.ToString());

            if (currentRealm == null)
            {
                MessageBox.Show("Failed to retrieve the selected Realm.", "Mud Designer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; //For some reason, this Realm does not exist...
            }

            objectProperties.SelectedObject = currentRealm;
            statusSelectedObject.Text = "Selected: " + currentRealm.ToString();

            AvailableZones.Items.Clear();

            foreach (IZone zone in currentRealm.Zones.Values)
            {
                AvailableZones.Items.Add(zone.Name);
            }
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

            currentZone = (BaseZone)currentRealm.GetZone(AvailableZones.SelectedItem.ToString());

            if (currentZone == null)
            {
                MessageBox.Show("Failed to retrieve the selected Zone.", "Mud Designer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objectProperties.SelectedObject = currentZone;
            statusSelectedObject.Text = "Selected: " + currentZone.ToString();

            AvailableRooms.Items.Clear();
            foreach (IRoom room in currentZone.Rooms.Values)
            {
                AvailableRooms.Items.Add(room.Name);
            }
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

            currentRoom = (BaseRoom)currentZone.GetRoom(AvailableRooms.SelectedItem.ToString());
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
            BaseRoom room = (BaseRoom)currentZone.GetRoom(data.ToString());

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
            if (currentRoom == null)
                return;

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

        private void loadRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentRoom == null)
                return;

            ToolStripDropDownItem menuItem = (ToolStripDropDownItem)sender;
            ContextMenuStrip menu = (ContextMenuStrip)menuItem.Owner;
            Button selectedButton = (Button)menu.SourceControl;

            string[] values = selectedButton.Text.Split('\n');
            AvailableTravelDirections direction = TravelDirections.GetTravelDirectionValue(values[0]);

            IRoom room = currentRoom.Doorways[direction].Arrival;
            if (AvailableRooms.Items.Contains(room.Name))
            {
                AvailableRooms.SelectedItem = room.Name;
            }
            else
            {
                if (AvailableRealms.Items.Count == 0)
                {
                    foreach (string realm in AvailableRealms.Items)
                    {
                        AvailableRealms.SelectedItem = realm;
                        if (AvailableZones.Items.Count == 0)
                            continue;
                        else
                        {
                            foreach (string zone in AvailableZones.Items)
                            {
                                AvailableZones.SelectedItem = zone;
                                if (AvailableRooms.Items.Count == 0)
                                    continue;
                                else
                                {
                                    if (AvailableRooms.Items.Contains(room.Name))
                                    {
                                        AvailableRooms.SelectedItem = room.Name;
                                        return;
                                    }
                                    else
                                        continue;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void mnuDeleteEnvironmentItem_Click(object sender, EventArgs e)
        {
            ToolStripDropDownItem menuItem = (ToolStripDropDownItem)sender;
            ContextMenuStrip menu = (ContextMenuStrip)menuItem.Owner;
            ListBox collection = (ListBox)menu.SourceControl;

            switch (collection.Name)
            {
                case "AvailableRealms":
                    game.World.RemoveRealm(collection.SelectedItem.ToString());
                    collection.Items.Remove(collection.SelectedItem);

                    if (currentRealm.Name == collection.SelectedItem.ToString())
                        currentRealm = null;

                    if (collection.Items.Count > 0)
                        collection.SelectedIndex = 0;
                    else
                        collection.SelectedIndex = -1;

                    break;
            }
        }

        private void loadToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            game.RestoreWorld();

            foreach (var r in game.World.Realms.Values)
            {
                AvailableRealms.Items.Add(r.Name);
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void objectProperties_Click(object sender, EventArgs e)
        {

        }
    }
}
