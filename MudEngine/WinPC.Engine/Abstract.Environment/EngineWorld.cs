using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Abstract.Objects;
using MudDesigner.Engine.Abstract.Core;

namespace MudDesigner.Engine.Abstract.Environment
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

        public Guid Id
        {
            get { throw new NotImplementedException(); }
        }

        public GameObjectType Type
        {
            get { throw new NotImplementedException(); }
        }

        public void Save(System.IO.BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public void Load(IGame game, System.IO.BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
