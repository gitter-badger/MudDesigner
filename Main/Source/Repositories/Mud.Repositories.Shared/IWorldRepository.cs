using Mud.Engine.Core.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Repositories.Shared
{
    public interface IWorldRepository
    {
        Task<IEnumerable<IWorld>> GetWorlds();
    }
}
