﻿//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Apps.Windows.Server
{
    using System;
    using System.Linq;
    using Microsoft.Practices.Unity;
    using Mud.Engine.Core.Character;
    using Mud.Engine.Core.Networking;
    using Mud.Engine.Default.Desktop.Engine;
    using Mud.Engine.DefaultDesktop.Networking;
    using Mud.Engine.Core.Engine;
    using Mud.Repositories.Shared;
    using Mud.Repositories.Engine.DefaultDesktop;
    using Mud.Services.Shared;
    using Mud.Engine.Core.Environment;
    using Mud.Services.FlatFile;
    using System.Threading;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Mud.Engine.Core.Environment.Time;
    using Mud.Engine.Core.Environment.Weather;

    /// <summary>
    /// The Mud Designer Telnet Server.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The Dependency Injection container
        /// </summary>
        private static IUnityContainer container = new UnityContainer();

        /// <summary>
        /// The engine game server
        /// </summary>
        private static IServer server;

        private static IGame game;

        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            RegisterContainerTypes();

            // Instance a new DesktopGame and try to initialize it.
            game = container.Resolve<IGame>();
            try
            {
                game.Initialize();
            }
            catch (Exception)
            {
                // Swallow temporarily.
            }

            // Instance our Default Server. This server is for Windows Desktop only.
            server = new DefaultServer();
            server.Port = 23;
            server.MaxConnections = 100;

            // Register to be notified when a player connects and disconnects.
            server.PlayerConnected += Server_PlayerConnected;
            server.PlayerDisconnected += Server_PlayerDisconnected;

            // Start the server. The DefaultPlayer Type will be instanced when each new player connects.
            server.Start<DefaultPlayer>(game);
            game.IsMultiplayer = true;

            SetupGameWorld(game);

            // Our game loop.
            while (server.Status == ServerStatus.Running)
            {
                Thread.Sleep(500);
            }

            // Check if the server has not stopped. If not, we stop.
            if (server.Status != ServerStatus.Stopped)
            {
                server.Stop();
            }

            server.PlayerConnected -= Server_PlayerConnected;
            server.PlayerDisconnected -= Server_PlayerDisconnected;
        }

        /// <summary>
        /// Handles the PlayerDisconnected event of the server control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ServerConnectionEventArgs"/> instance containing the event data.</param>
        private static void Server_PlayerDisconnected(object sender, ServerConnectionEventArgs e)
        {
            e.Player.MessageSent -= Player_MessageSent;
        }

        /// <summary>
        /// Handles the PlayerConnected event of the server control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ServerConnectionEventArgs"/> instance containing the event data.</param>
        private static void Server_PlayerConnected(object sender, ServerConnectionEventArgs e)
        {
            e.Player.MessageSent += Player_MessageSent;
        }

        /// <summary>
        /// Player_s the message sent.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private static void Player_MessageSent(object sender, InputArgs e)
        {
            Console.WriteLine(e.Message.Message);

            if (e.Message.Message.ToLower() == "stop")
            {
                server.Stop();
            }
        }

        /// <summary>
        /// Registers the container types.
        /// </summary>
        private static void RegisterContainerTypes()
        {
            container.RegisterType<IWorld, DefaultWorld>();
            container.RegisterType<IGame, DesktopGame>(new ContainerControlledLifetimeManager());
            container.RegisterType<IWorldRepository, WorldRepository>();
            container.RegisterType<IWorldService, WorldService>();
        }

        private static void SetupGameWorld(IGame game)
        {
            // Set up the Zone            
            var weatherStates = new List<IWeatherState> { new ClearWeather(), new RainyWeather(), new ThunderstormWeather() };
            IZone zone = new DefaultZone();
            zone.Name = "Country Side";
            zone.WeatherStates = weatherStates;
            zone.WeatherUpdateFrequency = 6;
            zone.WeatherChanged += (sender, weatherArgs) => Console.WriteLine(string.Format("{0} zone weather has changed to {1}", zone.Name, weatherArgs.CurrentState.Name));
            IZone zone2 = new DefaultZone();
            zone2.Name = "Castle Rock";
            zone2.WeatherStates = weatherStates;
            zone2.WeatherUpdateFrequency = 2;
            zone2.WeatherChanged += (sender, weatherArgs) => Console.WriteLine(string.Format("{0} zone weather has changed to {1}", zone2.Name, weatherArgs.CurrentState.Name));

            // Set up the World.
            IWorld world = new DefaultWorld();
            world.GameDayToRealHourRatio = 0.2;
            world.HoursPerDay = 10;
            world.Name = "Sample World";

            var morningState = new MorningState { StateStartTime = new TimeOfDay { Hour = 2 } };
            var afternoonState = new AfternoonState { StateStartTime = new TimeOfDay { Hour = 5 } };
            var nightState = new NightState { StateStartTime = new TimeOfDay { Hour = 8 } };

            world.TimeOfDayStates = new List<ITimeOfDayState> { morningState, afternoonState, nightState };
            world.TimeOfDayChanged += world_TimeOfDayChanged;

            // Set up the Realm.
            IRealm realm = new DefaultRealm();
            realm.TimeZoneOffset = new TimeOfDay { Hour = 3, Minute = 10, HoursPerDay = world.HoursPerDay };
            realm.Name = "Realm 1";

            // Initialize the environment.
            world.Initialize();
            world.AddRealmToWorld(realm);
            realm.AddZoneToRealm(zone);
            realm.AddZoneToRealm(zone2);
            game.Worlds.Add(world);
        }

        static void world_TimeOfDayChanged(object sender, TimeOfDayChangedEventArgs e)
        {
            // If we have a previous time of day, unregister our event.
            if (e.TransitioningFrom != null)
            {
                e.TransitioningFrom.TimeUpdated -= CurrentTimeOfDay_TimeUpdated;
            }

            e.TransitioningTo.TimeUpdated += CurrentTimeOfDay_TimeUpdated;
            CurrentTimeOfDay_TimeUpdated(sender, e.TransitioningTo.CurrentTime);
        }

        static void CurrentTimeOfDay_TimeUpdated(object sender, TimeOfDay e)
        {

            if (!(sender is ITimeOfDayState))
            {
                return;
            }

            // Indicates a new hour has passed.
            string hour = string.Empty;
            string minute = string.Empty;

            if (e.Hour < 10)
            {
                hour = string.Format("0{0}", e.Hour);
            }
            else
            {
                hour = e.Hour.ToString();
            }

            if (e.Minute < 10)
            {
                minute = string.Format("0{0}", e.Minute);
            }
            else
            {
                minute = e.Minute.ToString();
            }

            string timeOfDay = string.Empty;
            if (e.Hour < 12)
            {
                timeOfDay = "AM";
            }
            else
            {
                timeOfDay = "PM";
            }

            ITimeOfDayState timeOfDayState = (ITimeOfDayState)sender;

            Console.WriteLine(string.Format("World time is {0}:{1} {2} in the {3}", hour, minute, timeOfDay, timeOfDayState.Name));
            foreach (IRealm realm in game.Worlds.FirstOrDefault().Realms)
            {
                Console.WriteLine(string.Format("{0} world time is {1} in the {2}", realm.Name, realm.CurrentTimeOfDay.ToString(), realm.GetCurrentTimeOfDayState().Name));
            }

            Console.WriteLine(Environment.NewLine);
        }
    }
}
