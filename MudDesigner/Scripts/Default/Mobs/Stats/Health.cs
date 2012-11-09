using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Mobs;

namespace MudDesigner.Scripts.Default.Mobs.Stats
{
    public class Health : IStat
    {
        public string Name { get; set; }
        public int Amount { get; set; }

        public Health()
        {
            Name = "Health";
        }
    }
}
