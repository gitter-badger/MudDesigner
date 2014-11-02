using Mud.Engine.Core.Engine;
using Mud.Engine.Core.Environment;
using Mud.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Services.FlatFile
{
    public class WorldService : IWorldService
    {
        public Task<IEnumerable<IWorld>> GetAllWorlds(IDataStoreContext context = null)
        {
            throw new NotImplementedException();
        }

        public Task<IWorld> GetWorldForRealm(IRealm realm, IDataStoreContext context = null)
        {
            throw new NotImplementedException();
        }

        public Task<IWorld> GetWorldById(int id, IDataStoreContext context = null)
        {
            throw new NotImplementedException();
        }

        public Task SaveWorld(IWorld world, IDataStoreContext context = null)
        {
            throw new NotImplementedException();
        }

        public Task SaveAllWorlds(IEnumerable<IWorld> worlds, IDataStoreContext context = null)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWorld(IWorld world, IDataStoreContext context = null)
        {
            throw new NotImplementedException();
        }
    }
}
