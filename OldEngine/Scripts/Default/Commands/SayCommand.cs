using System.Collections.Generic;
using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Scripts.Default.Commands
{
    public class SayCommand : ICommand
    {
        public void Execute(IPlayer player)
        {
            var message = string.Empty;
            if (player.ReceivedInput.ToLower().StartsWith("say"))
            {
                message = player.ReceivedInput.Substring(3).TrimStart();

                string correctedMessage = string.Format("{0} says '{1}'", player.Name, message);

                player.Location.BroadcastMessage(correctedMessage, new List<IPlayer>() { player });

                correctedMessage = string.Format("You say '{0}'", message);
                player.SendMessage(correctedMessage);
            }
        }
    }
}
