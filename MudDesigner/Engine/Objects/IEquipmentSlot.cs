using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Actions;

namespace MudDesigner.Engine.Objects
{
    public interface IEquipmentSlot : IGameObject
    {
        IEquipable Equipment { get; }

        void Equip(IEquipable equipment);
        void Unequip(IEquipable equipment);
    }
}
