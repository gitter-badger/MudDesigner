using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MudDesigner.MudEngine.Interfaces;
using MudDesigner.MudEngine.Characters;
using MudDesigner.MudEngine.GameManagement;
using MudDesigner.MudEngine.GameObjects.Environment;

namespace MudDesigner.MudEngine.GameCommands
{
    public class CommandExit : IGameCommand
    {
        public bool Override { get; set; }
        public string Name { get; set; }

        public CommandResults Execute(BaseCharacter player, ProjectInformation project, Room room, string command)
        {
            Application.Exit();

            return new CommandResults();
        }
    }
}
