using Mud.Engine.Core.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment
{
    public static class IZoneExtensions
    {
        /// <summary>
        /// Determines whether the specified zone has the given room.
        /// </summary>
        /// <param name="zone">The zone.</param>
        /// <param name="room">The room.</param>
        /// <returns></returns>
        public static bool HasRoom(this IZone zone, IRoom room)
        {
            return zone.Rooms.Contains(room);
        }

        /// <summary>
        /// Determines whether the specified zone has the given character.
        /// </summary>
        /// <param name="zone">The zone.</param>
        /// <param name="character">The character.</param>
        /// <returns></returns>
        public static bool HasCharacter(this IZone zone, ICharacter character)
        {
            return zone.Rooms.Any(r => r.Occupants.Contains(character));
        }
    }
}
