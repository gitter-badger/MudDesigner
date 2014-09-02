using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mud.Engine.Core.Environment;
using Moq;
using System.Collections.Generic;
using Mud.Engine.Core.Environment.Time;

namespace Mud.Tests.Engine.Core.Environment
{
    [TestClass]
    public class DefaultRealmTests
    {
        /// <summary>
        /// Tests that initialization of a realm throws the require exception 
        /// when no world is provided.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DefaultRealm_InitializesWithoutWorld_ThrowsNullReference()
        {
            // Arrange
            var realm = new DefaultRealm();

            // Act
            try
            {
                realm.Initialize(null, new TimeOfDay());
            }
            catch (NullReferenceException)
            {   
                throw;
            }

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that initialization of a realm throws the require exception 
        /// when no time of day is provided.        
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DefaultRealm_InitializesWithoutTimeOfDay_ThrowsNullReference()
        {
            // Arrange
            var worldMock = new Mock<IWorld>();
            var realm = new DefaultRealm();
            IWorld world = worldMock.Object;

            // Act
            try
            {
                realm.Initialize(world, null);
            }
            catch (NullReferenceException)
            {
                throw;
            }

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that the DefaultRealm initializes properly.
        /// </summary>
        [TestMethod]
        public void DefaultRealm_Initializes()
        {
            // Arrange
            var worldMock = new Mock<IWorld>();
            var realm = new DefaultRealm();
            realm.TimeZoneOffset = new TimeOfDay { Hour = 3, Minute = 30, HoursPerDay = 24 };
            var timeOfDay = new TimeOfDay { Hour = 5, Minute = 10, HoursPerDay = 24 };
            
            IWorld world = worldMock.Object;
            world.HoursPerDay = 24;

            // Act
            realm.Initialize(world, timeOfDay);

            // Assert
            Assert.IsNotNull(realm.World, "World was not assigned.");

            // Time-zone corrected time should be changed from 5:10 to 1:40
            Assert.IsTrue(realm.CurrentTimeOfDay.Hour == 1, "Time zone hour offset was not properly applied.");
            Assert.IsTrue(realm.CurrentTimeOfDay.Minute == 40, "Time zone minute offset was not properly applied.");
        }

        /// <summary>
        /// Tests that the DefaultRealm throws a NullReferenceException when
        /// attempting to add a null zone.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DefaultRealm_AddNullZoneToRealm_ThrowsNullReference()
        {
            // Arrange
            var realm = new DefaultRealm();

            // Act
            // Should throw an exception due to null zone collection.
            try
            {
                realm.AddZoneToRealm(null);
            }
            catch (NullReferenceException)
            {
                throw;
            }

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that the DefaultRealm adds a Zone to it's internal collection properly.
        /// </summary>
        [TestMethod]
        public void DefaultRealm_AddZoneToRealm_AddsToCollection()
        {
            // Arrange
            var realm = new DefaultRealm();
            var zoneMock = new Mock<IZone>();
            IZone zone = zoneMock.Object;

            // Act
            realm.AddZoneToRealm(zone);

            // Assert
            Assert.IsNotNull(realm.Zones, "Realm's Zones collection was null");
            Assert.IsTrue(realm.Zones.Contains(zone), "Realm did not add the Zone.");
        }

        /// <summary>
        /// Tests that a DefaultRealm throws a NullReferenceException when attempting to
        /// apply a timezone offset with a null TimeOfDay instance.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DefaultRealm_ApplyTimeZoneOffset_WithNullTimeOfDay_ThrowsNullReference()
        {
            // Arrange
            var realm = new DefaultRealm();

            // Act
            try
            {
                realm.ApplyTimeZoneOffset(null);
            }
            catch (NullReferenceException)
            {   
                throw;
            }

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that a DefaultRealm throws a NullReferenceException when attempting to
        /// apply a timezone offset with a null World instance.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DefaultRealm_ApplyTimeZoneOffset_WithNullWorld_ThrowsNullReference()
        {
            // Arrange
            var realm = new DefaultRealm();
            var timeOfDay = new TimeOfDay
            {
                Hour = 2,
                Minute = 0,
                HoursPerDay = 24,
            };

            // Act
            try
            {
                realm.ApplyTimeZoneOffset(timeOfDay);
            }
            catch (NullReferenceException)
            {   
                throw;
            }

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that a DefaultRealm throws an ArgumentOutOfRangeException when attempting to
        /// apply a timezone offset with a TimeOfDay instance having negative values.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DefaultRealm_ApplyTimeZoneOffset_WithNegativeOffset_ThrowsArgumentOutOfRange()
        {
            // Arrange
            var realm = new DefaultRealm();
            var timeOfDay = new TimeOfDay
            {
                Hour = 2,
                Minute = 0,
                HoursPerDay = 24,
            };
            var timeZoneOffset = new TimeOfDay
            {
                Hour = -2,
            };
            realm.TimeZoneOffset = timeZoneOffset;

            // Act
            try
            {
                realm.ApplyTimeZoneOffset(timeOfDay);
            }
            catch (ArgumentOutOfRangeException)
            {   
                throw;
            }

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that a DefaultRealm applies a time zone offset properly with a
        /// null time-zone offset.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DefaultRealm_ApplyTimeZoneOffsetWithNullTimeZoneOffset_AppliesOffset()
        {
            // Arrange
            // Set up the Time of Day state.
            var timeOfDayStateMock = new Mock<ITimeOfDayState>();
            ITimeOfDayState timeOfDayState = timeOfDayStateMock.Object;
            timeOfDayState.StateStartTime = new TimeOfDay
            {
                Hour = 6,
                Minute = 0,
                HoursPerDay = 24,
            };

            // Set up the world.
            var worldMock = new Mock<IWorld>();
            IWorld world = worldMock.Object;
            world.HoursPerDay = 24;
            world.Initialize(timeOfDayState);

            // Set up the Realm
            var realm = new DefaultRealm();
            world.AddRealmToWorld(realm);
            var timeOfDay = new  TimeOfDay
            {
                Hour = 2,
                Minute = 0,
                HoursPerDay = 24,
            };

            // Act
            try
            {
                realm.ApplyTimeZoneOffset(timeOfDay);
            }
            catch (NullReferenceException)
            {   
                throw;
            }

            Assert.Fail();
        }

        /// <summary>
        /// Tests that a DefaultRealm applies a time zone offset properly with a
        /// valid time-zone offset.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DefaultRealm_ApplyTimeZoneOffset_AppliesOffset()
        {
            // Arrange
            // Set up the Time of Day state.
            var timeOfDayStateMock = new Mock<ITimeOfDayState>();
            ITimeOfDayState timeOfDayState = timeOfDayStateMock.Object;
            timeOfDayState.StateStartTime = new TimeOfDay
            {
                Hour = 6,
                Minute = 0,
                HoursPerDay = 24,
            };

            // Set up the Realm
            var realm = new DefaultRealm();
            realm.CurrentTimeOfDay = timeOfDayState.CurrentTime;
            var timeOfDay = new TimeOfDay
            {
                Hour = 4,
                Minute = 0,
                HoursPerDay = 24,
            };
            var timeZoneOffSet = new TimeOfDay
            {
                Hour = 1,
            };

            // Act
            try
            {
                realm.ApplyTimeZoneOffset(timeOfDay);
            }
            catch (NullReferenceException)
            {   
                throw;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void DefaultRealm_GetCurrentTimeOfDayState_ReturnsRealmTimeOfDayState()
        {
            // Arrange

            // Set up our mock Time of Day States
            var morningStateMock = new Mock<ITimeOfDayState>();
            var afternoonStateMock = new Mock<ITimeOfDayState>();
            morningStateMock
                .SetupGet(s => s.StateStartTime)
                .Returns(new TimeOfDay { Hour = 6, HoursPerDay = 24 });
            afternoonStateMock
                .SetupGet(s => s.StateStartTime)
                .Returns(new TimeOfDay { Hour = 12, HoursPerDay = 24 });
            
            // Set up world time of day states properties.
            var worldMock = new Mock<IWorld>();
            worldMock.SetupProperty(s => 
                    s.TimeOfDayStates, 
                    new List<ITimeOfDayState> 
                    {
                        morningStateMock.Object, 
                        afternoonStateMock.Object 
                    });
            worldMock
                .SetupGet(s => s.CurrentTimeOfDay)
                .Returns(worldMock.Object.TimeOfDayStates.FirstOrDefault());
            IWorld world = worldMock.Object;

            
            // Set up Realm.
            var realm = new DefaultRealm();
            realm.Initialize(world, new TimeOfDay { Hour = 12, Minute = 30, HoursPerDay = 24 });

            // Act
            ITimeOfDayState state = realm.GetCurrentTimeOfDayState();

            // Assert
            Assert.IsTrue(state == world.TimeOfDayStates.Last(), "Time of Day state was not the afternoon.");
        }

        [TestMethod]
        public void DefaultRealm_GetCurrentTimeOfDayState_ReturnsWorldsTimeOfDayState()
        {
            // Arrange

            // Set up our mock Time of Day States
            var morningStateMock = new Mock<ITimeOfDayState>();
            var afternoonStateMock = new Mock<ITimeOfDayState>();
            morningStateMock
                .SetupGet(s => s.StateStartTime)
                .Returns(new TimeOfDay { Hour = 6, HoursPerDay = 24 });
            afternoonStateMock
                .SetupGet(s => s.StateStartTime)
                .Returns(new TimeOfDay { Hour = 12, HoursPerDay = 24 });

            // Set up world time of day states properties.
            var worldMock = new Mock<IWorld>();
            worldMock.SetupProperty(s =>
                    s.TimeOfDayStates,
                    new List<ITimeOfDayState> 
                    {
                        morningStateMock.Object, 
                        afternoonStateMock.Object 
                    });
            worldMock
                .SetupGet(s => s.CurrentTimeOfDay)
                .Returns(worldMock.Object.TimeOfDayStates.FirstOrDefault());
            IWorld world = worldMock.Object;


            // Set up Realm.
            var realm = new DefaultRealm();
            realm.Initialize(world, new TimeOfDay { Hour = 3, Minute = 30, HoursPerDay = 24 });

            // Act
            ITimeOfDayState state = realm.GetCurrentTimeOfDayState();

            // Assert
            Assert.IsTrue(state == world.CurrentTimeOfDay, "Time of Day state was not set to match the Worlds.");
        }
    }
}
