using System;

using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Objects
{
    public interface IItem : IGameObject
    {
         //TODO - Add restriction lists.
        Guid ObjectBound { get; set; }

        int Weight { get; set; }
        int Health { get; set; }
        bool Indestructible { get; set; }

        void Inspect(IPlayer player);

        void OnInspect(IPlayer player);
        void OnDamage(int amount);
        void OnRepair(int amount);
    }
}