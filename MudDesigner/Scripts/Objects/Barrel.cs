using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Objects;

namespace MudDesigner.Scripts.Objects
{
    public class Barrel : BaseItem
    {
        public Barrel()
            : base()
        {
            Name = "Barrel";
            Description = "There is a barrel sitting in the corner.";
        }
    }
}
