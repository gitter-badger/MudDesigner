/* LoadFileCommand
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Provides support for loading the game world.  This is an admin only command.
 */

//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.IO;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Commands
{
    /// <summary>
    /// Provides support for loading the game world.  This is an admin only command.
    /// </summary>
    public class LoadFileCommand : ICommand
    {
        private IGame _game;
        private readonly string _fileToLoad;
        private IPlayer _player;

        public LoadFileCommand(IPlayer player, IGame game)
        {
            _game = game;
            _player = player;
        }

        public void Execute()
        {
            if (_player.Role == CharacterRoles.Admin)
            {
                LoadGame();

                _player.SendMessage(String.Format("Successfully loaded {0} ", _fileToLoad));

                // We need to set the RoomState for the player.
            }


        }

        //@ToDO: I have this planned out, for loading and saving, I just need to double check how I am saving game objects and creating definitions for loading the class types back. (I have sample for how i plan on doing this in general) and a test project for doing it.
        //@ToDO: I would also like to Encrypt the file. Possibly doing HMAC-SHA1 or something.... to prevent hacking and cleartext passwords.
        public void LoadGame()
        {
            var game = _game as Game;

            if (game != null) 
                game.RestoreWorld();
        }
    }
}