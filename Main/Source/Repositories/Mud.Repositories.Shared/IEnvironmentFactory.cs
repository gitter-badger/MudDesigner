using Mud.Engine.Core.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mud.Repositories.Shared
{
    public interface IEnvironmentFactory : IRepository
    {
        IWorldRepository CreateWorldRepository();
    }
}
