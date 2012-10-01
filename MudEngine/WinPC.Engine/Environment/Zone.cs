using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Scripting;

namespace MudDesigner.Engine.Environment
{
    public abstract class Zone : BaseGameObject, IGameObject, IZone
    {
        /// <summary>
        /// Realm that this Room resides within
        /// </summary>
        public IRealm Realm { get; set; }

        //Room Collection
        public Dictionary<string, Room> Rooms{ get; set; }
        
        public Zone(string name, IRealm realm) : base(name)
        {
            Rooms = new Dictionary<string, Room>();
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

        public virtual void RemoveDoorway(Room room)
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
