//-----------------------------------------------------------------------
// <copyright file="IZoneExtensions.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Shared.Environment
{
    using System.Linq;
    using Mud.Engine.Shared.Character;

    /// <summary>
    /// Provides method extensions for objects that implement the IZone interface
    /// </summary>
    public static class IZoneExtensions
    {
        /// <summary>
        /// Determines whether the specified zone has the given room.
        /// </summary>
        /// <param name="zone">The zone.</param>
        /// <param name="room">The room.</param>
        /// <returns>Returns true if the zone owns the given room or not.</returns>
        public static bool HasRoom(this IZone zone, IRoom room)
        {
            return zone.Rooms.Contains(room);
        }

        /// <summary>
        /// Determines whether the specified zone has the given character.
        /// </summary>
        /// <param name="zone">The zone.</param>
        /// <param name="character">The character.</param>
        /// <returns>Returns true if the zone has the given character within it or not.</returns>
        public static bool HasCharacter(this IZone zone, ICharacter character)
        {
            return zone.Rooms.Any(r => r.Occupants.Contains(character));
        }
    }
}
