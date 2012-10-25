
using System.IO;

using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Commands
{
    public class SaveWorldFileCommand : ICommand
    {
        private readonly IGame _game;
        
        public SaveWorldFileCommand(IGame game)
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
            var eGame = game;
            if (eGame == null) 
                return;

            using (var bw = new BinaryWriter(File.Open(filename, FileMode.OpenOrCreate)))
            {
                bw.Write(eGame.GameObjects.Count);

                foreach (var gameobject in eGame.GameObjects.Values)
                {
                    bw.Write(gameobject.ID.ToByteArray());
            
                }
                
                foreach (var gameobject in eGame.GameObjects.Values)
                {
                    gameobject.Save(bw);
                }
            }
        }
    }
}