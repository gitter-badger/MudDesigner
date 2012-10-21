using System;
using System.Collections.Generic;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Environment
{
    public interface IRealm
    {
        Dictionary<string, IZone> Zones { get; }
        string Name { get; set; }

        void AddZone(IZone zone, bool forceOverwrite);
        void AddZones(IZone[] zones, bool forceOverwrite);
        IZone GetZone(string zoneName);

        void RemoveZone(IZone zone);
        void BroadcastMessage(string message, List<IPlayer> playersToOmit);
    }
}