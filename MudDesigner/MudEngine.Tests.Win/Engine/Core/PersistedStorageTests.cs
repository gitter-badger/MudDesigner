using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MudEngine.Engine.Factories;
using MudEngine.Engine.Core;

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
            IPersistedStorage storage = StorageFactory.GetStorage<EngineXmlStorage>(mudEngineAssembly);

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
        /// Initializes the engines XML storage.
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
            IPersistedStorage storage = StorageFactory.GetStorage<EngineXmlStorage>(mudEngineAssembly);
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
        }
    }
}
