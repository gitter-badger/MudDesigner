//-----------------------------------------------------------------------
// <copyright file="IGame.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Scripting;

namespace MudDesigner.Engine.Core
{
    /// <summary>
    /// Provides Methods and Properties for adding objects to the game world, managing the server and maintaining the state of the game,
    /// thereby serving as a base class for all Types that run and manage the MUD Game.
    /// </summary>
    public interface IGame : IGameObject
    {
        /// <summary>
        /// Version of this game.
        /// </summary>
        string Version { get; set; }

        /// <summary>
        /// Gets ors Sets the website for this game.
        /// </summary>
        string Website { get; set; }

        /// <summary>
        /// Gets or Sets if the Rooms name will be displayed upon entering a room
        /// </summary>
        bool HideRoomNames { get; set; }

        /// <summary>
        /// Gets or Sets if the game should automatically save.
        /// </summary>
        bool Autosave { get; set; }

        /// <summary>
        /// Gets or Sets the frequency that the game saves when Autosave=true
        /// </summary>
        int SaveFrequency { get; set; }

        /// <summary>
        /// Gets a the last time that the game was saved.
        /// </summary>
        DateTime LastSave { get; }

        /// <summary>
        /// Gets a reference to the game world
        /// </summary>
        IWorld World { get; set; }

        /// <summary>
        /// Gets a reference to the game server
        /// </summary>
        IServer Server { get; set; }

        /// <summary>
        /// Sets up all of the game objects for use, loads any saved states, restores the world and links itself to the server.
        /// </summary>
        /// <param name="startedServer">A reference to a IServer Type that has had its Start() method called.</param>
        /// <param name="world">A instance of a IWorld Type.  This should be a non-restored World as the game will invoke the IWorld.Load method itself.</param>
        /// <returns></returns>
        bool Initialize(IServer startedServer);

        /// <summary>
        /// Restores the world to it's previously saved state.
        /// </summary>
        void RestoreWorld();

        /// <summary>
        /// Saves the current state of the world to file.
        /// </summary>
        void SaveWorld();
    }
}