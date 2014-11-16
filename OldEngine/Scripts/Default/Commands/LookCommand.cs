using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Mobs;
using MudDesigner.Scripts.Default.States;

namespace MudDesigner.Scripts.Default.Commands
{
    public class LookCommand : ICommand
    {
        public void Execute(IPlayer player)
        {
            player.SwitchState(new LookingState());
        }
    }
}
