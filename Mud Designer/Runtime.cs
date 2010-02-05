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

        public void Print(string message)
        {
            txtConsole.Text += message + "\n";
            txtConsole.Select(txtConsole.Text.Length, 0);
        }

        private void Runtime_Load(object sender, EventArgs e)
        {
            Print("Loading project information...");
            _Project = (ProjectInformation)_Project.Load(FileManager.GetDataPath(SaveDataTypes.Root));
            if (_Project.InitialLocation.Zone == "")
            {
                Print("No Initial Zone was defined within the Project Information. Please associated a Zone to the Projects Initial Zone setting in order to launch the game.");
                Application.Exit();
            }

            Print("Loading environment...");
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

            Print("Prepping test player...");
            _Player.CurrentRoom = _Room;
            _Player.OnTravel(AvailableTravelDirections.North);

            Print("Loading Game Commands...");
            CommandEngine.LoadAllCommands();

            Print("Startup Complete.");
            Print(""); //blank line
            txtCommand.Select();

            if (string.IsNullOrEmpty(_Project.CompanyName))
                Print("No company name defined for the project!");
            else
                Print("Created by " + _Project.CompanyName);

            if (string.IsNullOrEmpty(_Project.Website))
                Print("No website defined for the project!");
            else
                Print("Visit us at " + _Project.Website);

            if (string.IsNullOrEmpty(_Project.GameTitle))
                Print("No Game Title defiend for the project!");
            else
                Print(_Project.GameTitle);

            if (string.IsNullOrEmpty(_Project.Version))
                Print("Game Version was not specified.");
            else
                Print(_Project.Version);

            if (string.IsNullOrEmpty(_Project.Story))
                Print("The games startup story has not been created yet!");
            else
                Print(_Project.Story);

            Print("");//blank line
            CommandResults result = CommandEngine.ExecuteCommand("Look", _Player, _Project, _Room, "Look");
            if (result.Result.Length != 0)
                Print(result.Result[0].ToString());
        }

        private void txtCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string[] words = txtCommand.Text.Split(' ');
                string firstWord = "";
                if (words.Length == 0)
                    return;
                firstWord = words[0];
                List<object> arguments = new List<object>();
                foreach (string word in words)
                {
                    if (word == firstWord)
                        continue;
                    arguments.Add(word);
                }
                arguments.Add(_Room);
                arguments.Add(_Player);
                arguments.Add(_Project);
                CommandResults result = CommandEngine.ExecuteCommand(txtCommand.Text, _Player, _Project, _Room, txtCommand.Text);

                if (result.Result == null)
                    return;

                foreach (object obj in result.Result)
                {
                    switch (obj.GetType().Name.ToLower())
                    {
                        case "string":
                            Print(obj.ToString());
                            break;
                        case "room":
                            _Room = (Room)obj;
                            break;
                        case "projectinformation":
                            _Project = (ProjectInformation)obj;
                            break;
                        case "playerbasic":
                            _Player = (PlayerBasic)obj;
                            break;
                    }
                }

                txtCommand.Clear();
            }
        }
    }
}
