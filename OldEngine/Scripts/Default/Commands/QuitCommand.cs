using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Commands;

namespace MudDesigner.Scripts.Default.Commands
{
    public class QuitCommand : ICommand
    {

        public void Execute(IPlayer player)
        {
            player.Disconnect();
        }
    }
}
