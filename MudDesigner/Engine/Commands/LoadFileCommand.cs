using System;
using System.Collections.Generic;
using System.IO;

using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Commands
{
    public class LoadFileCommand : ICommand
    {
        private IGame _game;
        private readonly string _fileToLoad;
        private BasePlayer _player;

        public LoadFileCommand(IGame game)
        {
            _game = game;
        }

        public void Execute()
        {

            LoadGame();

            _player.SendMessage(String.Format("Successfully loaded {0} ", _fileToLoad));

            // We need to set the RoomState for the player.



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