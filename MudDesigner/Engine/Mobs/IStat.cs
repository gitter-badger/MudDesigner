using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Mobs
{
    public interface IStat
    {
        string Name { get; set; }
        int Amount { get; set; }
    }
}
