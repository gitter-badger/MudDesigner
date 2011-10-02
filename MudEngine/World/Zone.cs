using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Core;

namespace MudEngine.World
{
    public class Zone : BaseEnvironment
    {
        /// <summary>
        /// Gets a reference to the Room collection for this zone
        /// </summary>
        public List<Room> Rooms { get; private set; }

        public Zone(BaseGame game)
            : base(game)
        {
            Rooms = new List<Room>();
        }

        /// <summary>
        /// Installs a Room into this Zone, if the Room does not already exists.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool InstallRoom(Room room)
        {
            Room rm = Rooms.Find(delegate(Room r)
            {
                return r.ID == room.ID;
            }
            );

            if (rm != null)
                return false;

            Rooms.Add(room);
            return true;
        }

        /// <summary>
        /// Returns a reference to a room matching the supplied name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Room GetRoom(string name)
        {
            return Rooms.Find(r => r.Name == name);
        }

        /// <summary>
        /// Returns a reference to a room matching the supplied id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Room GetRoom(int id)
        {
            return Rooms.Find(r => r.ID == id);
        }

        /// <summary>
        /// Checks if the specified Room exists within this zone.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool RoomExists(Room room)
        {
            return Rooms.Exists(r => r.ID == room.ID);
        }

        /// <summary>
        /// Checks if the specified ID exists within this zone.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RoomExists(int id)
        {
            return Rooms.Exists(r => r.ID == id);
        }

        /// <summary>
        /// Replaces a room matching the oldID argument with the supplied newRoom.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool ReplaceRoom(Room newRoom, int oldID)
        {
            if (!RoomExists(oldID))
                return false;

            Room rm = Rooms.Find(delegate(Room r)
            {
                return r.ID == oldID;
            }
            );

            if (rm == null)
                return false;

            Rooms.Remove(rm);
            Rooms.Add(newRoom);

            return false;
        }
    }
}
