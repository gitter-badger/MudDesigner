public class CommandClear : IGameCommand
{
    public Boolean Override { get; set; }
    public String Name { get; set; }

    public void Execute(String command, BaseCharacter player)
    {
        player.FlushConsole();
    }
}