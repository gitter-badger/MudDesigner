//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using MudEngine.Engine.Core;
using MudEngine.Engine.Factories;
using MudEngine.Engine.GameObjects.Mob;
using MudEngine.Engine.Networking;

namespace MudDesigner.Server
{
    /// <summary>
    /// A stand-alone server that will run the Mud Game Engine when configured properly.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main entry point of the server.
        /// </summary>
        /// <param name="args">The server arguments.</param>
        static void Main(string[] args)
        {
            // Prepare our Progress object so the game can push messages back to the server console.
            var handler = new Progress<IMessage>((message) => Console.WriteLine(message.Message));

            Console.WriteLine("Server app starting...");

            // Instance the server.
            var game = new EngineFactory<MultiplayerGame>().GetObject();
            game.Logger = handler;

            // Initialize the game
            game.Initialize<DefaultPlayer>(null);

            // Start the server.
            game.Start<ServerPlayer, DefaultPlayer>();

            Console.WriteLine("Server running...");
            while (game.IsRunning)
            {
            }
        }
    }
}
