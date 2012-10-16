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
        object selectedObject;

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
                            AvailableRealms.Items.Add(e.ChangedItem.Value.ToString());
                            game.World.Realms.Remove(e.OldValue.ToString());
                            game.World.Realms.Add(e.ChangedItem.Value.ToString(), (IRealm)selectedObject);
                            AvailableRealms.SelectedItem = e.ChangedItem.Value.ToString();
                            break;
                        case "Zones":
                            AvailableZones.Items.Remove(e.OldValue);
                            AvailableZones.Items.Add(e.ChangedItem.Value.ToString());
                            IZone zone = (IZone)objectProperties.SelectedObject;
                            IRealm realm = (IRealm)game.World.GetRealm(zone.Realm.Name);
                            realm.RemoveZone(realm.Zones[e.OldValue.ToString()]);
                            realm.AddZone(zone, true);
                            break;
                        case "Rooms":
                            AvailableRooms.Items.Remove(e.OldValue);
                            AvailableRooms.Items.Add(e.ChangedItem.Value.ToString());
                            break;
                    }
                    objectProperties_SelectedObjectsChanged(null, null);
                }
            }
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

            EditorLib.RoomControl control = new EditorLib.RoomControl();
            control.SelectedRoom = (IRoom)ScriptFactory.GetScript(MudDesigner.Engine.Properties.Engine.Default.RoomType, null);
            
            RoomEditor_Properties.Panel1.Controls.Add(control);
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

            selectedObject = game.World.GetRealm(AvailableRealms.SelectedItem.ToString());
            objectProperties.SelectedObject = selectedObject;
        }

        private void AvailableZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AvailableZones.SelectedIndex == -1)
                return;

            IRealm r = game.World.GetRealm(AvailableRealms.SelectedItem.ToString());

            selectedObject = r.Zones[AvailableZones.SelectedItem.ToString()];
            objectProperties.SelectedObject = selectedObject;
        }

        private void objectProperties_SelectedObjectsChanged(object sender, EventArgs e)
        {
            dynamic value = objectProperties.SelectedObject;
            SelectedObjectLabel.Text = objectProperties.SelectedObject.GetType().FullName + "  (" + value.Name + ")";
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
