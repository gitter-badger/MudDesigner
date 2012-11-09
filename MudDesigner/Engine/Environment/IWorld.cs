using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;
using Newtonsoft.Json;

using MudDesigner.Engine.Mobs;
namespace MudDesigner.Engine.Environment
{
    public interface IWorld 
    {
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        Dictionary<Guid, IRealm> Realms { get; }
        bool IsSafe { get; set; }

        void AddRealm(IRealm realm, bool overwrite = false);
        IRealm GetRealm(Guid realmid);
        IRealm GetRealm(string realmname);
        void RemoveRealm(IRealm realm);
        void RemoveRealm(string realmName);

        void BroadcastMessage(string message, List<IPlayer> playersToOmit = null);
    }
}
