using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinPC.Engine.Abstract.Actions;

namespace WinPC.Engine.Abstract.Objects
{
    public interface IEquipmentSlot
    {
        string Name { get; set; }
        IUseable Equipment { get; }

        void Equip(IUseable equipment);
    }
}
