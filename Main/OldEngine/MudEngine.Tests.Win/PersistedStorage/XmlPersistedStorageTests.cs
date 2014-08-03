using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MudEngine.Engine.Core;
using MudEngine.Engine.GameObjects;
using MudEngine.Engine.GameObjects.Mob;
using MudEngine.Engine.Factories;
using MudEngine.Engine.XmlPersistedStorage;

namespace MudEngine.Tests.Win.PersistedStorage
{
    [TestClass]
    public class XmlPersistedStorageTests
    {
        [TestMethod]
        public void GetStoragePath()
        {
            // Arrange
            var storage = new XmlPersistedStorage();

            // Act
            string path = storage.GetStoragePath<DefaultPlayer>();

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(path));
        }

        [TestMethod]
        public void SaveSingleObjectTest()
        {
            // Arrange
            var game = new DefaultGame();
            var storage = new XmlPersistedStorage();
            storage.InitializeStorage();
            game.Initialize<DefaultPlayer>(storage);

            // Act
            game.StorageSource.Save<DefaultPlayer>(game.Player as DefaultPlayer);
            string filePath = game.StorageSource.GetStoragePath<IPlayer>(game.Player);

            // Assert
            Assert.IsTrue(System.IO.File.Exists(filePath));
        }
    }
}
