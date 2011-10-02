using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.Core
{
    public interface IObject
    {
        string Name { get; set; }

        string Filename { get; set; }

        string Description { get; set; }
    }
}
