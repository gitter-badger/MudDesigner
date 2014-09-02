using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mud.Engine.Core.Environment;
using Mud.Engine.Core.Environment.Travel;
using Mud.Engine.Core.Character;

namespace Mud.Tests.Engine.Core.Environment
{
    [TestClass]
    public class DefaultDoorwayTests
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DefaultDoorway_ConnectNullRooms_ThrowsNullReference()
        {
            // Arrange
            var doorway = new DefaultDoorway(new NorthDirection());

            // Act
            try
            {
                doorway.ConnectRooms(null, null);
            }
            catch (NullReferenceException)
            {
                throw;
            }

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DefaultDoorway_ConnectRooms_WithNullDirection_ThrowsNullReference()
        {
            // Arrange
            var doorway = new DefaultDoorway(null);
            var arrivalRoomMock = new Mock<IRoom>();
            var departureRoomMock = new Mock<IRoom>();
            IRoom arrivalRoom = arrivalRoomMock.Object;
            IRoom departureRoom = departureRoomMock.Object;

            // Act
            try
            {
                doorway.ConnectRooms(departureRoom, arrivalRoom);
            }
            catch (NullReferenceException)
            {
                throw;
            }

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void DefaultDoorway_ConnectRooms_CurrentRoomSet()
        {
            // Arrange
            var doorway = new DefaultDoorway(new NorthDirection());
            var arrivalRoomMock = new Mock<IRoom>();
            arrivalRoomMock
                .SetupGet(s => s.Name)
                .Returns("Hallway");
            arrivalRoomMock
                .SetupGet(s => s.Doorways)
                .Returns(new List<IDoorway>());
            var departureRoomMock = new Mock<IRoom>();
            departureRoomMock
                .SetupGet(s => s.Name)
                .Returns("Bedroom");
            departureRoomMock
                .SetupGet(s => s.Doorways)
                .Returns(new List<IDoorway>());

            IRoom arrivalRoom = arrivalRoomMock.Object;
            IRoom departureRoom = departureRoomMock.Object;

            // Act
            doorway.ConnectRooms(departureRoom, arrivalRoom);

            // Assert
            Assert.IsNotNull(doorway.ArrivalRoom, "Arrival room was not set.");
            Assert.IsTrue(doorway.ArrivalRoom == arrivalRoom, "Arrival room was set to the wrong room.");
        }

        [TestMethod]
        public void DefaultDoorway_ConnectRooms_OppositeIsSet()
        {
            // Arrange
            var doorway = new DefaultDoorway(new NorthDirection());
            var arrivalRoomMock = new Mock<IRoom>();
            arrivalRoomMock
                .SetupGet(s => s.Name)
                .Returns("Hallway");
            arrivalRoomMock
                .SetupGet(s => s.Doorways)
                .Returns(new List<IDoorway>());

            var departureRoomMock = new Mock<IRoom>();
            departureRoomMock
                .SetupGet(s => s.Name)
                .Returns("Bedroom");
            departureRoomMock
                .SetupGet(s => s.Doorways)
                .Returns(new List<IDoorway>());

            IRoom arrivalRoom = arrivalRoomMock.Object;
            IRoom departureRoom = departureRoomMock.Object;

            // Act
            doorway.ConnectRooms(departureRoom, arrivalRoom);

            // Assert
            Assert.IsTrue(arrivalRoom.Doorways.FirstOrDefault().ArrivalRoom == departureRoom, "Reverse room was not set.");
        }
        
        [TestMethod]
        public void DefaultDoorway_DisconnectDoorways_ClearsBothRooms()
        {
            // Arrange
            var doorway = new DefaultDoorway(new NorthDirection());
            var arrivalRoomMock = new Mock<IRoom>();
            arrivalRoomMock
                .SetupGet(s => s.Doorways)
                .Returns(new List<IDoorway>());
            arrivalRoomMock
                .SetupGet(s => s.Name)
                .Returns("Hallway");

            var departureRoomMock = new Mock<IRoom>();
            departureRoomMock
                .SetupGet(s => s.Doorways)
                .Returns(new List<IDoorway>());
            departureRoomMock
                .SetupGet(s => s.Name)
                .Returns("Bedroom");

            IRoom arrivalRoom = arrivalRoomMock.Object;
            IRoom departureRoom = departureRoomMock.Object;

            doorway.ConnectRooms(departureRoom, arrivalRoom);

            // Act
            doorway.DisconnectRoom();

            // Assert
            Assert.IsTrue(arrivalRoom.Doorways.Count == 0, "Arrival room did not get disconnected.");
            Assert.IsTrue(departureRoom.Doorways.Count == 0, "Departure room did not get disconnected.");
        }

        [TestMethod]
        public void DefaultDoorway_TraverseDoorway_ChangesCurrentRoom()
        {
            // Arrange
            // Set up the two rooms.
            var departureMock = new Mock<IRoom>();
            departureMock.SetupProperty(s => s.Name, "Bedroom");
            departureMock
                .SetupGet(s => s.Doorways)
                .Returns(new List<IDoorway>());

            var arrivalMock = new Mock<IRoom>();
            arrivalMock.SetupProperty(s => s.Name, "Hallway");
            arrivalMock
                .SetupGet(s => s.Doorways)
                .Returns(new List<IDoorway>());

            IRoom arrivalRoom = arrivalMock.Object;
            IRoom departureRoom = departureMock.Object;

            // Link the rooms
            IDoorway doorway = new DefaultDoorway(new NorthDirection());
            doorway.ConnectRooms(departureRoom, arrivalRoom);

            // Create our character.
            var characterMock = new Mock<ICharacter>();
            characterMock.SetupProperty(s => s.CurrentRoom, departureRoom);
            ICharacter character = characterMock.Object;

            // Act
            doorway.TraverseDoorway(character);

            // Assert
            Assert.IsNotNull(character.CurrentRoom, "Room was set to null when it shouldn't have been.");
            Assert.IsTrue(character.CurrentRoom == arrivalRoom, "Room was not changed like expected.");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DefaultDoorway_TraverseDoorway_WithNullCharacter_ThrowsNullReference()
        {
            // Arrange
            IDoorway doorway = new DefaultDoorway(new NorthDirection());

            // Act
            try
            {
                doorway.TraverseDoorway(null);
            }
            catch (NullReferenceException)
            {
                throw;
            }

            Assert.Fail();
        }
    }
}
