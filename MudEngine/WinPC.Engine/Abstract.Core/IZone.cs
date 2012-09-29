using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Abstract.Core;
using MudDesigner.Engine.Abstract.Actions;

namespace MudDesigner.Engine.Abstract.Core
{
    interface IZone : ILoadable, ISaveable, IUpdatable
    {
    }
}
