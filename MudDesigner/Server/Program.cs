//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using MudEngine.Engine.Core;
using MudEngine.Engine.Factories;
using MudEngine.Engine.GameObjects.Environment;
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
            // IGame game = (IGame)ScriptFactory.GetScript(EngineSettings.Default.GameScript, null);
            MultiplayerGame game = new EngineFactory<MultiplayerGame>().GetObject();
            game.Logger = handler;

            game.Initialize<DefaultPlayer>(null);
            game.Start<ServerPlayer, DefaultPlayer>();

            Console.WriteLine("Server running...");
            while (game.IsRunning)
            {
            }
        }
    }
}
