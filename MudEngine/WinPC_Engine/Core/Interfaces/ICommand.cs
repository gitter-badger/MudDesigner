using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Game.Characters;

namespace MudEngine.Core.Interfaces
{
    public interface ICommand
    {
        string Name { get; set; }
        string Description { get; set; }
        List<string> Help { get; set; }

        Boolean Execute(string command, StandardCharacter character);
    }
}
