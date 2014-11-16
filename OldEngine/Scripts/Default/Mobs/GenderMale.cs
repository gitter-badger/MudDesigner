using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Mobs;

namespace MudDesigner.Scripts.Default.Mobs
{
    public class GenderMale : IGender
    {
        public string Name { get; set; }

        public GenderMale()
        {
            Name = "Male";
        }
    }
}
