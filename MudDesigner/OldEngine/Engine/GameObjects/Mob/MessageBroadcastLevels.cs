//-----------------------------------------------------------------------
// <copyright file="MessageBroadcastLevels.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MudEngine.Engine.GameObjects.Mob
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
