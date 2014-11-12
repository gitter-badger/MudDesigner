using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mud.Engine.Runtime.Core;
using Autofac;
using Autofac.Builder;
using Mud.Engine.Shared.Core;
using Tests.Engine.Runtime.Core;
using Mud.Engine.Runtime.Environment;
using Mud.Engine.Shared.Environment;
using Mud.Engine.Shared.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests.Engine.Runtime.Core
{
    [TestClass]
    public class DefaultGameTests
    {
        private IContainer container;

        [TestInitialize]
        public void Setup()
        {
            var builder = new ContainerBuilder();

            // Set up our mock Log and World services.
            var loggerMock = new Mock<ILoggingService>();
            loggerMock.Setup(logger => logger.Log(It.IsAny<IMessage>()));

            var worldServiceMock = new Mock<IWorldService>();
            worldServiceMock
                .Setup(worldService => worldService.GetAllWorlds(It.IsAny<bool>(), It.IsAny<IDataStoreContext>()))
                .Returns(() =>
                {
                    IWorld world = new DefaultWorld();
                    var taskCompletion = new TaskCompletionSource<IEnumerable<IWorld>>();
                    taskCompletion.SetResult(new List<IWorld> { world });

                    return taskCompletion.Task;
                });

            // Register our types.
            builder.RegisterType<MessageFixture>().As<IMessage>();
            builder.RegisterInstance(loggerMock.Object).As<ILoggingService>();
            builder.RegisterInstance(worldServiceMock.Object).As<IWorldService>();

            // Build the IoC container.
            container = builder.Build();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Game_throws_exception_with_invalid_world_service()
        {
            // Arrange
            var game = new DefaultGame(container.Resolve<ILoggingService>(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Game_throws_exception_with_invalid_logging_service()
        {
            // Arrange
            var game = new DefaultGame(null, container.Resolve<IWorldService>());
        }

        [TestMethod]
        public async Task Game_can_initialize()
        {
            // Arrange
            var game = new DefaultGame(container.Resolve<ILoggingService>(), container.Resolve<IWorldService>());

            // Act
            await game.Initialize();

            // Assert
            Assert.IsNotNull(game.Worlds);
            Assert.IsTrue(game.Worlds.Count == 1);
        }
    }
}
