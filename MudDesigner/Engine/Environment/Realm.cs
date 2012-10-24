using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Environment
{
    public abstract class Realm : IRealm
    {
        //Room Collection
        [Browsable(false)]
        public Dictionary<string, IZone> Zones{ get; protected set; }

        [Browsable(false)]
        public Guid Id { get; set; }

        public String Name { get; set; }

        public Realm(string name)
        {
            Id = new Guid();
            Zones = new Dictionary<string, IZone>();
            Name = name;
        }

        public Realm(string name, Guid id)
        {
            Id = id;
            Zones = new Dictionary<string, IZone>();
            Name = name;
        }

        public virtual void AddZone(IZone zone, bool forceOverwrite = true)
        {
            if (zone == null)
                return;

            if (forceOverwrite)
            {
                if (Zones.ContainsValue(zone))
                {
                    foreach (var r in Zones.Where(newZone => newZone.Value == zone))
                    {
                        Zones.Remove(r.Key);
                        break;
                    }
                }
            }

            if (!Zones.Values.Contains<IZone>(zone))
                Zones.Add(zone.Name, zone);
        }

        public virtual IZone GetZone(string zoneName)
        {
            foreach (IZone zone in Zones.Values)
            {
                if (zone.Name == zoneName)
                    return zone;
            }

            return null;
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

        public void Save(BinaryWriter writer)
        {
            var properties = this.GetType().GetProperties();

            foreach (var p in properties)
            {
                var value = p.GetValue(this, null);

                if (p.GetType() is Int32)
                {
                    writer.Write((int)value);
                }
                else if(p.GetType() is Double)
                {
                    writer.Write((double)value);
                }
                else if(p.GetType() is String)
                {
                    writer.Write((string)value);

                }
                // we are purposefully not including the dictionary
            }


        }

     
        public void Load(IGame game, BinaryReader reader)
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
    }
}
