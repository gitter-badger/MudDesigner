using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Mobs;

namespace MudDesigner.Scripts.Default.Mobs.Appearances
{
    public class Head : IAppearanceAttribute
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

        public Head()
        {
            Name = "Head";
            Description = "The persons head.";
            Value = "Thin head with chubby cheeks.";
        }
    }
}
