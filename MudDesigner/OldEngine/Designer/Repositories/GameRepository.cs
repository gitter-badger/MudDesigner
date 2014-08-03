using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.Factories;
using MudEngine.Engine.Core;
using MudEngine.Engine;

namespace Designer.Repositories
{
    /// <summary>
    /// Provides methods for mananging game objects.
    /// </summary>
    public class GameRepository
    {
        /*
        /// <summary>
        /// The storage container
        /// </summary>
        private IPersistedStorage storageContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRepository"/> class.
        /// </summary>
        /// <param name="storage">The storage.</param>
        public GameRepository(IPersistedStorage storage)
        {
            this.storageContainer = storage;
        }

        /// <summary>
        /// Gets the game specified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns the game loaded from disk if it exists.</returns>
        public IGame GetGame<T>() where T : class, IGame, new()
        {
            IGame game = new T();
            game.Name = GameProperties.Default.Name;

            // Get the file path for the game name saved in the properties.
            string filePath = this.storageContainer.GetStoragePath<T>((T)game);

            // If no game file exists with the saved name (could still be defaulted)
            // then we return a fresh instance.
            if (!File.Exists(filePath))
            {
                game = GameFactory.GetGame<T>();
                game.Name = GameProperties.Default.Name;
                game.Version = new Version(GameProperties.Default.Version);
                return game;
            }

            // If we found a file, we restore it and return it.
            return storageContainer.Load<T>((T)game);
        }*/
    }
}
