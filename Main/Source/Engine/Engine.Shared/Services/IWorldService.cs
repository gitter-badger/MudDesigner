using Mud.Engine.Shared.Core;
using Mud.Engine.Shared.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Shared.Services
{
    public interface IWorldService
    {
        Task<IEnumerable<IWorld>> GetAllWorlds(bool hydrateWorlds = false, IDataStoreContext context = null);
    }
}
