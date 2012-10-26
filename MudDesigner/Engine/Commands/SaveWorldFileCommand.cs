
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
            SaveGame();
        }

        public void SaveGame()
        {
            var game = _game as Game;
            
            if (game != null) 
                game.Save();
                 
        }
    }
}