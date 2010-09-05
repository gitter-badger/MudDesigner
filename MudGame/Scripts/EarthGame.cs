/// <summary>
/// The EarthGame script extends from the Mud Designer Game Engine internal Game class.
/// In order for the script to be used as the new default Game class within the engine, it MUST
/// inherit from Game by using a colon followed by Game after the script name. 
/// 
/// Ex: EarthGame : Game
/// 
/// This allows developers to extend on or modify how the Game will act during runtime.
/// </summary>
public class EarthGame : Game
{
    //Our custom California Realm script.
    public WorldCalifornia Cali;

    /// <summary>
    /// The Constructor for the Game.
    /// If you wish to have the internal engine Game class execute it's constructor code before your code
    /// is called, you must call the base class by using a colon followed by base() on the same line as your constructor.
    /// 
    /// Ex: EarthGame() : base()
    /// 
    /// This will ensure that all of the engine properties are setup correctly, incase you miss anything. 
    /// Any code placed within your constructor is executed after the internal Game class constructor is executed,
    /// allowing you to override of the engines properties and set things up how your game needs things set.
    /// </summary>
    public EarthGame() : base()
    {
        //The following are Properties this script inherits from the internal Game class.
        GameTitle = "Planet Earth MUD";
        Story = "The planet Earth reproduced in a MUD for your playing enjoyment!";
        IsMultiplayer = true;

        CompanyName = "Mud Designer Team";
        Website = "Visit Http://MudDesigner.Codeplex.com for the latest News, Documentation and Releases.";
        Version = "Example Game Version 1.0";

        //Maximum number of players allowed to play on the server at any given time.
        MaximumPlayers = 1000;

        //Create a new instance of our California realm script, we must pass a reference to our EarthGame
        //to the script so that it may add the Realm to our Game world. That is done by using the 'this' keyword.
        //Cali = new WorldCalifornia(this);

        //Calling the create method within the california script.
        //Cali.Create();
    }
}