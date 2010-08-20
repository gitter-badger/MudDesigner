public class CommandSay : IGameCommand
{
    public Boolean Override { get; set; }
    public String Name { get; set; }
    public List<String> Help { get; set; }
    public void Execute(String command, BaseCharacter player)
    {
        if (command.Length <= 4) //user only sent 'Say' or 'Say '
        {
            return; //nothing to say, don't say anything at all.
        }

        String message = command.Substring("Say ".Length);

        foreach (BaseCharacter p in player.ActiveGame.GetPlayerCollection())
        {
            if ((p.CurrentRoom.Realm == player.CurrentRoom.Realm) && (p.CurrentRoom.Zone == player.CurrentRoom.Zone) && (p.CurrentRoom.Filename == player.CurrentRoom.Filename))
            {
                p.Send(player.Name + " says: " + message);
            }
        }

        player.Send("You say: " + message);

    }
}