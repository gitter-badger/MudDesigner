using System;
using System.Collections.Generic;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Objects;

namespace MudDesigner.Engine.Environment
{
    public interface IRealm: IEnvironment
    {
        Dictionary<Guid, IZone> Zones { get; }

        void AddZone(IZone zone, bool forceOverwrite);
        void AddZones(IZone[] zones, bool forceOverwrite);
        IZone GetZone(string zoneName);
        void RemoveZone(IZone zone);
    }
}