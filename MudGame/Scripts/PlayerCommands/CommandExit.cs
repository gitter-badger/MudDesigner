/// <summary>
/// The Exit command is used to exit the MUD game.
/// Using this command while connected to a MUD server will perform a disconnect from the server.
/// Using the command while running the game in offline mode will simply shut down the game.
/// </summary>
public class CommandExit : BaseCommand
{
    /// <summary>
    /// Constructor for the class.
    /// </summary>
    public CommandExit()
    {
        Help.Add("Exits the game."); 
    }

    /// <summary>
    /// Executes the command.
    /// This method is called from the Command Engine, it is not recommended that you call this method directly.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="player"></param>
    public void Execute(String command, BaseCharacter player)
    {
        //Check if the game is multiplayer. 
        //Multiplayer games require disconnecting from the server and letting other players in the same Room know
        //that this player has left.
        if (player.ActiveGame.IsMultiplayer)
        {
            //Query the Active Games Player collection so that we can build a collection of Players that need to be
            //informed of the Player disconnecting from the Server.
            var playerQuery =
                from p in player.ActiveGame.GetPlayerCollection()
                where !p.Name.StartsWith("New") && p.Name != player.Name && p.CurrentWorldLocation == player.CurrentWorldLocation
                select p;

            //Inform each player found in our LINQ query that the player has disconnected from the Server.
            foreach (BaseCharacter p in playerQuery)
                p.Send(player.Name + " has left."); ;

            //TODO: If a player is in a Group then s/he needs to be removed upon disconnecting.
            player.Disconnect();
        }
        else
        {
            //Call the game's shutdown method which will save all objects and exit the game gracefully.
            player.ActiveGame.Shutdown();
        }
    }
}