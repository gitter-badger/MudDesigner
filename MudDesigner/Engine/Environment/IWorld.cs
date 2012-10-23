using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Actions;
using MudDesigner.Engine.Objects;

namespace MudDesigner.Engine.Environment
{
    public interface IWorld : IGameObject 
    {
        Dictionary<Guid, IRealm> Realms { get; }
        string Name { get; set; }

        void Create(string name);
        void Create(string name, List<IRealm> realms);

        IRealm GetRealm(Guid realmid);
        IRealm GetRealm(string realmname);
        void AddRealm(IRealm realm, bool overwrite = false);
        void RemoveRealm(IRealm realm);
        void RemoveRealm(string realmName);
    }
}
