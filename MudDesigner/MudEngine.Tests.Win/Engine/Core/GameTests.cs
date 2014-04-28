//-----------------------------------------------------------------------
// <copyright file="GameTests.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MudEngine.Tests;
using MudEngine.Engine.Core;
using MudEngine.Engine.Factories;

namespace MudEngine.Tests.Win
{
    [TestClass]
    public class GameTests
    {
        /// <summary>
        /// Initializes the game without a server.
        /// </summary>
        [TestMethod]
        public void InitializeGameWithoutServer()
        {
            // Arrange
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToList();

            IGame game = GameFactory.GetGame<EngineGame>();
            IPersistedStorage storage = PersistedStorageFactory.GetStorageContainer<EngineXmlStorage>(mudEngineAssembly.ToArray());

            // Act
            try
            {
                game.Initialize(storage);
            }
            catch(Exception e)
            {
                Assert.Fail(string.Format("IGame.Initialize for {0} failed.\n{1}", game.GetType().Name, e.Message));
            }
            finally
            {
                // Assert
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Initializes the game with a server.
        /// </summary>
        [TestMethod]
        public void InitializeGameWithServer()
        {
            // Arrange
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToList();

            // Fetch our objects from the factories.
            IGame game = GameFactory.GetGame<EngineGame>();
            IPersistedStorage storage = PersistedStorageFactory.GetStorageContainer<EngineXmlStorage>(mudEngineAssembly.ToArray());
            IServer server = ServerFactory.GetServer<EngineServer>(mudEngineAssembly.ToArray());

            // Act
            try
            {
                // Attempt to initialize the game.
                game.Initialize(storage, server);
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("IGame.Initialize for {0} failed with exception \n'{1}'.", game.GetType().Name, e.Message));
            }
            finally
            {
                // Assert
                Assert.IsTrue(game.IsRunning);
            }
        }

        /// <summary>
        /// Initializes the game with a server without any storage, resulting in an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void InitializeGameWithoutStorage()
        {
            // Arrange
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToList();

            // Fetch our objects from the factories.
            IGame game = GameFactory.GetGame<EngineGame>();

            // Act
            game.Initialize(null);

            // Assert
            Assert.Fail("Test failed to throw the appropriate exception.");
        }
    }
}
