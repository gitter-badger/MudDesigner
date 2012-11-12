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

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Properties;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.States;

namespace MudDesigner.Editor
{
    public partial class frmEngineSettings : Form
    {
        Dictionary<string, SortedDictionary<string, string>> settingCollection = new Dictionary<string, SortedDictionary<string, string>>();

        bool requiresReset = false;

        public frmEngineSettings()
        {
            InitializeComponent();
            //EngineSettings.Default.Reset();
        }

        private void frmEngineSettings_Load(object sender, EventArgs e)
        {
            ProcessScripts();
        }

        private void ProcessScripts()
        {
            SetupComboBox(defaultGameType, typeof(IGame), EngineSettings.Default.GameScript);
            SetupComboBox(defaultPlayerType, typeof(IPlayer), EngineSettings.Default.PlayerScript);
            SetupComboBox(loginSuccessState, typeof(IState), EngineSettings.Default.ClientConnectState);
            SetupComboBox(initialState, typeof(IState), EngineSettings.Default.LoginState);
            SetupComboBox(defaultWorldType, typeof(IWorld), EngineSettings.Default.WorldScript);
            SetupComboBox(realmType, typeof(IRealm), EngineSettings.Default.RealmScript);
            SetupComboBox(zoneType, typeof(IZone), EngineSettings.Default.ZoneScript);
            SetupComboBox(roomType, typeof(IRoom), EngineSettings.Default.RoomScript);
            SetupComboBox(doorType, typeof(IDoor), EngineSettings.Default.DoorScript);

            loginRoom.Text = EngineSettings.Default.InitialRoom;
            scriptsPath.Text = EngineSettings.Default.ScriptsPath;
            worldFile.Text = EngineSettings.Default.WorldSaveFile;
            playerSavePath.Text = EngineSettings.Default.PlayerSavePath;
            scriptProgressBar.Value++;

            foreach (string library in EngineSettings.Default.ScriptLibrary)
            {
                scriptLibrary.Items.Add(library);
            }
            scriptProgressBar.Value = scriptProgressBar.Maximum;
        }

        private void SetupComboBox(ComboBox box, Type implementInterface, string engineSetting)
        {
            Type[] objects = ScriptFactory.GetTypesWithInterface(implementInterface.Name);
            Type defaultObject = null;
            SortedDictionary<string, string> objectCollection = new SortedDictionary<string, string>();

            if (objects.Length == 0)
            {
                MessageBox.Show("Warning! You do not have any scripts that implement " + implementInterface.FullName + "!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            foreach (Type obj in objects)
            {
                //box.Items.Add(obj.Name);
                objectCollection.Add(obj.Name, obj.FullName);

                if (obj.FullName == engineSetting)
                    defaultObject = obj;
            }

            if (objectCollection.Count > 0)
            {
                box.DataSource = new BindingSource(objectCollection, null); ;
                box.DisplayMember = "key";
                box.ValueMember = "value";

                settingCollection.Add(engineSetting, objectCollection);
            }
            scriptProgressBar.Value++;

            if (defaultObject != null && objectCollection.Count > 0)
            {
                box.SelectedValue = defaultObject.FullName;
                return;
            }
            else if (objectCollection.Count > 0)
                box.SelectedIndex = 0;            
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            //IGame
            if (defaultGameType.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)defaultGameType.SelectedItem;

                //Only do this if the setting was changed
                if (EngineSettings.Default.GameScript != selectedValue.Value)
                {
                    //Save a reference to our current World data.
                    IGame currentGame = EngineEditor.Game;

                    dynamic newGame = ScriptFactory.GetScript(selectedValue.Value);

                    if (newGame != null)
                    {
                        currentGame.CopyState(ref newGame);

                        EngineEditor.Game = newGame;
                    }

                    EngineSettings.Default.GameScript = selectedValue.Value;
                }
            }

            //IPlayer
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
            if (loginSuccessState.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)loginSuccessState.SelectedItem;

                //Only do this if the setting was changed
                if (EngineSettings.Default.LoginState != selectedValue.Value)
                {
                    EngineSettings.Default.LoginState = selectedValue.Value;
                }
            }

            //Login Room
            if (!string.IsNullOrEmpty(loginRoom.Text))
            {
                //Only do this if the setting was changed
                if (EngineSettings.Default.InitialRoom != loginRoom.Text)
                {
                    EngineSettings.Default.InitialRoom = loginRoom.Text;
                }
            }

            //Initial State
            if (initialState.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)initialState.SelectedItem;

                //Only do this if the setting was changed
                if (EngineSettings.Default.ClientConnectState != selectedValue.Value)
                {
                    EngineSettings.Default.ClientConnectState = selectedValue.Value;
                }
            }

            //IWorld
            if (defaultWorldType.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)defaultWorldType.SelectedItem;

