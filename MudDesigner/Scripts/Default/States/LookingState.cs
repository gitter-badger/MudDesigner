using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.States;

namespace MudDesigner.Scripts.Default.States
{
    public class LookingState : IState
    {
        IPlayer currentPlayer;

        public void Render(IPlayer player)
        {
            currentPlayer = player;

            if (!player.Director.Server.Game.HideRoomNames)
                player.SendMessage(player.Location.Name);

            player.SendMessage(player.Location.Description);

            foreach(IDoor door in player.Location.GetDoorways())
            {
                player.SendMessage(door.FacingDirection.ToString() + ": " + door.Arrival.Name);
            }

            if (player.Location.Occupants.Count == 1 && player.Location.Occupants[0] != player)
            {
                
                player.SendMessage(player.Location.Occupants[0] + " is here.");
            }

            else if (player.Location.Occupants.Count > 1)
            {
                foreach (IPlayer occupant in player.Location.Occupants)
                {
                    if (occupant != player)
                        player.SendMessage(occupant.Name, false);
                }
                player.SendMessage(" is here.");
            }
        }

        public ICommand GetCommand()
        {
            currentPlayer.SwitchState(new EnteringCommandState());
            return new NoOpCommand();
        }
    }
}
