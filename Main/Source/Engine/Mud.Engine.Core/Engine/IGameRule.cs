using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Engine
{
    public interface IGameRule
    {
        string Name { get; }

        bool Enabled { get; set; }

        void Execute();
    }
}
