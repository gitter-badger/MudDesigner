using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MudEngine.Engine.Core;
using MudEngine.Engine.Factories;
using MudEngine.Engine.GameObjects.Mob;

namespace MudEngine.Tests.Win.Engine.Core
{
    /// <summary>
    /// Unit tests for the EngineXmlStorage class.
    /// </summary>
    [TestClass]
    public class PersistedStorageTests
    {
        /// <summary>
        /// Initializes the engine XML storage.
        /// </summary>
        [TestMethod]
        public void InitializeEngineXmlStorage()
        {
            // Arrange  
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();
            IPersistedStorage storage = PersistedStorageFactory.GetStorageContainer<EngineXmlStorage>(mudEngineAssembly);

            // Act
            try
            {
                storage.InitializeStorage();
            }
            catch(Exception e)
            {
                Assert.Fail(string.Format("Failed to initialize EngineXmlStorage.\n{0}", e.Message));
            }

            // Assert
            Assert.IsNotNull(storage);
        }

        /// <summary>
        /// Saves a single object using the engines XML storage.
        /// </summary>
        [TestMethod]
        public void SaveEngineGame()
        {
            // Arrange  
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();
            
            // Instance a EngineGame object to test saving to disk.
            EngineGame game = (EngineGame)GameFactory.GetGame<EngineGame>(mudEngineAssembly);
            game.Name = "EngineGame Unit Test";
            IPersistedStorage storage = PersistedStorageFactory.GetStorageContainer<EngineXmlStorage>(mudEngineAssembly);
            storage.InitializeStorage();

            string filePath = Path.Combine(storage.RootPath, game.GetType().Name);

            // Act
            try
            {
                game = storage.Save<EngineGame>(game);
            }
            catch(Exception e)
            {
                Assert.Fail(string.Format("Failed to initialize EngineXmlStorage.\n{0}", e.Message));
            }

            // Assert
            Assert.IsTrue(File.Exists(storage.GetStoragePath<EngineGame>(game)));

            // Cleanup
            Directory.Delete(storage.GetStoragePath<EngineGame>(), true);
        }

        /// <summary>
        /// Saves collection of objects using the engines XML storage.
        /// </summary>
        [TestMethod]
        public void SaveEnginePlayers()
        {
            // Arrange  
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            // Instance a EngineGame object to test saving to disk.
            IPersistedStorage storage = PersistedStorageFactory.GetStorageContainer<EngineXmlStorage>(mudEngineAssembly);
            storage.InitializeStorage();

            // Create 20 instances.
            var players = new List<EnginePlayer>();
            for (int count = 0; count < 20; count++)
            {
                EnginePlayer player = (EnginePlayer)MobFactory.GetPlayer<EnginePlayer>(mudEngineAssembly);
                player.Name = "Player" + count;
                players.Add(player);
            }

            // Act
            try
            {
                storage.Save<EnginePlayer>(players.ToArray());
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Failed to initialize EngineXmlStorage.\n{0}", e.Message));
            }

            // Assert
            Assert.IsTrue(Directory.GetFiles(storage.GetStoragePath<EnginePlayer>()).Length == 20);

            // Cleanup
            Directory.Delete(storage.GetStoragePath<EnginePlayer>(), true);
        }

        /// <summary>
        /// Loads a single object using the engines XML storage.
        /// </summary>
        [TestMethod]
        public void LoadEngineGame()
        {
            // Arrange  
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            // Instance a EngineGame object to test saving to disk.
            EngineGame game = (EngineGame)GameFactory.GetGame<EngineGame>(mudEngineAssembly);
            EngineGame game2 = (EngineGame)GameFactory.GetGame<EngineGame>(mudEngineAssembly);
            game.Name = "TestGame";

            // Instance storage for testing.
            IPersistedStorage storage = PersistedStorageFactory.GetStorageContainer<EngineXmlStorage>(mudEngineAssembly);
            storage.InitializeStorage();

            string filePath = Path.Combine(storage.RootPath, game.GetType().Name);

            // Act
            try
            {
                storage.Save<EngineGame>(game);
                game2 = storage.Load<EngineGame>(game);
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Failed to initialize EngineXmlStorage.\n{0}", e.Message));
            }

            // Assert
            Assert.IsNotNull(game2);
            Assert.IsTrue(game2.Name == game.Name);

            // Clean up
            File.Delete(storage.GetStoragePath<EngineGame>(game));
        }

        /// <summary>
        /// Loads collection of objects using the engines XML storage.
        /// </summary>
        [TestMethod]
        public void LoadEnginePlayers()
        {
            // Arrange  
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            // Instance a EngineGame object to test saving to disk.
            IPersistedStorage storage = PersistedStorageFactory.GetStorageContainer<EngineXmlStorage>(mudEngineAssembly);
            storage.InitializeStorage();

            // Create 20 instances.
            var players = new List<EnginePlayer>();
            for (int count = 0; count < 20; count++)
            {
                EnginePlayer player = (EnginePlayer)MobFactory.GetPlayer<EnginePlayer>(mudEngineAssembly);
                player.Name = "Player" + count;
                players.Add(player);
            }

            // Act
            try
            {
                // Save players
                storage.Save<EnginePlayer>(players.ToArray());

                // Load all players.
                players.Clear();
                players = storage.Load<EnginePlayer>().ToList();
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Failed to initialize EngineXmlStorage.\n{0}", e.Message));
            }

            // Assert
            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count == 20);

            // Cleanup
            Directory.Delete(storage.GetStoragePath<EnginePlayer>(), true);
        }

        /// <summary>
        /// Deletes a single object using the engines XML storage.
        /// </summary>
        [TestMethod]
        public void DeleteEngineGame()
        {
            // Arrange  
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            // Instance a EngineGame object to test saving to disk.
            EngineGame game = (EngineGame)GameFactory.GetGame<EngineGame>(mudEngineAssembly);
            game.Name = "EngineGame Unit Test";
            IPersistedStorage storage = PersistedStorageFactory.GetStorageContainer<EngineXmlStorage>(mudEngineAssembly);
            storage.InitializeStorage();

            string filePath = Path.Combine(storage.RootPath, game.GetType().Name);

            // Act
            try
            {
                storage.Save<EngineGame>(game);
                storage.Delete<EngineGame>(game);
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Failed to initialize EngineXmlStorage.\n{0}", e.Message));
            }

            // Assert
            Assert.IsFalse(File.Exists(storage.GetStoragePath<EngineGame>(game)));
        }

        /// <summary>
        /// Deletes collection of objects using the engines XML storage.
        /// </summary>
        [TestMethod]
        public void DeleteEnginePlayers()
        {
            // Arrange  
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();

            // Instance a EngineGame object to test saving to disk.
            IPersistedStorage storage = PersistedStorageFactory.GetStorageContainer<EngineXmlStorage>(mudEngineAssembly);
            storage.InitializeStorage();

            // Create 20 instances.
            var players = new List<EnginePlayer>();
            for (int count = 0; count < 20; count++)
            {
                EnginePlayer player = (EnginePlayer)MobFactory.GetPlayer<EnginePlayer>(mudEngineAssembly);
                player.Name = "Player" + count;
                players.Add(player);
            }

            // Act
            try
            {
                storage.Save<EnginePlayer>(players.ToArray());
                storage.Delete<EnginePlayer>(players.ToArray());
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Failed to initialize EngineXmlStorage.\n{0}", e.Message));
            }

            // Assert
            Assert.IsFalse(Directory.GetFiles(storage.GetStoragePath<EnginePlayer>()).Length > 0);
        }
    }
}
