using Mud.Engine.Shared.Core;
using Mud.Engine.Shared.Environment;
using Mud.Engine.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Components.DefaultServices
{
    public class WorldService : IWorldService
    {
        public Task<IEnumerable<IWorld>> GetAllWorlds(bool hydrateWorlds = false, IDataStoreContext context = null)
        {
            return Task.FromResult<IEnumerable<IWorld>>(new List<IWorld>());
        }
    }
}
