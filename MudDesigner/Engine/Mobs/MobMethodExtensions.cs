using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Mobs
{
    public static class MobMethodExtensions
    {
        public static IGameObject ToGameObject(this IPlayer player)
        {
            return (IGameObject)player;
        }

        public static IGameObject ToGameObject(this IClass mobClass)
        {
            return (IGameObject)mobClass;
        }

        public static IGameObject ToGameObject(this INPC npc)
        {
            return (IGameObject)npc;
        }
    }
}
