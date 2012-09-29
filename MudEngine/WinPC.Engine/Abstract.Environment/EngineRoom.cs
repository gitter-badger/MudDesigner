using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Abstract.Core;

namespace MudDesigner.Engine.Abstract.Environment
{
    public enum TravelDirections
    {
        North,
        South,
        East,
        West,
        Up,
        Down,
    }

    public abstract class EngineRoom : IRoom
    {
        public IZone Zone { get; set; }
        public bool Safe { get; set; }
        public Dictionary<string, IPlayer> Occupants { get; set; }
        public Dictionary<TravelDirections, IDoor> Doorways { get; protected set; }

    }
}
