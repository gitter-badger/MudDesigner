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

    public class EngineWorld : GameObject, IWorld
    {
        [Browsable(false), JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public List<IRealm> Realms { get; set; }

        public bool IsSafe { get; set; }

        public EngineWorld()
        {
            Realms = new List<IRealm>();
            Name = "World";
        }

        public IWorld ShallowCopy()
        {
            return (IWorld)this.MemberwiseClone();
        }

        public IWorld DeepCopy()
        {
            return null;
        }

        public void AddRealm(IRealm realm, bool forceOverwrite = false)
        {
            if (realm == null)
                return;

            if (forceOverwrite)
            {
                if (Realms.Contains(realm))
                {
                    Realms.Remove(realm);
                }
            }

            Realms.Add(realm);
        }

        /// <summary>
        /// Retrieves a realm based on the realm name.
        /// Returns null if the realm doesn't exist. 
        /// </summary>
        /// <param name="realm"></param>
        /// <returns></returns>
        public IRealm GetRealm(string realm)
        {
            foreach (IRealm r in Realms)
            {
                if (r.Name.ToLower() == realm.ToLower())
                    return r;
            }

            return null;
        }

        public IRealm[] GetRealms()
        {
            return Realms.ToArray();
        }

        public void RemoveRealm(IRealm realm)
        {
            if (Realms.Contains(realm))
                Realms.Remove(realm);
        }

        public void RemoveRealm(string realmName)
        {
            foreach (IRealm realm in Realms)
            {
                if (realm.Name.ToLower() == realmName.ToLower())
                {
                    Realms.Remove(realm);
                    break;
                }
            }
        }

        public void BroadcastMessage(string message, List<Mobs.IPlayer> playersToOmit = null)
        {
            foreach (IRealm realm in Realms)
            {
                if (playersToOmit == null)
                    realm.BroadcastMessage(message);
                else
                    realm.BroadcastMessage(message, playersToOmit);
            }
        }
    }
}
