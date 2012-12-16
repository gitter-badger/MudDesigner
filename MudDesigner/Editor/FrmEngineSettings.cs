/* frmEngineSettings
 * Product: Mud Designer Editor
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Provides a graphical approach to editing the Mud Designer Engine Settings. Users can assign default Types to
 *                   various game engine objects that run during run-time.
 */

//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Properties;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.States;

using log4net;

namespace MudDesigner.Editor
{
    /// <summary>
    /// Provides a graphical approach to editing the Mud Designer Engine Settings. Users can assign default Types to
    /// various game engine objects that run during run-time.
    /// </summary>
    public partial class FrmEngineSettings : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(FrmEngineSettings)); 
        /// <summary>
        /// Optional Types that can be used by EngineSettings are stored here. The Combo Box UI controls are bound to each collection added
        /// to the SettingsCollection, allowing for the UI to be updated as Types are added or removed without needing to manually update the UI
        /// </summary>
        private Dictionary<string, SortedDictionary<string, string>> settingCollection = new Dictionary<string, SortedDictionary<string, string>>();

        /// <summary>
        /// Gets or Sets if the currently selected Game Objects in the static Editor class require resetting.
        /// If true, all Game Objects will be set to null, requiring the user to re-load them from their respective editors.
        /// This will usually be set to true when an object that is loaded was modified due to changing it's script Type.
        /// </summary>
        private bool requiresReset = false;

        public FrmEngineSettings()
        {
            InitializeComponent();
            //EngineSettings.Default.Reset();
        }

        private void frmEngineSettings_Load(object sender, EventArgs e)
        {
            //Load all of the scripts that have settings associated with them, store them and 
            //present them to the GUI.
            ProcessScripts();

            bool good = false;
            if (EngineSettings.Default.InitialRoom != null)
            {
                string[] env = EngineSettings.Default.InitialRoom.Split('>');

                if (env.Length == 3)
                {
                    IRealm realm = Editor.Game.World.GetRealm(env[0]);
                    if (realm != null)
                    {
                        IZone zone = realm.GetZone(env[1]);
                        if (zone != null)
                        {
                            IRoom room = zone.GetRoom(env[2]);
                            if (room != null)
                            {
                                lblLoginRoom.Text = string.Format("Login Room: {0}>{1}>{2}", realm.Name, zone.Name, room.Name);
                                good = true;
                            }
                        }
                    }
                }
            }

            if (!good)
                lblLoginRoom.Text = "Login Room: None set.";
        }

        private void ProcessScripts()
        {
            //Setup all of our combo boxes. This will build a collection of Types that pertain
            //to each engine setting and then populate the combo box UI control with the collections
            //values. Current engine settings will then be selected by default.
            //The SetupComboBox method is also responsible for updating the progressbar.
            SetupComboBox(defaultGameType, typeof(IGame), EngineSettings.Default.GameScript);
            SetupComboBox(defaultPlayerType, typeof(IPlayer), EngineSettings.Default.PlayerScript);
            SetupComboBox(LoginCompleteState, typeof(IState), EngineSettings.Default.LoginCompletedState);
            SetupComboBox(loginState, typeof(IState), EngineSettings.Default.LoginState);
            SetupComboBox(defaultWorldType, typeof(IWorld), EngineSettings.Default.WorldScript);
            SetupComboBox(realmType, typeof(IRealm), EngineSettings.Default.RealmScript);
            SetupComboBox(zoneType, typeof(IZone), EngineSettings.Default.ZoneScript);
            SetupComboBox(roomType, typeof(IRoom), EngineSettings.Default.RoomScript);
            SetupComboBox(doorType, typeof(IDoor), EngineSettings.Default.DoorScript);

            scriptsPath.Text = EngineSettings.Default.ScriptsPath;
            worldFile.Text = EngineSettings.Default.WorldSaveFile;
            playerSavePath.Text = EngineSettings.Default.PlayerSavePath;
            //Update the progress bar after text boxes are updated.
            scriptProgressBar.Value++;

            //Scan the engine settings collection of script libraries and build our list.
            foreach (string library in EngineSettings.Default.ScriptLibrary)
            {
                scriptLibrary.Items.Add(library);
            }
            //We're done so set the progressbar to 100%
            //TODO: Setting progressbar.visible = false in this method hides
            //the progressbar without showing the progress.
            scriptProgressBar.Value = scriptProgressBar.Maximum;
        }

        private void SetupComboBox(ComboBox box, Type implementInterface, string engineSetting)
        {
            //Build an array of objects that implement the specified interface.
            Type[] objects = ScriptFactory.GetTypesWithInterface(implementInterface.Name);
            Type defaultObject = null;

            //Collection of Types that implement the interface.
            SortedDictionary<string, string> objectCollection = new SortedDictionary<string, string>();

            if (objects.Length == 0)
            {
                MessageBox.Show("Warning! You do not have any scripts that implement " + implementInterface.FullName + "!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //Loop through the array and build our collection
            foreach (Type obj in objects)
            {
                //The object Name is placed as the Key so that the Combo Boxes
                //can use it to display. The Fullname is stored as a value so we can
                //reference the full Type later for instancing.

                if (objectCollection.ContainsKey(obj.Name))
                    continue;

                objectCollection.Add(obj.Name, obj.FullName);

                //If this object is the current EngineSetting, store a reference to it.
                if (obj.FullName == engineSetting)
                    defaultObject = obj;
            }

            if (objectCollection.Count > 0)
            {
                //Bind our combo box to the object collection
                box.DataSource = new BindingSource(objectCollection, null); ;

                //Show users the keys
                box.DisplayMember = "key";

                //Tie what the user sees to the actual full Type path.
                //Example: user selects "MyRealm". They are really selecting "MudDesigner.Scripts.MyGame.MyRealm".
                box.ValueMember = "value";

                //Store this collection into the Settings Collection pool.
                //A Collection within a Collection is used, so that I don't need to have 12 different
                //Dictionary members created.
                settingCollection.Add(engineSetting, objectCollection);
            }
            //Increment the progress bar
            scriptProgressBar.Value++;

            //If the default object (current engine setting) is not null
            if (defaultObject != null && objectCollection.Count > 0)
            {
                //Select it and display it for the users so they know what
                //settings are currently being used.
                box.SelectedValue = defaultObject.FullName;
                return;
            }
            //Otherwise select the first object in the combo box.
            else if (objectCollection.Count > 0)
                box.SelectedIndex = 0;
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            //Save the engine settings. This code is not pretty.... - JS

            //IGame
            //Check if we have changed the IGame script. If so, copy the current IGame property values
            //over to the new IGame Type.
            if (defaultGameType.SelectedIndex >= 0)
            {
                //Gets the Value from the collection based on the Key the combo box has selected.
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)defaultGameType.SelectedItem;

                //Only do this if the setting was changed
                if (EngineSettings.Default.GameScript != selectedValue.Value)
                {
                    //Save a reference to our current World data.
                    IGameObject currentGame = (IGameObject)Editor.Game;

                    //Create a instance of the new scripted Type
                    IGameObject newGame = (IGameObject)ScriptFactory.GetScript(selectedValue.Value);

                    //If we found the script, lets copy the properties of the current IGame
                    //object over to the new one. If we didn't copy the properites, all of the games
                    //items, rooms, players, characters would be lost.
                    if (newGame != null)
                    {
                        //Copy the properties from currentGame over to newGame
                        newGame.CopyState(ref currentGame);

                        Editor.Game = (IGame)newGame;
                    }

                    //Save the new IGame game object to the engine settings.
                    EngineSettings.Default.GameScript = selectedValue.Value;
                }
            }

            //IPlayer
            //Check if we have changed the IPlayer script. If so, copy the current IPlayerproperty values
            //over to the new IPlayer Type.
            if (defaultPlayerType.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)defaultPlayerType.SelectedItem;

                //Only do this if the setting was changed
                if (EngineSettings.Default.PlayerScript != selectedValue.Value)
                {
                    EngineSettings.Default.PlayerScript = selectedValue.Value;
                }
            }

            //Login State
            //Check if we have changed the login script.  
            //This is the script that gets executed immediately once the player 
            //has established a network connection with the server.
            if (loginState.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)loginState.SelectedItem;

                //Only do this if the setting was changed
                if (EngineSettings.Default.LoginState != selectedValue.Value)
                {
                    EngineSettings.Default.LoginState = selectedValue.Value;
                }
            }

            //Login Completed State
            //Check if we have changed the initial state used when a player completes the login process.
            if (LoginCompleteState.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)LoginCompleteState.SelectedItem;

                //Only do this if the setting was changed
                if (EngineSettings.Default.LoginCompletedState != selectedValue.Value)
                {
                    EngineSettings.Default.LoginCompletedState = selectedValue.Value;
                }
            }

            //IWorld
            //Check if we have changed the IWorld script. If so, copy the current IWorld property values
            //over to the new IWorld Type.
            if (defaultWorldType.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)defaultWorldType.SelectedItem;

                //Only do this if the setting was changed
                if (EngineSettings.Default.WorldScript != selectedValue.Value)
                {
                    //Save a reference to our current World data.
                    IGameObject currentWorld = (IGameObject)Editor.Game.World;

                    IGameObject newWorld = (IGameObject)ScriptFactory.GetScript(selectedValue.Value);

                    if (newWorld != null)
                    {
                        newWorld.CopyState(ref currentWorld);

                        Editor.Game.World = (IWorld)newWorld;
                    }

                    EngineSettings.Default.WorldScript = selectedValue.Value;
                }
            }

            //IRealm
            //Check if we have changed the IRealm script. If so, copy the current IRealm property values
            //over to the new IRealm Type.
            if (realmType.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)realmType.SelectedItem;

                if (EngineSettings.Default.RealmScript != selectedValue.Value)
                {
                    List<IRealm> newRealmCollection = new List<IRealm>();

                    foreach (IRealm realm in Editor.Game.World.Realms)
                    {
                        IGameObject newRealm = (IGameObject)ScriptFactory.GetScript(selectedValue.Value);

                        if (newRealm != null)
                        {
                            IGameObject tmp = (IGameObject)realm;
                            newRealm.CopyState(ref tmp);
                            newRealmCollection.Add((IRealm)newRealm);
                        }
                    }

                    Editor.Game.World.Realms = newRealmCollection;
                    requiresReset = true;
                }
                EngineSettings.Default.RealmScript = selectedValue.Value;
            }

            //IZone
            //Check if we have changed the IZone script. If so, copy the current IZone property values
            //over to the new IZone Type.
            if (zoneType.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)zoneType.SelectedItem;

                if (EngineSettings.Default.ZoneScript != selectedValue.Value)
                {
                    List<IZone> newZoneCollection = new List<IZone>();

                    foreach (IRealm realm in Editor.Game.World.Realms)
                    {
                        foreach (IZone zone in realm.Zones)
                        {
                            IGameObject newZone = (IGameObject)ScriptFactory.GetScript(selectedValue.Value);

                            if (newZone != null)
                            {
                                IGameObject tmp = (IGameObject)zone;
                                newZone.CopyState(ref tmp);
                                newZoneCollection.Add((IZone)newZone);
                            }

                            realm.Zones = newZoneCollection;
                            newZoneCollection = new List<IZone>();
                            requiresReset = true;
                        }
                    }
                    EngineSettings.Default.ZoneScript = selectedValue.Value;
                }
            }

            //IRoom
            //Check if we have changed the IRoom script. If so, copy the current IRoom property values
            //over to the new IRoom Type.
            if (roomType.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)roomType.SelectedItem;

                if (EngineSettings.Default.RoomScript != selectedValue.Value)
                {
                    List<IRoom> newRoomCollection = new List<IRoom>();

                    foreach (IRealm realm in Editor.Game.World.Realms)
                    {
                        foreach (IZone zone in realm.Zones)
                        {
                            foreach (IRoom room in zone.Rooms)
                            {
                                IGameObject newRoom = (IGameObject)ScriptFactory.GetScript(selectedValue.Value);

                                if (newRoom != null)
                                {
                                    IGameObject tmp = (IGameObject)room;
                                    newRoom.CopyState(ref tmp);
                                    newRoomCollection.Add((IRoom)newRoom);
                                }
                            }
                            zone.Rooms = newRoomCollection;
                            newRoomCollection = new List<IRoom>();
                            requiresReset = true;
                        }
                    }
                    EngineSettings.Default.RoomScript = selectedValue.Value;
                }
            }

            //IDoor
            //Check if we have changed the IDoor script. If so, copy the current IDoor property values
            //over to the new IDoor Type.
            if (doorType.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)doorType.SelectedItem;

                if (EngineSettings.Default.DoorScript != selectedValue.Value)
                {
                    Dictionary<AvailableTravelDirections, IDoor> newDoorCollection = new Dictionary<AvailableTravelDirections, IDoor>();

                    foreach (IRealm realm in Editor.Game.World.Realms)
                    {
                        foreach (IZone zone in realm.Zones)
                        {
                            foreach (IRoom room in zone.Rooms)
                            {
                                foreach (IDoor door in room.Doorways.Values)
                                {
                                    IDoor newDoor = (IDoor)ScriptFactory.GetScript(selectedValue.Value);

                                    if (newDoor != null)
                                    {
                                        IGameObject tmp = (IGameObject)door;
                                        newDoor.CopyState(ref tmp);
                                        newDoorCollection.Add(newDoor.FacingDirection, newDoor);
                                    }
                                }
                                room.Doorways = newDoorCollection;
                                newDoorCollection = new Dictionary<AvailableTravelDirections, IDoor>();
                            }
                        }
                    }
                    EngineSettings.Default.DoorScript = selectedValue.Value;
                }
            }

            //Script Folder Name
            //Check if we have changed the scripts path.
            if (!string.IsNullOrEmpty(scriptsPath.Text))
            {
                //Only do this if the setting was changed
                if (EngineSettings.Default.ScriptsPath != scriptsPath.Text)
                {
                    string oldPath = EngineSettings.Default.ScriptsPath;
                    string newPath = Path.Combine(Application.StartupPath, scriptsPath.Text);

                    //Get all of the plug-in script libraries that exists in the old path
                    string[] files = Directory.GetFiles(oldPath, "*.dll");

                    //Copy each file over to the new path.
                    //We only move the libraries, because the library collection will expect
                    //them to exist.
                    foreach (string file in files)
                    {
                        string filename = Path.GetFileName(file);
                        File.Copy(file, Path.Combine(newPath, filename), true);
                    }
                    EngineSettings.Default.ScriptsPath = scriptsPath.Text;
                }
            }

            //Script Libraries
            //Re-creates the list of script assembly libraries that the game is using
            if (scriptLibrary.Items.Count >= 0)
            {
                EngineSettings.Default.ScriptLibrary.Clear();

                foreach (string library in scriptLibrary.Items)
                {
                    EngineSettings.Default.ScriptLibrary.Add(library);
                }
            }

            //World Save Folder
            //Checks if we have changed the save file location for the World
            if (!string.IsNullOrEmpty(worldFile.Text))
            {
                //Only do this if the setting was changed
                if (EngineSettings.Default.WorldSaveFile != worldFile.Text)
                {
                    try
                    {
                        if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, worldFile.Text)))
                            Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, worldFile.Text));

                        //Save the World with the new location
                        FileIO file = new FileIO();
                        file.Save(Editor.Game.World, Path.Combine(Environment.CurrentDirectory, worldFile.Text));
                        //Delete the old file
                        File.Delete(Path.Combine(Environment.CurrentDirectory, EngineSettings.Default.WorldSaveFile));

                        //Save the Engine Setting.
                        EngineSettings.Default.WorldSaveFile = worldFile.Text;
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Failed to change the settings for the World file.");
                    }
                }
            }

            //Player Save Folder
            //Checks if we have changed the save file location for players.
            if (!string.IsNullOrEmpty(playerSavePath.Text))
            {
                //Only do this if the setting was changed
                if (EngineSettings.Default.PlayerSavePath != playerSavePath.Text)
                {
                    if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, playerSavePath.Text)))
                    {
                        Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, playerSavePath.Text));
                    }
                    foreach (string file in Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, EngineSettings.Default.PlayerSavePath), "*.char"))
                    {
                        File.Copy(file, Path.Combine(Environment.CurrentDirectory, playerSavePath.Text, Path.GetFileName(file)));
                        File.Delete(file);
                    }
                    EngineSettings.Default.PlayerSavePath = playerSavePath.Text;
                }
            }

            //Save the engine settings
            EngineSettings.Default.Save();

            //Save the game
            Editor.Game.SaveWorld();

            //Check if we require a reset. If so, clear out Game Objects that might have been edited.
            if (requiresReset)
            {
                Editor.CurrentRealm = null;
                Editor.CurrentZone = null;
                Editor.CurrentRoom = null;

                MessageBox.Show("Due to changing scripts that belong to Environments, currently loaded Environments have been unloaded.\n\nYou will need to re-select the environments you want to work with.", this.Text);
            }

            //Done.
            this.Close();
        }

        private void btnCancelSettings_Click(object sender, EventArgs e)
        {
            //Reset the engine back to what we had prior to opening the Engine Settings editor.
            EngineSettings.Default.Reload();

            //Done.
            this.Close();
        }

        private void btnSetLoginRoom_Click(object sender, EventArgs e)
        {
            frmLoginRoom form = new frmLoginRoom();

            form.ShowDialog();

            while (form.Visible)
            {
                Application.DoEvents();
            }

            form = null;

            //Reset the login room text
            bool good = false;
            if (EngineSettings.Default.InitialRoom != null)
            {
                string[] env = EngineSettings.Default.InitialRoom.Split('>');

                if (env.Length == 3)
                {
                    IRealm realm = Editor.Game.World.GetRealm(env[0]);
                    if (realm != null)
                    {
                        IZone zone = realm.GetZone(env[1]);
                        if (zone != null)
                        {
                            IRoom room = zone.GetRoom(env[2]);
                            if (room != null)
                            {
                                lblLoginRoom.Text = string.Format("Login Room: {0}>{1}>{2}", realm.Name, zone.Name, room.Name);
                                good = true;
                            }
                        }
                    }
                }
            }

            if (!good)
                lblLoginRoom.Text = "Login Room: None set.";
        }
    }
}
