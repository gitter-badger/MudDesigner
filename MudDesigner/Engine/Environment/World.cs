using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Core;
using Newtonsoft.Json;

namespace MudDesigner.Engine.Environment
{
    
    public class World : GameObject, IWorld
    {
        [Browsable(false),JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public Dictionary<Guid, IRealm> Realms { get; protected set; }

        public bool IsSafe { get; set; }

        public World()
        {
            Realms = new Dictionary<Guid, IRealm>();
            Name = "World";
        }

        //overloaded member for loading
        public World(Guid id) : base(id)
        {
            Realms = new Dictionary<Guid, IRealm>();
            Name = "World";
        }

        public IRealm GetRealm(Guid id)
        {
            IRealm realm;
            Realms.TryGetValue(id, out realm);

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

            Realms.Add(realm.ID, realm);
        }

        public void RemoveRealm(IRealm realm)
        {
            if (Realms.ContainsKey(realm.ID))
                Realms.Remove(realm.ID);
        }

        public void RemoveRealm(string realmName)
        {
            IRealm realm = Realms.Where(r => r.Value.Name == realmName).Select(r => r.Value).First();

            if (realm == null)
                return;
            else
                Realms.Remove(realm.ID);
        }

        public void BroadcastMessage(string message, List<Mobs.IPlayer> playersToOmit = null)
        {
            foreach (IRealm realm in Realms.Values)
            {
                if (playersToOmit == null)
                    realm.BroadcastMessage(message);
                else
                    realm.BroadcastMessage(message, playersToOmit);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
          
        }
    }
}
