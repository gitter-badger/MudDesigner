using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.Core.Interfaces
{
    /// <summary>
    /// Public API for classes that need to be updated constantly.
    /// </summary>
    public interface IUpdatable
    {
        void Update();
    }
}
