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
    public abstract class BaseZone : GameObject, IZone
    {
        /// <summary>
        /// Realm that this Room resides within
        /// </summary>
        [Browsable(false)]
        public IRealm Realm { get; protected set; }

        //Room Collection
        [Browsable(false)]
        public Dictionary<string, BaseRoom> Rooms{ get; protected set; }

        public BaseZone(string name, IRealm realm)
        {
            Rooms = new Dictionary<string, BaseRoom>();
            Realm = realm;
            Name = name;
        }

        public virtual void AddRoom(BaseRoom room, bool forceOverwrite = true)
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

            if (!Rooms.Values.Contains<IRoom>(room))
                Rooms.Add(room.Name, room);
        }

        public virtual void AddRooms(BaseRoom[] rooms, bool forceOverwrite = true)
        {
            foreach (BaseRoom room in rooms)
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

        public virtual void RemoveRoom(BaseRoom room)
        {
            if (Rooms.ContainsKey(room.Name))
                Rooms.Remove(room.Name);
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
                foreach (BaseRoom room in Rooms.Values)
                {
                    foreach (BasePlayer player in room.Occupants.Values)
                    {
                        if (playersToOmmit != null)
                        {
                            if (playersToOmmit.Contains((IPlayer)player))
                                continue; //Skip this player if it's in the list.
                        }
                        //Send the message
                        player.SendMessage(message);
                    }
                }
        }

        public override string ToString()
        {
            return Realm.Name + "->" + Name;
        }

        #region == Events ==
        public delegate void OnEnterHandler(IPlayer player, AvailableTravelDirections enteredDirection);
        public event OnEnterHandler OnEnterEvent;
        public virtual void OnEnter(IPlayer player, AvailableTravelDirections enteredDirection)
        {
            BroadcastMessage(player.Name + " has entered from the " + enteredDirection.ToString());
        }
        #endregion


        public bool Safe
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool Enabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        Dictionary<string, IRoom> IZone.Rooms
        {
            get { throw new NotImplementedException(); }
        }

        public void AddRoom(IRoom room, bool forceOverwrite)
        {
            throw new NotImplementedException();
        }

        public void AddRooms(IRoom[] rooms, bool forceOverwrite)
        {
            throw new NotImplementedException();
        }

        public void RemoveRoom(IRoom room)
        {
            throw new NotImplementedException();
        }

        public void RemoveRoom(Guid id)
        {
            throw new NotImplementedException();
        }

        public void BroadcastMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
