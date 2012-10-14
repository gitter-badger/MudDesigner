using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Environment
{
    public abstract class Zone : IGameObject, IZone
    {
        /// <summary>
        /// Realm that this Room resides within
        /// </summary>
        public IRealm Realm { get; protected set; }

        //Room Collection
        public Dictionary<string, Room> Rooms{ get; protected set; }

        public string Name { get; set; }

        public Zone(string name, IRealm realm)
        {
            Rooms = new Dictionary<string, Room>();
            Realm = realm;
            Name = name;
        }

        public virtual void AddRoom(Room room, bool forceOverwrite = true)
        {
            //Check if room is null.
            if (room== null)
                return; //No null references within our collections!

            //If this Room already exists, overwrite it
            //but only if 'forceOverwrite' is true
            if (Rooms.ContainsKey(room.Name))
            {
                Rooms[room.Name] = room;
            }
                //Room does not exist, so lets add it.
            else
            {
                Rooms.Add(room.Name, room);
            }
        }

        public virtual void AddRooms(Room[] rooms, bool forceOverwrite = true)
        {
            foreach (Room room in rooms)
            {
                AddRoom(room, forceOverwrite);
            }
        }

        public virtual void RemoveRoom(Room room)
        {
            if (Rooms.ContainsKey(room.Name))
                Rooms.Remove(room.Name);
        }

        public virtual void BroadcastMessage(string message, List<IPlayer> playersToOmmit = null)
        {
                foreach (Room room in Rooms.Values)
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

        public Guid Id
        {
            get { return Guid.NewGuid(); }
        }

        public GameObjectType Type
        {
            get { return GameObjectType.Realm; }
        }
    }
}
