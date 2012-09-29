namespace MudDesigner.Engine.Abstract.Core
using System;
using System.Collections.Generic;
using System.Collections;

using MudDesigner;
namespace MudDesigner.Engine.Abstract.Core
{
    public interface IRoom
    {
        public IZone Zone { get; private set; }
        public bool Safe { get; set; }
        public Dictionary<string, IPlayer> Occupants { get; private set; }

         
    }
}