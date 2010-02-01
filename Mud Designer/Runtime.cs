//.Net Framework
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//Mud Designer
using MudDesigner.MudEngine.Characters;
using MudDesigner.MudEngine.Characters.Controlled;
using MudDesigner.MudEngine.Characters.NPC;
using MudDesigner.MudEngine.GameCommands;
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameManagement;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;
using MudDesigner.MudEngine.GameObjects.Items;

namespace MudDesigner
{
    public partial class Runtime : Form
    {
        PlayerBasic _Player;
        Room _Room;
        ProjectInformation _Project;

        public Runtime()
        {
            InitializeComponent();
            _Player = new PlayerBasic();
            _Project = new ProjectInformation();
            _Room = new Room();
        }

        private void Runtime_Load(object sender, EventArgs e)
        {
            _Project = (ProjectInformation)_Project.Load(FileManager.GetDataPath(SaveDataTypes.Root));
            if (_Project.InitialLocation.Zone == "")
            {
                MessageBox.Show("No Initial Zone was defined within the Project Information. Please associated a Zone to the Projects Initial Zone setting in order to launch the game.",
                    "Mud Designer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }

            string filename = FileManager.GetDataPath(SaveDataTypes.Root);
            if (_Project.InitialLocation.Realm != "No Realm Associated.")
            {
                filename = Path.Combine(filename, "Realms");
                filename = Path.Combine(filename, _Project.InitialLocation.Realm);
            }

            filename = Path.Combine(filename, "Zones");
            filename = Path.Combine(filename, _Project.InitialLocation.Zone);
            filename = Path.Combine(filename, "Rooms");
            filename = Path.Combine(filename, _Project.InitialLocation.Room);
            filename += ".room";
            _Room = (Room)_Room.Load(filename);
            _Player.CurrentRoom = _Room;
            _Player.OnTravel(AvailableTravelDirections.North);
        }

        private void txtCommand_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
