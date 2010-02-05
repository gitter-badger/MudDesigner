using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.MudEngine.GameCommands;
using MudDesigner.MudEngine.Characters;
using MudDesigner.MudEngine.GameManagement;
using MudDesigner.MudEngine.GameObjects.Environment;

namespace MudDesigner.MudEngine.Interfaces
{
    public interface IGameCommand
    {
        //Name of the command
        string Name { get; set; }
        //Used to override commands with the same name
        bool Override { get; set; }
        //Executes the command.
        CommandResults Execute(BaseCharacter player, ProjectInformation project, Room room, string command);
    }
}
