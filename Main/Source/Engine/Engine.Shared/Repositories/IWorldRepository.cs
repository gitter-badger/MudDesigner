using Mud.Engine.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Shared.Repositories
{
    public interface IWorldRepository
    {
        Task<IEnumerable<IWorldRepository>> GetWorlds(IDataStoreContext context = null);
    }
}
