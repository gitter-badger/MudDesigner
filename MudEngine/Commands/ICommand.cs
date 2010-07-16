//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//MUD Engine
using MudEngine.Commands;
using MudEngine.GameObjects.Characters;
using MudEngine.GameManagement;
using MudEngine.GameObjects.Environment;

namespace MudEngine.Commands
{
    public interface IGameCommand
    {
        //Name of the command
        string Name { get; set; }
        //Used to override commands with the same name
        bool Override { get; set; }
        //Executes the command.
        CommandResults Execute(BaseCharacter player, GameSetup project, Room room, string command);
    }
}