                //Only do this if the setting was changed
                if (EngineSettings.Default.WorldScript != selectedValue.Value)
                {
                    //Save a reference to our current World data.
                    IWorld currentWorld = EngineEditor.Game.World;

                    dynamic newWorld = ScriptFactory.GetScript(selectedValue.Value);

                    if (newWorld != null)
                    {
                        currentWorld.CopyState(ref newWorld);

                        EngineEditor.Game.World = newWorld;
                    }

                    EngineSettings.Default.WorldScript = selectedValue.Value;
                }
            }

            //IRealm
            if (realmType.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)realmType.SelectedItem;

                if (EngineSettings.Default.RealmScript != selectedValue.Value)
                {
                    List<IRealm> newRealmCollection = new List<IRealm>();

                    foreach (IRealm realm in EngineEditor.Game.World.Realms)
                    {
                        dynamic newRealm = ScriptFactory.GetScript(selectedValue.Value);

                        if (newRealm != null)
                        {
                            realm.CopyState(ref newRealm);
                            newRealmCollection.Add(newRealm);
                        }
                    }

                    EngineEditor.Game.World.Realms = newRealmCollection;
                    requiresReset = true;
                }
                EngineSettings.Default.RealmScript = selectedValue.Value;
            }

            //IZone
            if (zoneType.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)zoneType.SelectedItem;

                if (EngineSettings.Default.ZoneScript != selectedValue.Value)
                {
                    List<IZone> newZoneCollection = new List<IZone>();

                    foreach (IRealm realm in EngineEditor.Game.World.Realms)
                    {
                        foreach (IZone zone in realm.Zones)
                        {
                            dynamic newZone = ScriptFactory.GetScript(selectedValue.Value);

                            if (newZone != null)
                            {
                                zone.CopyState(ref newZone);
                                newZoneCollection.Add(newZone);
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
            if (roomType.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)roomType.SelectedItem;

                if (EngineSettings.Default.RoomScript != selectedValue.Value)
                {
                    List<IRoom> newRoomCollection = new List<IRoom>();

                    foreach (IRealm realm in EngineEditor.Game.World.Realms)
                    {
                        foreach (IZone zone in realm.Zones)
                        {
                            foreach (IRoom room in zone.Rooms)
                            {
                                dynamic newRoom = ScriptFactory.GetScript(selectedValue.Value);

                                if (newRoom != null)
                                {
                                    room.CopyState(ref newRoom);
                                    newRoomCollection.Add(newRoom);
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
            if (doorType.SelectedIndex >= 0)
            {
                KeyValuePair<string, string> selectedValue = (KeyValuePair<string, string>)doorType.SelectedItem;

                if (EngineSettings.Default.DoorScript != selectedValue.Value)
                {
                    Dictionary<AvailableTravelDirections, IDoor> newDoorCollection = new Dictionary<AvailableTravelDirections, IDoor>();

                    foreach (IRealm realm in EngineEditor.Game.World.Realms)
                    {
                        foreach (IZone zone in realm.Zones)
                        {
                            foreach (IRoom room in zone.Rooms)
                            {
                                foreach (IDoor door in room.Doorways.Values)
                                {
                                    dynamic newDoor = ScriptFactory.GetScript(selectedValue.Value);

                                    if (newDoor != null)
                                    {
                                        door.CopyState(ref newDoor);
                                        newDoorCollection.Add(door.FacingDirection, newDoor);
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
                    foreach(string file in files)
                    {
                        string filename = Path.GetFileName(file);
                        File.Copy(file, Path.Combine(newPath, filename), true);
                    }
                    EngineSettings.Default.ScriptsPath = scriptsPath.Text;
                }
            }

            //Script Libraries - The list is always re-created for now.
            if (scriptLibrary.Items.Count >= 0)
            {
                EngineSettings.Default.ScriptLibrary.Clear();

                foreach (string library in scriptLibrary.Items)
                {
                    EngineSettings.Default.ScriptLibrary.Add(library);
                }
            }

            //World Save Folder
            if (!string.IsNullOrEmpty(worldFile.Text))
            {
                //Only do this if the setting was changed
                if (EngineSettings.Default.WorldSaveFile != worldFile.Text)
                {
                    EngineSettings.Default.WorldSaveFile = worldFile.Text;
                }
            }

            //Player Save Folder
            if (!string.IsNullOrEmpty(playerSavePath.Text))
            {
                //Only do this if the setting was changed
                if (EngineSettings.Default.PlayerSavePath != playerSavePath.Text)
                {
                    EngineSettings.Default.PlayerSavePath = playerSavePath.Text;
                }
            }

            //Save the engine settings
            EngineSettings.Default.Save();

            //Save the game
            EngineEditor.Game.SaveWorld();

            if (requiresReset)
            {
                EngineEditor.CurrentRealm = null;
                EngineEditor.CurrentZone = null;
                EngineEditor.CurrentRoom = null;

                MessageBox.Show("Due to changing scripts that belong to Environments, currently loaded Environments have been unloaded.\n\nYou will need to re-select the environments you want to work with.", this.Text);
            }
            //Done.
            this.Close();
        }

        private void btnCancelSettings_Click(object sender, EventArgs e)
        {
            EngineSettings.Default.Reset();
            this.Close();
        }
    }
}
