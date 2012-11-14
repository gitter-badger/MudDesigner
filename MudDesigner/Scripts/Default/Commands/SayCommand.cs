using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Environment;
using MudDesigner.Scripts.Default.States;

namespace MudDesigner.Scripts.Default.Commands
{
    public class SayCommand : ICommand
    {
        public void Execute(IPlayer player)
        {
            if (String.IsNullOrEmpty(player.ReceivedInput))
                return;

           // player.SwitchState(new TalkingState());
        }
    }
}
