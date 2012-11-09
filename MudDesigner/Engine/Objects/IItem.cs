using System;

using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Objects
{
    public interface IItem : IGameObject
    {
         //TODO - Add restriction lists.
        Guid ObjectBound { get; set; }

        bool IsAdminOnly { get; set; }

        int Weight { get; set; }
        int Health { get; set; }
        bool Indestructible { get; set; }

        void Inspect(IPlayer player);
        void Repair(IMob healder, int amount);
        void Damage(IMob dealer, int amount);
    }
}