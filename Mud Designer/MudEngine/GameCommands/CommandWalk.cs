using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.MudEngine.Interfaces;

namespace MudDesigner.MudEngine.GameCommands
{
    public class CommandWalk : IGameCommand
    {
        public string Name { get; set; }
        public bool Override { get; set; }

        public object Execute(params object[] parameters)
        {
            return null;
        }
    }
}
