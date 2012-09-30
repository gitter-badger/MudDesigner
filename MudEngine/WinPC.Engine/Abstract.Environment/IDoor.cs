using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Abstract.Environment
{
    public interface IDoor
    {
        bool Locked { get; set; }

        TravelDirections MountedDirection { get; }
    }
}