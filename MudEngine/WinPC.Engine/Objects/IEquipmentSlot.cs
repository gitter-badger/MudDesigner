using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Actions;

namespace MudDesigner.Engine.Objects
{
    public interface IEquipmentSlot
    {
        string Name { get; set; }
        IUseable Equipment { get; }

        void Equip(IUseable equipment);
    }
}
