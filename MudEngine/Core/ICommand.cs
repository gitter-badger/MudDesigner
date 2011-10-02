using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.Core
{
    public interface ICommand
    {
        string Name { get; set; }
        string Description { get; set; }
        List<string> Help { get; set; }

        void Execute(string command, ICharacter character);
    }
}
