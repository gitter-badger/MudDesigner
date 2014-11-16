//-----------------------------------------------------------------------
// <copyright file="MobMethodExtensions.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Mobs
{
    /// <summary>
    /// Method extensions used by IMob based objects.
    /// </summary>
    public static class MobMethodExtensions
    {
        /// <summary>
        /// Convert an IPlayer to IGameObject
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public static IGameObject ToGameObject(this IPlayer player)
        {
            return (IGameObject)player;
        }

        /// <summary>
        /// Convert IClass to IGameObject
        /// </summary>
        /// <param name="mobClass">The mob class.</param>
        /// <returns></returns>
        public static IGameObject ToGameObject(this IClass mobClass)
        {
            return (IGameObject)mobClass;
        }

        /// <summary>
        /// Convert INPC to IGameObject
        /// </summary>
        /// <param name="npc">The NPC.</param>
        /// <returns></returns>
        public static IGameObject ToGameObject(this INPC npc)
        {
            return (IGameObject)npc;
        }
    }
}
