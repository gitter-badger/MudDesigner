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
        [Browsable(false), JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public List<IZone> Zones { get; protected set; }

        public bool IsAdminOnly { get; set; }

        public BaseRealm()
        {
            Zones = new List<IZone>();
        }

        public BaseRealm(string name)
            : base()
        {
            Zones = new List<IZone>();
            Name = name;
        }

        public virtual void AddZone(IZone zone, bool forceOverwrite = true)
        {
            if (zone == null)
                return;

            if (forceOverwrite)
            {
                if (Zones.Contains(zone))
                {
                    Zones.Remove(zone);
                }
            }

            zone.Realm = this;
            Zones.Add(zone);
        }

        public virtual IZone GetZone(string zoneName)
        {
            foreach (IZone zone in Zones)
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
            if (Zones.Contains(zone))
                Zones.Remove(zone);
        }

        public virtual void BroadcastMessage(string message, List<IPlayer> playersToOmmit = null)
        {
            foreach (IZone zone in Zones)
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

        public override string ToString()
        {
            return Name;
        }
    }
}
