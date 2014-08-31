using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mud.Repositories.Shared;
using Mud.Engine.Core.Environment;
using System.Collections.Generic;

namespace Mud.Tests.Engine.Core.Engine
{
    [TestClass]
    public class DefaultGameTests
    {
        private IWorldRepository worldRepository;

        [TestInitialize]
        public void Setup()
        {
            var worldRepositoryMock = new Mock<IWorldRepository>();
            worldRepositoryMock
                .Setup(s => s.GetAllWorlds(It.IsAny<bool>()))
                .ReturnsAsync(new List<IWorld>());

            this.worldRepository = worldRepositoryMock.Object;
        }

        [TestMethod]
        public void DefaultGame()
        {
        }
    }
}
