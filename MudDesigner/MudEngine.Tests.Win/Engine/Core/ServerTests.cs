//-----------------------------------------------------------------------
// <copyright file="ServerTests.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MudEngine.Tests;
using MudEngine.Engine.Core;
using MudEngine.Engine.Factories;

namespace MudEngine.Tests.Win.Engine.Core
{
    [TestClass]
    public class ServerTests
    {
        /*
        [TestMethod]
        public void StarServer()
        {
            // Arrange
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();
            IServer server = ServerFactory.GetServer<EngineServer>(mudEngineAssembly);
            server.Port = 1001;
            IGame game = GameFactory.GetGame<EngineGame>(mudEngineAssembly);

            // Act
            server.Start(game);

            // Assert
            Assert.IsTrue(server.Status == ServerStatus.Running);
        }

        [TestMethod]
        public void StopServer()
        {
            // Arrange
            // Fetch the MudEngine.dll assembly from memory
            var mudEngineAssembly = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies())
                .Where(assembly => assembly.ManifestModule.Name == "MudEngine.dll")
                .ToArray();
            IServer server = ServerFactory.GetServer<EngineServer>(mudEngineAssembly);
            server.Port = 1000;
            IGame game = GameFactory.GetGame<EngineGame>(mudEngineAssembly);

            // Act
            server.Start(game);
            server.Stop();

            // Assert
            Assert.IsTrue(server.Status == ServerStatus.Stopped);
            Assert.IsTrue(server.IsEnabled == false);
            Assert.IsTrue(server.Connections.Count == 0);
        }
         */
    }
}
