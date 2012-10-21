using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Actions;

namespace MudDesigner.Engine.Environment
{
    public interface IWorld : ILoadable, ISaveable
    {
        Dictionary<string, IRealm> Realms { get; }
        string Name { get; set; }

        void Create(string name);
        void Create(string name, List<IRealm> realms);

        IRealm GetRealm(string realmName);
        void AddRealm(IRealm realm, bool overwrite = false);
    }
}
