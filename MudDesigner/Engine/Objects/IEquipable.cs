using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Objects
{
    public interface IEquipable : IItem
    {
        void Equip(IPlayer player);
        void Unequip(IPlayer player);

    }
}
