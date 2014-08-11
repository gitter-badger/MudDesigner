using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mud.Engine.Core.Networking
{
    public interface IConnectionContext
    {
        object Context { get; }
    }
}
