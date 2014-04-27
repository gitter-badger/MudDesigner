using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mud.Models;
using Mud.Models.Mobs;

namespace Mud.Models.Environment
{
    public interface IRoom
    {
        List<IMob> Characters { get; }
        List<IPlayer> Players { get; }
    }
}
