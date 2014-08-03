using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.States;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Directors;

namespace MudDesigner.Scripts.Default.States
{
    public class EnteringCommandState : IState
    {
        IPlayer currentPlayer;
        IServerDirector director;

        public void Render(IPlayer player)
        {
            currentPlayer = player;
            director = player.Director;
            currentPlayer.SendMessage("Command: ", false);
        }

        public ICommand GetCommand()
        {
            Type[] gameCommands = ScriptFactory.GetTypesWithInterface("ICommand");
            var input = currentPlayer.ReceiveInput().ToLower();
            string[] args = input.Split(' ');

            if (args.Length >= 1)
                input = args[0];

            if (string.IsNullOrEmpty(input))
                return new InvalidCommand();

            foreach (Type command in gameCommands)
            {
                string correctedCommand = command.Name.ToLower();

                if (correctedCommand.StartsWith("command"))
                    correctedCommand = correctedCommand.Substring("command".Length);

                if (correctedCommand.EndsWith("command"))
                    correctedCommand = correctedCommand.Substring(0, correctedCommand.Length - "command".Length);

                if (correctedCommand == input)
                {
                    var commandToExecute = ScriptFactory.GetScript(command.FullName);

                    if (commandToExecute != null)
                        return (ICommand)commandToExecute;
                }
            }

            return new InvalidCommand();
        }
    }
}
