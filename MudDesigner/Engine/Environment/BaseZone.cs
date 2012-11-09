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
        [Browsable(false), JsonProperty(ReferenceLoopHandling = ReferenceLoopHandling.Serialize)]
        public IRealm Realm { get; set; }

        //Room Collection
        [Browsable(false), JsonProperty(TypeNameHandling = TypeNameHandling.All, ReferenceLoopHandling = ReferenceLoopHandling.Serialize)]
        public List<IRoom> Rooms { get; protected set; }

        public bool IsAdminOnly { get; set; }

        public bool Safe { get; set; }

        public BaseZone()
        {
            Rooms = new List<IRoom>();

            Enabled = true;

        }

        public BaseZone(string name)
        {
            Rooms = new List<IRoom>();
            Name = name;

            Enabled = true;
        }

        public BaseZone(string name, IRealm realm)
        {
            Rooms = new List<IRoom>();

            if (realm != null)
            {
                Realm = realm;
            }

            Name = name;

            Enabled = true;
        }

        public virtual void AddRoom(IRoom room, bool forceOverwrite = true)
        {
            if (room == null)
                return;

            if (forceOverwrite)
            {
                if (Rooms.Contains(room))
                {
                    Rooms.Remove(room);
                }
            }

            room.Zone = this;
            Rooms.Add(room);
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
            foreach (IRoom room in Rooms)
            {
                if (room.Name == roomName)
                    return room;
            }

            return null;
        }

        public virtual void RemoveRoom(IRoom room)
        {
            if (Rooms.Contains(room))
                Rooms.Remove(room);
        }

        public virtual void DeleteRooms()
        {
            foreach (IRoom room in Rooms)
            {
                room.Destroy();
            }
        }

        public virtual void BroadcastMessage(string message, List<IPlayer> playersToOmmit = null)
        {
            foreach (IRoom room in Rooms)
            {
                if (playersToOmmit == null)
                    room.BroadcastMessage(message);
                else
                    room.BroadcastMessage(message, playersToOmmit);
            }
        }

        public override string ToString()
        {
            return Realm.Name + ">" + Name;
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
