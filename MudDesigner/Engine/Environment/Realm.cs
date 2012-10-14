using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Environment
{
    public abstract class Realm : IGameObject, IRealm
    {
        //Room Collection
        [Browsable(false)]
        public Dictionary<string, IZone> Zones{ get; protected set; }

        public String Name { get; set; }

        public Realm(string name)
        {
            Zones = new Dictionary<string, IZone>();
            Name = name;
        }

        public virtual void AddZone(IZone zone, bool forceOverwrite = true)
        {
            //Check if room is null.
            if (zone == null)
                return; //No null references within our collections!

            //If this Room already exists, overwrite it
            //but only if 'forceOverwrite' is true
            if (Zones.ContainsKey(zone.Name))
            {
                Zones[zone.Name] = zone;
            }
                //Room does not exist, so lets add it.
            else
            {
                Zones.Add(zone.Name, zone);
            }
        }

        public virtual void AddZones(IZone[] zones, bool forceOverwrite = true)
        {
            foreach (Zone zone in zones)
            {
                AddZone(zone, forceOverwrite);
            }
        }

        public virtual void RemoveZone(IZone zone)
        {
            if (Zones.ContainsKey(zone.Name))
                Zones.Remove(zone.Name);
            else if (Zones.ContainsValue(zone))
                Zones.Remove(zone.Name);
        }

        public virtual void BroadcastMessage(string message, List<IPlayer> playersToOmmit = null)
        {
                foreach (Zone zone in Zones.Values)
                {
                    foreach (Room room in zone.Rooms.Values)
                    {
                        foreach (Player player in room.Occupants.Values)
                        {
                            if (playersToOmmit != null)
                            {
                                if (playersToOmmit.Contains(player))
                                    continue; //Skip this player if it's in the list.
                            }
                            //Send the message
                            player.SendMessage(message);
                        }
                    }
                }
        }

        public void Save(System.IO.BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public void Load(IGame game, System.IO.BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }

        #region == Events ==
        public delegate void OnEnterHandler(IPlayer player, AvailableTravelDirections enteredDirection);
        public event OnEnterHandler OnEnterEvent;
        public virtual void OnEnter(IPlayer player, AvailableTravelDirections enteredDirection)
        {
            BroadcastMessage(player.Name + " has entered from the " + enteredDirection.ToString());
        }
        #endregion

        [Browsable(false)]
        public Guid Id
        {
            get { return Guid.NewGuid(); }
        }

        [Browsable(false)]
        public GameObjectType Type
        {
            get { return GameObjectType.Realm; }
        }
    }
}
