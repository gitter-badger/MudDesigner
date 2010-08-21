public class CommandCreate : IGameCommand
{
    public Boolean Override { get; set; }
    public String Name { get; set; }
    public List<String> Help { get; set; }

    public CommandCreate()
    {
        Help = new List<string>();
        Help.Add("Allows for the creation of environment objects.");
        Help.Add("Linking of Rooms are not supported yet.");
        //Help.Add("In order to link Rooms together use the LinkRoom command.");
    }

    public void Execute(String command, BaseCharacter player)
    {
        if ((player.Role == SecurityRoles.Player) || (player.Role == SecurityRoles.NPC))
        {
            return; //Don't let them know this even exists.
        }

        //Build our create menu.
        player.Send("");
        player.Send("Welcome to " + player.ActiveGame.GameTitle + " World Creation Tool.");
        player.Send("What would you like to create?");
        player.Send("");
        player.Send("1: Realm");
        player.Send("2: Zone");
        player.Send("3: Room");
        player.Send("4: Exit Tool");
        player.Send("At point during creation, you may type 'Cancel' to exit with no changes saved.");
        player.Send("");
        player.Send("Selection: ", false);

        Int32 selection = 0;
        String input = player.ReadInput();

        //Allows for aborting the creation tool if the user wants too.
        if (input.ToLower() == "cancel")
        {
            player.Send("Creation aborted.");
            return;
        }

        try
        {
            selection = Convert.ToInt32(input);
        }
        catch (Exception)
        {
            Log.Write("Invalid selection!");
            player.Send("Invalid selection!");
            player.Send("Creation aborted.");
            return;
        }
        //Fire off what ever Method we need to, according to the users input.
        switch (selection)
        {
            case 1:
                CreateRealm(player);
                break;
            case 2:
                CreateZone(player);
                break;
            case 3:
                CreateRoom(player);
                break;
            case 4:
                return;
        }
    }

    //Creates a Realm.
    public void CreateRealm(BaseCharacter player)
    {
        //Instance a new Realm.
        Realm realm = new Realm(player.ActiveGame);
        Boolean isLegalName = false;

        while (!isLegalName)
        {
            isLegalName = true;
            //Get the name of this Realm from the player.
            player.Send("Realm Name: ", false);
            realm.Name = player.ReadInput();

            //Check for canceling
            if (realm.Name.ToLower() == "cancel")
            {
                player.Send("Creation aborted.");
                return;
            }

            //Check if a Realm with this name already exists.
            foreach (Realm r in player.ActiveGame.World.RealmCollection)
            {
                if (r.Name == realm.Name)
                {
                    player.Send("Realm already exists!");
                    isLegalName = false;
                }
            }
        }

        player.ActiveGame.World.AddObject(realm);
        Log.Write(player.Name + " has created a Realm called " + realm.Name);
        player.Send(realm.Name + " has been created and added to the world.");
    }

    public void CreateZone(BaseCharacter player)
    {
        player.Send("Select which Realm this Zone will belong to.");
        Boolean isValidRealm = false;
        String input = "";
        Realm realm = new Realm(player.ActiveGame);

        while (!isValidRealm)
        {
            isValidRealm = true;//Default to true, assume the user entered a valid name.
            foreach (Realm r in player.ActiveGame.World.RealmCollection)
            {
                player.Send(r.Filename + " | ", false);
            }

            player.Send("");
            player.Send("Selection: ", false);

            input = player.ReadInput();

            if (input.ToLower() == "cancel")
            {
                player.Send("Zone creation aborted.");
                return;
            }

            //Ensure it's a valid name, if not then loop back and try again.
            foreach (Realm r in player.ActiveGame.World.RealmCollection)
            {
                if (r.Filename.ToLower() == input.ToLower())
                {
                    isValidRealm = true;
                    realm = r;
                    break;
                }
                else
                {
                    isValidRealm = false;
                }
            }

            if (!isValidRealm)
                player.Send("That Realm does not exist! Please try again.");
        }

        Zone zone = new Zone(player.ActiveGame);
        //realm.AddZone(zone);

        Boolean isValidZone = false;
        player.Send(""); //blank line

        while (!isValidZone)
        {
            isValidZone = true; //assume the user will enter a correct value.
            player.Send("Enter a name for this Zone: ", false);
            String name = player.ReadInput();

            if (String.IsNullOrEmpty(name))
                continue;

            foreach (Zone z in realm.ZoneCollection)
            {
                if (z.Name == name)
                {
                    isValidZone = false;
                    break;
                }
            }

            if (isValidZone)
            {
                zone.Name = name;
            }
        }

        Log.Write(player.Name + " has created a Zone called " + zone.Name + " within the Realm " + realm.Name);
        player.Send(zone.Name + " has been created and added to Realm " + realm.Name + ".");
        realm.AddZone(zone);
    }

    public void CreateRoom(BaseCharacter player)
    {
        player.Send("Select which Realm this Zone will belong to.");
        Boolean isValidRealm = false;
        String input = "";
        Realm realm = new Realm(player.ActiveGame);

        while (!isValidRealm)
        {
            isValidRealm = true;//Default to true, assume the user entered a valid name.
            foreach (Realm r in player.ActiveGame.World.RealmCollection)
            {
                player.Send(r.Filename + " | ", false);
            }

            player.Send("");
            player.Send("Selection: ", false);

            input = player.ReadInput();

            if (input.ToLower() == "cancel")
            {
                player.Send("Zone creation aborted.");
                return;
            }

            //Ensure it's a valid name, if not then loop back and try again.
            foreach (Realm r in player.ActiveGame.World.RealmCollection)
            {
                if (r.Filename.ToLower() == input.ToLower())
                {
                    isValidRealm = true;
                    realm = r;
                    break;
                }
                else
                {
                    isValidRealm = false;
                }
            }

            if (!isValidRealm)
                player.Send("That Realm does not exist! Please try again.");
        }

        Zone zone = new Zone(player.ActiveGame);
        //realm.AddZone(zone);

        Boolean isValidZone = false;
        player.Send(""); //blank line

        while (!isValidZone)
        {
            isValidZone = true;//Default to true, assume the user entered a valid name.
            foreach (Zone z in realm.ZoneCollection)
            {
                player.Send(z.Filename + " | ", false);
            }

            player.Send("");
            player.Send("Selection: ", false);

            input = player.ReadInput();

            if (input.ToLower() == "cancel")
            {
                player.Send("Room creation aborted.");
                return;
            }

            //Ensure it's a valid name, if not then loop back and try again.
            foreach (Zone z in realm.ZoneCollection)
            {
                if (z.Filename.ToLower() == input.ToLower())
                {
                    isValidZone = true;
                    zone = z;
                    break;
                }
                else
                {
                    isValidZone = false;
                }
            }

            if (!isValidZone)
                player.Send("That Zone does not exist! Please try again.");
        }

        //Create the Room.
        Room room = new Room(player.ActiveGame);

        Boolean isValidRoom = false;
        player.Send(""); //blank line

        while (!isValidRoom)
        {
            isValidRoom = true; //assume the user will enter a correct value.
            player.Send("Enter a name for this Room: ", false);
            String name = player.ReadInput();

            if (String.IsNullOrEmpty(name))
                continue;

            foreach (Room r in zone.RoomCollection)
            {
                if (r.Name == name)
                {
                    isValidRoom = false;
                    break;
                }
            }

            if (isValidRoom)
            {
                room.Name = name;
            }
        }


        Log.Write(player.Name + " has created a Room called " + zone.Name + " within the Zone " + realm.Name + "->" + zone.Name);
        player.Send(room.Name + " has been created and added to " + realm.Name + "->" + zone.Name + ".");
        zone.AddRoom(room);
    }
}