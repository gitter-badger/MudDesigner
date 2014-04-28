//-----------------------------------------------------------------------
// <copyright file="CoreFactoryTests.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MudEngine.Engine.Core;
using MudEngine.Engine.Factories;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Tests.Win.Engine.Factories
{
    /// <summary>
    /// Unit Tests for all of the Factory methods associated with Core objects.
    /// </summary>
    [TestClass]
    public class CoreFactoryTests
    {
        #region GameFactory

        /// <summary>
        /// Tests Fetching all IGame objects from the loaded assemblies.
        /// </summary>
        [TestMethod]
        public void GetGames()
        {
            // Arrange
            var games = new List<IGame>();
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            // Act
            games = GameFactory.GetGames(mudEngineAssembly);

            // Assert
            Assert.IsTrue(games.Count == 1); // Should pick up CoreGame.
        }

        /// <summary>
        /// Tests fetching the CoreGame object.
        /// </summary>
        [TestMethod]
        public void GetCoreGame()
        {
            // Arrange
            IGame coreGame = null;
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            // Act
            coreGame = GameFactory.GetGame<EngineGame>(mudEngineAssembly);

            // Assert
            Assert.IsNotNull(coreGame);
        }

        /// <summary>
        /// Tests fetching the default game object.
        /// </summary>
        [TestMethod]
        public void GetDefaultGame()
        {
            // Arrange
            IGame defaultGame = null;
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();
            GameFactory.DefaultGame = new EngineGame();

            // Act
            defaultGame = GameFactory.GetDefaultGame(mudEngineAssembly);

            // Assert
            Assert.IsNotNull(defaultGame);
        }

        #endregion

        #region ServerFactory
        /// <summary>
        /// Tests Fetching all IServer objects from the loaded assemblies.
        /// </summary>
        [TestMethod]
        public void GetServers()
        {
            // Arrange
            var servers = new List<IServer>();
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            // Act
            servers = ServerFactory.GetServers(mudEngineAssembly);

            // Assert
            Assert.IsTrue(servers.Count == 1); // Should pick up CoreServer.
        }

        /// <summary>
        /// Tests fetching the CoreServer object.
        /// </summary>
        [TestMethod]
        public void GetCoreServer()
        {
            // Arrange
            IServer coreServer = null;
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            // Act
            coreServer = ServerFactory.GetServer<EngineServer>(mudEngineAssembly);

            // Assert
            Assert.IsNotNull(coreServer);
        }

        /// <summary>
        /// Tests fetching the default game object.
        /// </summary>
        [TestMethod]
        public void GetDefaultServer()
        {
            // Arrange
            IServer defaultServer = null;
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();
            ServerFactory.DefaultServer = new EngineServer();

            // Act
            defaultServer = ServerFactory.GetDefaultServer(mudEngineAssembly);

            // Assert
            Assert.IsNotNull(defaultServer);
        }

        #endregion

        #region StorageFactory

        /// <summary>
        /// Tests Fetching all IPersistedStorage objects from the loaded assemblies.
        /// </summary>
        [TestMethod]
        public void GetStorageCollection()
        {
            // Arrange
            var storage = new List<IPersistedStorage>();
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            // Act
            storage = PersistedStorageFactory.GetStorageContainers(mudEngineAssembly);

            // Assert
            Assert.IsTrue(storage.Count == 1); // Should pick up CoreServer.
        }

        /// <summary>
        /// Tests fetching the CoreXmlStorage object.
        /// </summary>
        [TestMethod]
        public void GetGameCoreXmlStorage()
        {
            // Arrange
            IPersistedStorage xmlStorage = null;
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            // Act
            xmlStorage = PersistedStorageFactory.GetStorageContainer<EngineXmlStorage>(mudEngineAssembly);

            // Assert
            Assert.IsNotNull(xmlStorage);
        }

        /// <summary>
        /// Tests fetching the default storage object.
        /// </summary>
        [TestMethod]
        public void GetDefaultStorage()
        {
            // Arrange
            IPersistedStorage defaultStorage = null;
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();
            PersistedStorageFactory.DefaultStorage = new EngineXmlStorage();

            // Act
            defaultStorage = PersistedStorageFactory.GetDefaultStorage(mudEngineAssembly);

            // Assert
            Assert.IsNotNull(defaultStorage);
        }

        #endregion

        #region MobFactory

        /// <summary>
        /// Tests Fetching all IMob objects from the loaded assemblies.
        /// </summary>
        [TestMethod]
        public void GetMobs()
        {
            // Arrange
            var mob = new List<IMob>();
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            // Act
            mob = MobFactory.GetMobs(mudEngineAssembly);

            // Assert
            Assert.IsTrue(mob.Count == 2); // EngineMob and EnginePlayer
        }

        /// <summary>
        /// Tests fetching the mob object.
        /// </summary>
        [TestMethod]
        public void GetEngineMob()
        {
            // Arrange
            IMob mob = null;
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            // Act
            mob = MobFactory.GetMob<EngineMob>(mudEngineAssembly);

            // Assert
            Assert.IsNotNull(mob);
        }

        /// <summary>
        /// Tests fetching the default mob object.
        /// </summary>
        [TestMethod]
        public void GetDefaultMob()
        {
            // Arrange
            IMob defaultMob = null;
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            MobFactory.DefaultMob = new EngineMob();

            // Act
            defaultMob = MobFactory.GetDefaultMob(mudEngineAssembly);

            // Assert
            Assert.IsNotNull(defaultMob);
        }

        /// <summary>
        /// Tests Fetching all IPlayer objects from the loaded assemblies.
        /// </summary>
        [TestMethod]
        public void GetPlayers()
        {
            // Arrange
            var players = new List<IPlayer>();
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            // Act
            players = MobFactory.GetPlayers(mudEngineAssembly);

            // Assert
            Assert.IsTrue(players.Count == 1);
        }

        /// <summary>
        /// Tests fetching the player object.
        /// </summary>
        [TestMethod]
        public void GetEnginePlayer()
        {
            // Arrange
            IPlayer player = null;
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            // Act
            player = MobFactory.GetPlayer<EnginePlayer>(mudEngineAssembly);

            // Assert
            Assert.IsNotNull(player);
        }

        /// <summary>
        /// Tests fetching the default player object.
        /// </summary>
        [TestMethod]
        public void GetDefaultPlayer()
        {
            // Arrange
            IPlayer defaultPlayer = null;
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            MobFactory.DefaultPlayer = new EnginePlayer();

            // Act
            defaultPlayer = MobFactory.GetDefaultPlayer(mudEngineAssembly);

            // Assert
            Assert.IsNotNull(defaultPlayer);
        }

        #endregion
    }
}
