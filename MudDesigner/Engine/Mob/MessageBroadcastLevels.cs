//-----------------------------------------------------------------------
// <copyright file="MessageBroadcastLevels.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MudEngine.Engine.Mob
{
    /// <summary>
    /// Determines how far a message can be broadcasted by a mob object.
    /// </summary>
    public enum MessageBroadcastLevels
    {
        /// <summary>
        /// The realm
        /// </summary>
        Realm,

        /// <summary>
        /// The zone
        /// </summary>
        Zone,

        /// <summary>
        /// The room
        /// </summary>
        Room
    }
}
