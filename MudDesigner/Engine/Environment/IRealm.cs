using System;
using System.Collections.Generic;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Objects;

namespace MudDesigner.Engine.Environment
{
    public interface IRealm: IGameObject
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