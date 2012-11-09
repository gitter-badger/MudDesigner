using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Scripts.Default.Commands
{
    public class LookCommand : ICommand
    {
        private IPlayer currentPlayer;

        public LookCommand(IPlayer player)
        {
            currentPlayer = player;
        }

        public void Execute()
        {
            IRoom location = currentPlayer.Location;

            currentPlayer.SendMessage(location.Name);
            currentPlayer.SendMessage(location.Description);
        }
    }
}
