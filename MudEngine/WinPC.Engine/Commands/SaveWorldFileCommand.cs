
using System.IO;

using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Commands
{
    public class SaveWorldFileCommand : ICommand
    {
        private readonly IGame _game;
        
        public SaveWorldFileCommand(string fileToSave, IGame game)
        {
            
            _game = game;
        }

        public void Execute()
        {
            var fileAndPathToSave = Path.Combine(Directory.GetCurrentDirectory(),"saves", MudDesigner.Engine.Properties.Engine.Default.WorldFile);
            var path = Path.GetDirectoryName(fileAndPathToSave);

            if(path == null)
            {
                return;
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(string.Format(path));
            }

            SaveGame(fileAndPathToSave, _game);

        }

        public void SaveGame(string filename, IGame game)
        {
            //TODO - Shouldn't the engine pass an already instanced copy of IGame?
            //Otherwise we are relying on a hard-coded Game class.  Should be able to use IGame down below without any issues. - JS
            //var eGame = game as EngineGame;
            var eGame = game;
            if (eGame == null) 
                return;

            using (var bw = new BinaryWriter(File.Open(filename, FileMode.OpenOrCreate)))
            {
                bw.Write(eGame.GameObjects.Count);

                foreach (var gameobject in eGame.GameObjects.Values)
                {
                    bw.Write((int)gameobject.Type);
                    bw.Write(gameobject.Id.ToByteArray());
            
                }
                
                foreach (var gameobject in eGame.GameObjects.Values)
                {
                    gameobject.Save(bw);
                }
            }
        }
    }
}