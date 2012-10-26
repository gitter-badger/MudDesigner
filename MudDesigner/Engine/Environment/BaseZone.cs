using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Mobs;
using Newtonsoft.Json;

namespace MudDesigner.Engine.Environment
{
    public abstract class BaseZone : GameObject, IZone
    {
        /// <summary>
        /// Realm that this Room resides within
        /// </summary>
        [Browsable(false),JsonProperty(IsReference = true)]
        public IRealm Realm { get; protected set; }

        //Room Collection
        [Browsable(false), JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public Dictionary<Guid, IRoom> Rooms{ get; protected set; }

        public bool Safe { get; set; }

        public BaseZone(string name, IRealm realm) : base()
        {
            Rooms = new Dictionary<Guid, IRoom>();
            Realm = realm;
            Name = name;

            Enabled = true;
        }

        public BaseZone(string name, Guid id, IRealm realm) : base(id)
        {
            Rooms = new Dictionary<Guid, IRoom>();
            Realm = realm;
            Name = name;

            Enabled = true;
        }

        public virtual void AddRoom(IRoom room, bool forceOverwrite = true)
        {
            if (room == null)
                return;

            if (forceOverwrite)
            {
                if (Rooms.ContainsValue(room))
                {
                    foreach (var r in Rooms.Where(newRoom => newRoom.Value == room))
                    {
                        Rooms.Remove(r.Key);
                        break;
                    }
                }
            }

            Rooms.Add(room.ID, room);
        }

        public virtual void AddRooms(IRoom[] rooms, bool forceOverwrite = true)
        {
            foreach (IRoom room in rooms)
            {
                AddRoom(room, forceOverwrite);
            }
        }

        public virtual IRoom GetRoom(string roomName)
        {
            foreach (IRoom room in Rooms.Values)
            {
                if (room.Name == roomName)
                    return room;
            }

            return null;
        }

        public virtual void RemoveRoom(IRoom room)
        {
            if (Rooms.ContainsValue(room))
                Rooms.Remove(room.ID);
        }

        public virtual void RemoveRoom(Guid id)
        {
            if (Rooms.ContainsKey(id))
                Rooms.Remove(id);
        }

        public virtual void DeleteRooms()
        {
            foreach (IRoom room in Rooms.Values)
            {
                room.Destroy();
            }
        }

        public virtual void BroadcastMessage(string message, List<IPlayer> playersToOmmit = null)
        {
            foreach (IRoom room in Rooms.Values)
            {
                if (playersToOmmit == null)
                    room.BroadcastMessage(message);
                else
                    room.BroadcastMessage(message, playersToOmmit);
            }
        }

        public override string ToString()
        {
            return Realm.Name + "->" + Name;
        }

        public delegate void OnEnterHandler(IMob occupant, IEnvironment departureEnvironment, AvailableTravelDirections direction);
        public event OnEnterHandler OnEnterEvent;
        public void OnEnter(IMob occupant, IEnvironment departureEnvironment, AvailableTravelDirections direction)
        {
        }

        public delegate void OnLeavehandler(IMob occupant, IEnvironment arrivalEnvironment, AvailableTravelDirections direction);
        public event OnLeavehandler OnLeaveEvent;
        public void OnLeave(IMob occupant, IEnvironment arrivalEnvironment, AvailableTravelDirections direction)
        {
        }
    }
}
