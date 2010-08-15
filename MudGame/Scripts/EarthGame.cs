public class EarthGame : Game
{
       public WorldCalifornia Cali;

    public EarthGame()
        : base()
 {
     GameTitle = "Planet Earth MUD";
     Story = "The planet Earth reproduced in a MUD for your playing enjoyment!";
     IsMultiplayer = true;

     CompanyName = "Mud Designer Team";
     Website = "Visit Http://MudDesigner.Codeplex.com for the latest News, Documentation and Releases.";
     Version = "Example Game Version 1.0";
     MaximumPlayers = 5000;

     Cali = new WorldCalifornia(this);
     Cali.Create();
 }
}