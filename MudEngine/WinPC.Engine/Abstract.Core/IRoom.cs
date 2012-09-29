using System.Collections.Generic;

using MudDesigner.Engine.Abstract.Core;
using System;
using System.Collections.Generic;
using System.Collections;
using MudDesigner;
namespace MudDesigner.Engine.Abstract.Core
{
    public interface IRoom
    {
        IZone Zone { get; set; }
        bool Safe { get; set; }
        Dictionary<string, IPlayer> Occupants { get; set; }

         
    }
}