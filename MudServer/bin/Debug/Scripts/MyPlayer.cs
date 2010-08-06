public class MyPlayer : BaseCharacter
{
    //Example use of custom properties
    public string GuildName { get; set; }

    //Player constructor. Passes the game parameter off to the parent class BaseCharacter.
    public MyPlayer(Game game) : base(game)
    {
        GuildName = "MUD Guild";
    }


    public override void Save(string filename)
    {
        Log.Write("Saving custom player...");
        //Don't save if the file name doesn't exist!
        if (String.IsNullOrEmpty(filename))
            return;

        Log.Write("Saving base player...");
        //Save all of the parent properties such as character name first.
        base.Save(filename);

        Log.Write("Saving custom content...");
        //Write our GuildName out to the player save file.
        FileManager.WriteLine(filename, GuildName, "GuildName");
    }
}