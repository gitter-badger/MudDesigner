using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.States;
using MudDesigner.Engine.Directors;

namespace MudDesigner.Scripts.Default.States
{
    public class LookingState : IState
    {
        IPlayer currentPlayer;
        IServerDirector director;

        public void Render(IPlayer player)
        {
            currentPlayer = player;
            director = player.Director;

            if (!player.Director.Server.Game.HideRoomNames)
                player.SendMessage(player.Location.Name);

            player.SendMessage(player.Location.Description);

            foreach (IDoor door in player.Location.GetDoorways())
            {
                player.SendMessage(door.FacingDirection.ToString() + ": " + door.Arrival.Name);
            }

            List<IPlayer> omit = new List<IPlayer>() { player };
            string message = string.Empty;
            if (player.Location.Occupants.Count == 2) //Only the player + 1 occupant, so we need a simpler message
            {
                foreach (IMob occupant in player.Location.Occupants)
                {
                    //We need it to say "Bob is here", but only to our player
                    if (occupant == player)
                        continue;

                    message = string.Format("{0} is here.", occupant.Name);
                    player.SendMessage(message);
                }
            }
            else if (player.Location.Occupants.Count > 2) //more than just the player and one other occupant.
            {
                // We need it to say "Bob, Sussie and Chris is here"
                foreach (IMob occupant in player.Location.Occupants)
                {
                    if (occupant == player)
                        continue;

                    if (occupant == player.Location.Occupants[player.Location.Occupants.Count - 1])
                        message += "and " + occupant.Name;
                    else
                        message += occupant.Name + ", ";
                }
                message += "is here.";
                player.SendMessage(message);
            }
        }

        public ICommand GetCommand()
        {
            currentPlayer.SwitchState(new EnteringCommandState());
            return new NoOpCommand();
        }
    }
}
