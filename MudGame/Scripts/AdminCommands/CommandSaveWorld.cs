    public class CommandSaveWorld : BaseCommand
    {
        public CommandSaveWorld()
        {
            Help.Add("Saves the game world.");
        }

        public override void Execute(String command, BaseCharacter player)
        {
            if ((player.Role == SecurityRoles.Admin) || (player.Role == SecurityRoles.GM))
            {
                player.ActiveGame.Save();
            }
        }
    }
}
