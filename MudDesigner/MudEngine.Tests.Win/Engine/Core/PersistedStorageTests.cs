using System;
using System.Collections.Generic;
using System.Linq;
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
            catch
            {
                Assert.Fail("Failed to initialize EngineXmlStorage");
            }

            // Assert
            Assert.IsNotNull(storage);
        }
    }
}
