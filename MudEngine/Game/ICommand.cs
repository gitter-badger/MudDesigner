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

namespace MudEngine.GameManagement
{
    public interface IGameCommand
    {
        //Name of the command
        String Name { get; set; }
        //Used to override commands with the same name
        Boolean Override { get; set; }

        //Used when the player enters the help command
        List<String> Help { get; set; }

        //Executes the command.
        void Execute(String command, BaseCharacter player);
    }
}
