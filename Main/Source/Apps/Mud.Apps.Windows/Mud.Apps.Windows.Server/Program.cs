//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Apps.Windows.Server
{
    using System;
    using Microsoft.Practices.Unity;
    using Mud.Engine.Core.Mob;
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

        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            RegisterContainerTypes();

            // Instance a new DesktopGame and try to initialize it.
            var game = container.Resolve<IGame>();
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

            foreach(IWorld world in game.Worlds)
            {
                world.TimeOfDayChanged += (sender, timeOfDay) => Console.WriteLine(string.Format("It is now {0}", timeOfDay.Name));
            }

            // Our game loop.
            while (server.Status == ServerStatus.Running)
            {
                Thread.Sleep(2500);
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
    }
}
