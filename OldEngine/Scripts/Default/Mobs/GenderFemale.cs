using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Mobs;

namespace MudDesigner.Scripts.Default.Mobs
{
    public class GenderFemale : IGender
    {
        public string Name { get; set; }

        public GenderFemale()
        {
            Name = "Female";
        }
    }
}
