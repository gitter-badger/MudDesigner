using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Scripting;

namespace MudDesigner.Engine.Environment
{
    public abstract class Realm : BaseGameObject, IGameObject, IRealm
    {
        //Room Collection
        public Dictionary<string, Zone> Zones{ get; protected set; }

        public Realm(string name)
            : base(name)
        {
            Zones = new Dictionary<string, Zone>();
        }

        public virtual void AddZone(Zone zone, bool forceOverwrite = true)
        {
            //Check if room is null.
            if (zone== null)
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

        public virtual void AddZones(Zone[] zones, bool forceOverwrite = true)
        {
            foreach (Zone zone in zones)
            {
                AddZone(zone, forceOverwrite);
            }
        }

        public virtual void RemoveZone(Zone zone)
        {
            if (Zones.ContainsKey(zone.Name))
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

        #region == Events ==
        public delegate void OnEnterHandler(IPlayer player, AvailableTravelDirections enteredDirection);
        public event OnEnterHandler OnEnterEvent;
        public virtual void OnEnter(IPlayer player, AvailableTravelDirections enteredDirection)
        {
            BroadcastMessage(player.Name + " has entered from the " + enteredDirection.ToString());
        }
        #endregion
    }
}
