using System.Collections.Generic;

using MudDesigner.Engine.Abstract.Core;
using System;
using System.Collections;
using MudDesigner;
namespace MudDesigner.Engine.Abstract.Environment
{
    public interface IRoom
    {
        IZone Zone { get; set; }
        bool Safe { get; set; }

        Dictionary<string, IPlayer> Occupants { get; set; }
        Dictionary<TravelDirections, IDoor> Doorways { get;  }
         
    }
}