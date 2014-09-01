using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment.Travel
{
    public interface ITravelDirection
    {
        string Direction { get; }

        ITravelDirection GetOppositeDirection();
    }
}
