using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Mobs;

namespace MudDesigner.Scripts.Default.Mobs.Appearances
{
    public class Hair : IAppearanceAttribute
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

        public Hair()
        {
            Name = "Hair";
            Description = "The hair on someones head.";
            Value = "Hair is combed neatly.";
        }

        public Hair(string description, string value)
        {
            Description = description;
            Name = "Hair";
            Value = value;
        }
    }
}
