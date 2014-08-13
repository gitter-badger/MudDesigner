using Mud.Engine.Core.Engine;
using Mud.Engine.Core.Environment;
using Mud.Repositories.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mud.Engine.Core.Engine.ValidationRules;
using System.Threading.Tasks;

namespace Mud.Engine.Default.Desktop.Engine
{
    /// <summary>
    /// The default implementation of the IGame interface for the Mud Engine.
    /// </summary>
    public class DefaultMultiplayerGame : DefaultGame
    {
        [ValidateValueIsNotNull(FailureMessage = "Name can not be left blank.", ValidationMessageType = typeof(ErrorMessage))]
        public new string Name { get; set; }

        /// <summary>
        /// The environment factory
        /// </summary>
        private readonly IEnvironmentFactory environmentFactory;

        public DefaultMultiplayerGame(IEnvironmentFactory environmentFactory, ILogger logger)
            : base()
        {
            this.environmentFactory = environmentFactory;
            this.Logger = logger;
        }

        /// <summary>
        /// The initialize method is responsible for restoring the world and state.
        /// </summary>
        public async override Task Initialize()
        {
            this.ValidateProperty("Name");

            // Restore our previously saved worlds.
            IWorldRepository worldRepository = this.environmentFactory.CreateWorldRepository();
            IEnumerable<IWorld> result = await worldRepository.GetWorlds();

            foreach (IWorld world in result)
            {
                world.Initialize();
            }

            this.Worlds = new ObservableCollection<IWorld>(result);

            this.IsRunning = this.Worlds != null && this.Worlds.Any();
        }
    }
}
