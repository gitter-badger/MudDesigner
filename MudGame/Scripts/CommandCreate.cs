    public class CommandCreate : IGameCommand
    {
        public Boolean Override { get; set; }
        public String Name { get; set; }

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
            player.Send("Selection: ", false);
            Int32 selection = Convert.ToInt32(player.ReadInput());

            //Fire off what ever Method we need to, according to the users input.
            switch (selection)
            {
                case 1:
                    CreateRealm(player);
                    break;
                case 2:
                    CreateZone(player);
                    break;
            }
        }

        //Creates a Realm.
        public void CreateRealm(BaseCharacter player)
        {
            //Instance a new Realm.
            Realm realm = new Realm(player.ActiveGame);

            //Get the name of this Realm from the player.
            player.Send("Realm Name: ", false);
            realm.Name = player.ReadInput();

            //Check if a Realm with this name already exists.
            foreach (Realm r in player.ActiveGame.World.RealmCollection)
            {
                if (r.Name == realm.Name)
                {
                    player.Send("Realm already exists!");
                }
            }

            player.ActiveGame.World.AddRealm(realm);
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

                input = player.ReadInput();

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
            realm.AddZone(zone);

            Boolean isValidZone = false;

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

            player.Send(zone.Name + " has been created and added to Realm " + realm.Name + ".");
        }
    }