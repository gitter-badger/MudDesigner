using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Environment
{
    public interface IEnvironment
    {
        string Name { get; set; }
        bool Safe { get; set; }
        string Description { get; set; }
        bool Enabled { get; set; }
    }
}
