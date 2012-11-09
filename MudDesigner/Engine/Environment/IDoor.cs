using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;

namespace MudDesigner.Engine.Environment
{
    public interface IDoor
    {
        bool Locked { get; }

        IItem Key { get; }

        AvailableTravelDirections FacingDirection { get; }

        IRoom Arrival { get; }

        IRoom Departure { get; }
    }
}