using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Core;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace MudDesigner.Engine.Environment
{
    
    public class World : GameObject, IWorld
    {
        public Dictionary<Guid, IRealm> Realms { get; protected set; }

        public World() : base()
        {
            Realms = new Dictionary<Guid, IRealm>();
            Name = "World";
        }

        //overloaded member for loading
        public World(Guid id) : base()
        {
            Realms = new Dictionary<Guid, IRealm>();
            Name = "World";
        }

        public IRealm GetRealm(Guid realmid)
        {
            IRealm realm;
            Realms.TryGetValue(realmid, out realm);

            return realm;
        }

        /// <summary>
        /// Retrieves a realm based on the realm name.
        /// Returns null if the realm doesn't exist. 
        /// </summary>
        /// <param name="realm"></param>
        /// <returns></returns>
        public IRealm GetRealm(string realm)
        {
            return Realms.Where(r => r.Value.Name == realm).Select(r => r.Value).FirstOrDefault();
        }


        public void AddRealm(IRealm realm, bool forceOverwrite = false)
        {
            if (realm == null)
                return;

            if (forceOverwrite)
            {
                if (Realms.ContainsValue(realm))
                {
                    foreach (var r in Realms.Values.Where(newRealm => newRealm == realm))
                    {
                        Realms.Remove(r.ID);
                        break; //We removed our Realm, so escape.
                    }
                }
            }

            if (!Realms.Values.Contains<IRealm>(realm))
                Realms.Add(realm.ID, realm);
        }

        public void RemoveRealm(IRealm realm)
        {
            if (Realms.Keys.Contains(realm.ID))
                Realms.Remove(realm.ID);
        }

        public void RemoveRealm(string realmName)
        {
            foreach(var realm in Realms.Values.Where(newRealm => newRealm.Name == realmName))
            {
                RemoveRealm(realm);
                break;
            }
        }

        public void Create(string name)
        {
            throw new NotImplementedException();
        }

        public void Create(string name, List<IRealm> realms)
            {
                throw new NotImplementedException();
            }


        public void Save(System.IO.BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public void Load(IGame game, System.IO.BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
          
        }
    }
}
