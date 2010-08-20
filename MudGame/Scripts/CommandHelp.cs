    public class CommandHelp : IGameCommand
    {
        public Boolean Override { get; set; }
        public String Name { get; set; }
        
        public List<String> Help { get; set; }

        public void Execute(String command, BaseCharacter player)
        {
            string topic = command.Substring("Help".Length);

            //TODO: Help command should display a complete list of available commands and should have self contained help topics.
            if (topic.Length == 0)
            {
                player.Send("Available commands: ", false);
                foreach (String cmd in CommandEngine.GetCommands())
                {
                    IGameCommand g = CommandEngine.GetCommand(cmd);
                    player.Send(CommandEngine.GetCommandName(g) + ", ", false);
                }
                player.Send("");
                player.Send("Usage: Help 'Command'");
                return;
            }
            else
                topic = topic.Trim();

            IGameCommand gc = CommandEngine.GetCommand("Command" + topic);

            foreach (String help in gc.Help)
            {
                player.Send(help);
            }
        }
    }