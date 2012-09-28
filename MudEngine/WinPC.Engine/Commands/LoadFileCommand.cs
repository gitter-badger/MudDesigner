﻿using System;
using System.Collections.Generic;
using System.IO;
using WinPC.Engine.Abstract.Core;
using WinPC.Engine.Abstract.Objects;
using WinPC.Engine.Core;

namespace WinPC.Engine.Commands
{
    public class LoadFileCommand : ICommand
    {
        private IEngine _engine;
        private readonly string _fileToLoad;
        private EnginePlayer _player;

        public LoadFileCommand(string fileToLoad, IEngine engine)
        {
            _engine = engine;
            _fileToLoad = fileToLoad;
        }

        public void Execute()
        {

            var game = LoadGame(String.Format("{0}\\saves\\{1}.sav", Directory.GetCurrentDirectory(), _fileToLoad));

            _player.SendMessage(String.Format("Successfully loaded {0} ", _fileToLoad));

            // We need to set the RoomState for the player.



        }

        //@ToDO: I have this planned out, for loading and saving, I just need to double check how I am saving game objects and creating definitions for loading the class types back. (I have sample for how i plan on doing this in general) and a test project for doing it.
        //@ToDO: I would also like to Encrypt the file. Possibly doing HMAC-SHA1 or something.... to prevent hacking and cleartext passwords.
        public IGame LoadGame(string filename)
        {
            var game = new Game();

            using(var br = new BinaryReader(File.Open(filename,FileMode.Open)))
            {
                var gameObjects = new List<IGameObject>();

                var gameObjectCount = br.ReadInt32();

                for(var i =0; i<gameObjectCount; i++)
                {
                    var type = br.ReadInt32();
                    var id = new Guid(br.ReadBytes(16));
                }
            }

            return game;
        }
    }
}