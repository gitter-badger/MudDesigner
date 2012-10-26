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
using Newtonsoft.Json;

namespace MudDesigner.Engine.Environment
{
    public abstract class BaseRealm : GameObject, IRealm
    {
        //Room Collection
        [Browsable(false),JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public Dictionary<Guid, IZone> Zones{ get; protected set; }

        public BaseRealm(string name) : base()
        {
            Zones = new Dictionary<Guid, IZone>();
            Name = name;
        }

        public BaseRealm(string name, Guid id) : base(id)
        {
            Zones = new Dictionary<Guid, IZone>();
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

            Zones.Add(zone.ID, zone);
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
            foreach (BaseZone zone in zones)
            {
                AddZone(zone, forceOverwrite);
            }
        }

        public virtual void RemoveZone(IZone zone)
        {
            if (Zones.ContainsKey(zone.ID))
                Zones.Remove(zone.ID);
            else if (Zones.ContainsValue(zone))
                Zones.Remove(zone.ID);
        }

        public virtual void BroadcastMessage(string message, List<IPlayer> playersToOmmit = null)
        {
            foreach (IZone zone in Zones.Values)
            {
                if (playersToOmmit == null)
                    zone.BroadcastMessage(message);
                else
                    zone.BroadcastMessage(message, playersToOmmit);
            }
        }

        public delegate void OnEnterHandler(IMob occupant, IEnvironment departureEnvironment, AvailableTravelDirections directions);
        public event OnEnterHandler OnEnterEvent;
        public virtual void OnEnter(IMob occupant, IEnvironment departureEnvironment, AvailableTravelDirections direction)
        {
        }

        public delegate void OnLeaveHandler(IMob occupant, IEnvironment arrivalEnvironment, AvailableTravelDirections directions);
        public event OnLeaveHandler OnLeaveEvent;
        public void OnLeave(IMob occupant, IEnvironment arrivalEnvironment, AvailableTravelDirections direction)
        {
        }

        public override void Save(BinaryWriter writer)
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
    }
}
