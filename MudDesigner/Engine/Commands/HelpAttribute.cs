using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Commands
{
    public class HelpAttribute : Attribute
    {
        public string Description { get; set; }

        public HelpAttribute(string description)
        {
            Description = description;
        }
    }
}
