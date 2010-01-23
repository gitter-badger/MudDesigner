using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.MudEngine.Interfaces
{
    public interface IGameCommand
    {
        //Name of the command
        string Name { get; set; }
        //Used to override commands with the same name
        bool Override { get; set; }
        //Executes the command.
        object Execute(params object[] Parameter);
    }
}
