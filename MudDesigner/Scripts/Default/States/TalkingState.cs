using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.States;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Scripting;

namespace MudDesigner.Scripts.Default.States
{
    public class TalkingState : IState
    {
        IPlayer currentPlayer;

        public void Render(IPlayer player)
        {
            currentPlayer = player;

            string input = player.ReceivedInput;
            if (player.ReceivedInput.ToLower().StartsWith("say"))
                input = player.ReceivedInput.Substring(3).TrimStart(new char[] { ' ' });

            if (String.IsNullOrEmpty(input))
            {
                player.SendMessage("You didn't provide any message content.");
                return;
            }

            foreach (IPlayer p in player.Location.Occupants)
            {
                if (player.CanTalk && p != player)
                {
                    p.SendMessage(player.Name + " says '" + input + "'");
                }
            }

            if (player.CanTalk)
                player.SendMessage("You say '" + input + "'");
        }

        public ICommand GetCommand()
        {
            currentPlayer.SwitchState(new EnteringCommandState());
            return NoOpCommand();
        }
    }
}
