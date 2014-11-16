using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Mobs;

namespace MudDesigner.Scripts.Default.Mobs.Stats
{
    public class Mana : IStat
    {
        public string Name { get { return "Mana"; } }

        public int Amount { get; set; }
    }
}
