using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.Commands;

namespace MudEngine.Engine.GameObjects.Mob.Role
{
    /// <summary>
    /// Used for implementing a role
    /// </summary>
    public interface IRole
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the commands.
        /// </summary>
        IEnumerable<ICommand> Commands { get; }

        /// <summary>
        /// Gets the level that this Role has in comparison to other Roles.
        /// </summary>
        int PriorityLevel { get; }
    }
}
