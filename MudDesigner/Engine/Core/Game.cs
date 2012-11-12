﻿/* Game
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Provides Methods and Properties for adding objects to the game world, managing the server and maintaining the state of the game,
 *                   thereby serving as a base class for all Types that run and manage the MUD Game.
 */

//Microsoft .NET using statements
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

//AllocateThis! Mud Designer Using statements
using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Properties;
using Newtonsoft.Json;

namespace MudDesigner.Engine.Core
{
    /// <summary>
    /// Provides Methods and Properties for adding objects to the game world, managing the server and maintaining the state of the game,
    /// thereby serving as a base class for all Types that run and manage the MUD Game.
    /// </summary>
    public abstract class Game : GameObject, IGame
    {
        /// <summary>
        /// Version of this game.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets ors Sets the website for this game.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Gets or Sets if the Rooms name will be displayed upon entering a room
        /// </summary>
        [DisplayName("Hide Room Names")]
        public bool HideRoomNames { get; set; }

        /// <summary>
        /// Gets or Sets if the game should automatically save.
        /// </summary>
        [DisplayName("Auto-Save")]
        public bool Autosave {get;set;}

        /// <summary>
        /// Gets or Sets the frequency that the game saves when Autosave=true
        /// </summary>
        [DisplayName("Save Frequency")]
        public int SaveFrequency {get;set;}

        /// <summary>
        /// Gets a reference to the game server
        /// </summary>
        [Browsable(false)]
        public IServer Server { get; set; }

        /// <summary>
        /// Gets a reference to the game world
        /// </summary>
        [Browsable(false)]
        public IWorld World { get; set; }

        /// <summary>
        /// Gets a the last time that the game was saved.
        /// </summary>
        [Browsable(false)]
        public DateTime LastSave { get; private set; }

        /// <summary>
        /// Takes all of this Game Objects properties and copies them over to the argument object.
        /// </summary>
        /// <param name="copyTo">The object that will have it's properties replaced with the calling Object</param>
        public override void CopyState(ref dynamic copyTo)
        {
            //If the new object is a Game, we can safely copy our properties
            if (copyTo is IGame)
            {
                ScriptObject newObject = new ScriptObject(copyTo);

                newObject.SetProperty("Version", Version, null);
                newObject.SetProperty("Website", Website, null);
                newObject.SetProperty("HideRoomNames", HideRoomNames, null);
                newObject.SetProperty("Autosave", Autosave, null);
                newObject.SetProperty("SaveFrequency", SaveFrequency, null);
                newObject.SetProperty("Server", Server, null);
                newObject.SetProperty("World", World, null);
            }

            base.CopyState(ref copyTo);
        }

        /// <summary>
        /// Sets up all of the game objects for use, loads any saved states, restores the world and links itself to the server.
        /// </summary>
        /// <param name="startedServer">A reference to a IServer Type that has had its Start() method called.</param>
        /// <param name="world">A instance of a IWorld Type.  This should be a non-restored World as the game will invoke the IWorld.Load method itself.</param>
        /// <returns></returns>
        public virtual bool Initialize(IServer startedServer)
        {
            //See if we need to update the settings.
            string version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
            
            //If the versions do not equal each other, update the stored settings to match this version of the engine.
            if (version != EngineSettings.Default.Version)
            {
                EngineSettings.Default.Upgrade();
                EngineSettings.Default.Save();
            }

            Name = "AllocateThis! Mud Game";

            if (startedServer != null)
            {
                Server = startedServer;
            }
            else
            {
                Server = new Server();
            }

            //Add the engine assembly to the Script Factory
            ScriptFactory.AddAssembly(Assembly.GetExecutingAssembly());
            
            //Add any additional assemblies that might have been compiled elsewhere (downloadable assemblies)
            if (MudDesigner.Engine.Properties.EngineSettings.Default.ScriptLibrary.Count != 0)
            {
                foreach (string assembly in MudDesigner.Engine.Properties.EngineSettings.Default.ScriptLibrary)
                {
                    //Make sure the assembly actually exists first.
                    if (File.Exists(assembly))
                        ScriptFactory.AddAssembly(System.Environment.CurrentDirectory + "\\" + assembly);
                }
            }

            //Get a reference to a new instance of a IWorld Type.
            IWorld world = (IWorld)ScriptFactory.GetScript(MudDesigner.Engine.Properties.EngineSettings.Default.WorldScript, null);

            //If it's not null, we apply it to the Game.World property.
            if (world != null)
                World = world;
            return true;
        }

        /// <summary>
        /// Restores the world to it's previously saved state.
        /// </summary>
        public void RestoreWorld()
        {
            //Build a path using our current install directory + "Saves" + the engine setting for the world save file.
            var fileAndPathToSave = Path.Combine(Directory.GetCurrentDirectory(), "saves",
                                                MudDesigner.Engine.Properties.EngineSettings.Default.WorldSaveFile);

            //If the file doesn't exists, we abort.
            if (!File.Exists(fileAndPathToSave))
                return;

            //Our file IO manager
            FileIO fileLoad = new FileIO();

            try
            {
                //Try to load and restore our world.
                World = (IWorld)fileLoad.Load(fileAndPathToSave, typeof(IWorld));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       /// <summary>
       /// Saves the current state of the world to file.
       /// </summary>
        public void SaveWorld()
        {
            LastSave = DateTime.Now;

            var fileAndPathToSave = Path.Combine(Directory.GetCurrentDirectory(), "saves",
                                                 MudDesigner.Engine.Properties.EngineSettings.Default.WorldSaveFile);

            FileIO fileSave = new FileIO();
            fileSave.Save(World, fileAndPathToSave);
        }
    }
}
