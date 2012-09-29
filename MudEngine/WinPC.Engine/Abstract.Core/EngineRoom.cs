using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Abstract.Core
{
    public abstract class EngineRoom : IRoom
    {
        public IZone Zone { get; set; }
        public bool Safe { get; set; }
        public Dictionary<string, IPlayer> Occupants { get; set; }
    }
}
