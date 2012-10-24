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
    public abstract class Zone : IZone
    {
        /// <summary>
        /// Realm that this Room resides within
        /// </summary>
        [Browsable(false)]
        public IRealm Realm { get; protected set; }

        //Room Collection
        [Browsable(false)]
        public Dictionary<string, Room> Rooms{ get; protected set; }

        public string Name { get; set; }

        [Browsable(false)]
        public Guid Id
        {
            get { return Guid.NewGuid(); }
        }

        public Zone(string name, IRealm realm)
        {
            Rooms = new Dictionary<string, Room>();
            Realm = realm;
            Name = name;
        }

        public virtual void AddRoom(Room room, bool forceOverwrite = true)
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

        public virtual void AddRooms(Room[] rooms, bool forceOverwrite = true)
        {
            foreach (Room room in rooms)
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

        public virtual void RemoveRoom(Room room)
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
                foreach (Room room in Rooms.Values)
                {
                    foreach (Player player in room.Occupants.Values)
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

        public string Description
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
    }
}
