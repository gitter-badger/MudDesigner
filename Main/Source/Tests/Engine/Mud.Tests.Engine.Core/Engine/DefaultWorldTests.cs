using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mud.Engine.Core.Environment;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace Mud.Tests.Engine.Core.Engine
{
    [TestClass]
    public class DefaultWorldTests
    {
        private IWorld world;

        private bool isFirstRun = true;

        [TestMethod]
        public void DefaultWorld_Initialization_SetsWorldClock()
        {
            // Arrange
            world = new DefaultWorld();
            
            // Setting the factor really low allows the unit test to cycle the game-time quickly.
            world.HoursFactor = 0.3;
            world.HoursPerDay = 12;

            var morningState = new MorningState { StateStartTime = new TimeOfDay { Hour = 3 } };
            var afternoonState = new AfternoonState { StateStartTime = new TimeOfDay { Hour = 6 } };
            var nightState = new NightState { StateStartTime = new TimeOfDay { Hour = 10 } };

            world.TimeOfDayStates = new List<ITimeOfDayState> { morningState, afternoonState, nightState };

            // Register to be notified when the time of day changes.
            world.TimeOfDayChanged += world_TimeOfDayChanged;
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            // Act
            world.Initialize(morningState);

            // Allow the first state time change to happen so our while loop will run.
            Thread.Sleep(1500);
            while (world.CurrentTimeOfDay != morningState || isFirstRun)
            {
                Thread.Sleep(500);

                // Our safety check for Continous Integration Testing. If we are running longer than anticipated,
                // then that indicates something is broken and our loop has likely to run forever.
                // If debugging this unit test, this will result in the world being disposed after 20 seconds
                // and throwing a null reference exception during the debug session.
                if (watch.Elapsed.TotalSeconds > 30)
                {
                    world.Dispose();
                    Assert.Fail();
                    break;
                }
            }
        }

        void world_TimeOfDayChanged(object sender, TimeOfDayChangedEventArgs e)
        {
            // If we have a previous time of day, unregister our event.
            if (e.TransitioningFrom != null)
            {
                e.TransitioningFrom.TimeUpdated -= this.CurrentTimeOfDay_TimeUpdated;

                if (e.TransitioningTo.Name == "Morning")
                {
                    this.isFirstRun = false;
                }
            }

            e.TransitioningTo.TimeUpdated += this.CurrentTimeOfDay_TimeUpdated;
        }
 
        void CurrentTimeOfDay_TimeUpdated(object sender, TimeOfDay e)
        {
            Debug.WriteLine(string.Format("Current World Time: {0}:{1}", e.Hour, e.Minute));
            world.CurrentTimeOfDay.CurrentTime.Minute = 59;
        }
    }
}
