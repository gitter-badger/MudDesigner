using System;
using System.Collections.Generic;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Objects;
using Newtonsoft.Json;

namespace MudDesigner.Engine.Environment
{
    public interface IRealm: IEnvironment
    {
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        List<IZone> Zones { get; set; }

        void AddZone(IZone zone, bool forceOverwrite);
        void AddZones(IZone[] zones, bool forceOverwrite);
        IZone GetZone(string zoneName);
        void RemoveZone(IZone zone);
    }
}