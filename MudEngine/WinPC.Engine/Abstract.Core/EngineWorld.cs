using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Abstract.Objects;

namespace MudDesigner.Engine.Abstract.Core
{
    
    public class EngineWorld :BaseGameObject, IWorld, IGameObject
    {
        public Dictionary<string, IRealm> Realms { get; protected set; }

        public string Name { get; set; }

        public IRealm GetRealm(string realmName)
        {
            IRealm realm;
            Realms.TryGetValue(realmName, out realm);

            return realm;
        }

        public bool Load(string filename)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public void Create(string name)
        {
            throw new NotImplementedException();
        }

        public void Create(string name, List<IRealm> realms)
            {
                throw new NotImplementedException();
            }
        }
}
