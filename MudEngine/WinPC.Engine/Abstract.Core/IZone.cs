using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WinPC.Engine.Abstract.Core;
using WinPC.Engine.Abstract.Actions;

namespace WinPC.Engine.Abstract.Core
{
    interface IZone : ILoadable, ISaveable, IUpdatable
    {
    }
}
