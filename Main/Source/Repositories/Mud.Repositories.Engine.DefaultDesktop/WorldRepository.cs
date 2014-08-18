using Microsoft.Practices.Unity;
using Mud.Engine.Core.Environment;
using Mud.Repositories.Shared;
using Mud.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Repositories.Engine.DefaultDesktop
{
    public class WorldRepository : IWorldRepository
    {
        private IWorldService service;

        public WorldRepository(IWorldService worldService)
        {
            this.service = worldService;
        }

        public async Task<IEnumerable<IWorld>> GetAllWorlds(bool includeAllChildrenObjects = false)
        {
            IEnumerable<IWorld> worlds = await this.service.GetAllWorlds();

            return worlds;
        }


        public Task<IWorld> GetWorldForRealm(bool includeAllChildrenObjects = false)
        {
            throw new NotImplementedException();
        }

        public Task<IWorld> GetWorldById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveWorld(IWorld world)
        {
            throw new NotImplementedException();
        }

        public Task SaveAllWorlds(IEnumerable<IWorld> worlds)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWorld(IWorld world)
        {
            throw new NotImplementedException();
        }
    }
}
