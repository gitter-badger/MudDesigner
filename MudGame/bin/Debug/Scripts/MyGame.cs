public class MyGame : Game
{
       public California Cali;

    public MyGame()
        : base()
 {
     GameTitle = "Mud Designer Example Game";
     IsMultiplayer = true;

     CompanyName = "Mud Designer Team";
     Website = "Visit Http://MudDesigner.Codeplex.com for the latest News, Documentation and Releases.";
     Version = "Example Game Version 1.0";
     MaximumPlayers = 5000;

     Cali = new California(this);
     Cali.Create();
 }
}