using System.Collections.Generic;

using MudDesigner.Engine.Core;
using System;
using System.Collections;
using MudDesigner;
namespace MudDesigner.Engine.Environment
{
    public interface IRoom
    {
        IZone Zone { get; set; }
        bool Safe { get; set; }

        Dictionary<string, IPlayer> Occupants { get; set; }
        Dictionary<TravelDirections, IDoor> Doorways { get;  }
         
    }
}