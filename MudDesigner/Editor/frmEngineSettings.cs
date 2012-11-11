using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public frmEngineSettings()
        {
            InitializeComponent();
            EngineSettings.Default.Reset();
        }

        private void frmEngineSettings_Load(object sender, EventArgs e)
        {
            SetupComboBox(defaultGameType, typeof(IGame), EngineSettings.Default.GameScript);
            SetupComboBox(defaultPlayerType, typeof(IPlayer), EngineSettings.Default.PlayerScript);
            SetupComboBox(loginSuccessState, typeof(IState), EngineSettings.Default.ClientConnectState);

            loginRoom.Text = EngineSettings.Default.InitialRoom;

            SetupComboBox(initialState, typeof(IState), EngineSettings.Default.LoginState);
            SetupComboBox(defaultWorldType, typeof(IWorld), EngineSettings.Default.WorldScript);
            SetupComboBox(realmType, typeof(IRealm), EngineSettings.Default.RealmScript);
            SetupComboBox(zoneType, typeof(IZone), EngineSettings.Default.ZoneScript);
            SetupComboBox(roomType, typeof(IRoom), EngineSettings.Default.RoomScript);
            SetupComboBox(doorType, typeof(IDoor), EngineSettings.Default.DoorScript);

            scriptsPath.Text = EngineSettings.Default.ScriptsPath;

            foreach (string library in EngineSettings.Default.ScriptLibrary)
            {
                scriptLibrary.Items.Add(library);
            }

            worldFile.Text = EngineSettings.Default.WorldSaveFile;
            playerSavePath.Text = EngineSettings.Default.PlayerSavePath;
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

                if (obj.Name == engineSetting)
                    defaultObject = obj;
            }

            if (objectCollection.Count > 0)
            {
                box.DataSource = new BindingSource(objectCollection, null); ;
                box.DisplayMember = "key";
                box.ValueMember = "value";

                settingCollection.Add(engineSetting, objectCollection);
            }

            if (defaultObject != null && objectCollection.Count > 0)
            {
                box.SelectedItem = defaultObject.Name;
                return;
            }
            else if (objectCollection.Count > 0)
                box.SelectedIndex = 0;
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            if (defaultGameType.SelectedIndex >= 0)
                EngineSettings.Default.GameScript = defaultGameType.SelectedItem.ToString();

            if (defaultWorldType.SelectedIndex >= 0)
            {
                //Only do this if the setting was changed
                if (EngineSettings.Default.WorldScript != defaultWorldType.SelectedItem.ToString())
                {
                    //Save a reference to our current World data.
                    IWorld currentWorld = EngineEditor.Game.World;
                    KeyValuePair<string, string> selectedValue = (KeyValuePair<string,string>)defaultWorldType.SelectedItem;

                    dynamic newWorld = ScriptFactory.GetScript(selectedValue.Value);

                    if (newWorld != null)
                    {
                        currentWorld.CopyState(ref newWorld);

                        EngineEditor.Game.World = currentWorld;
                        EngineEditor.Game.SaveWorld();
                        EngineSettings.Default.WorldScript = defaultWorldType.SelectedItem.ToString();
                    }
                }
            }
            //EngineSettings.Default.Save();
            this.Close();
        }

        private void btnCancelSettings_Click(object sender, EventArgs e)
        {
            EngineSettings.Default.Reset();
            this.Close();
        }
    }
}
