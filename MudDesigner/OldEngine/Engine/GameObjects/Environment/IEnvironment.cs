using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Engine.GameObjects.Environment
{
    public interface IEnvironment
    {
        int Id { get; set; }

        string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the world is safe.
        /// </summary>
        bool IsSafe { get; set; }

        /// <summary>
        /// Gets the occupants of the world.
        /// </summary>
        List<IMob> Occupants { get; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Updates this instance of the World.
        /// </summary>
        void Update();
    }
}
