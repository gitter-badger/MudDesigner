using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Mobs
{
    public interface IClass : IGameObject
    {
        List<IRace> RaceRestrictions { get; set; }

        //TODO: Create a IEquipmentType interface, allowing class restrictions on Equipment.
    }
}
