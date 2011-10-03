using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Core;
using MudEngine.Runtime;

namespace MudGame.Commands
{
    public class CommandLogin : BaseCommand
    {
        public override void Execute(string command, ICharacter character)
        {
            character.Send("Welcome to " + character.ActiveGame.Name);
        }
    }
}
