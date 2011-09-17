using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

using MudEngine.FileSystem;
using MudEngine.GameObjects;
using MudEngine.GameObjects.Characters;
using MudEngine.GameManagement;
using MudEngine.Commands;
using MudEngine.GameObjects.Environment;

namespace MudEngine.Commands
{
    public class CommandEditRoom : BaseCommand
    {
        public CommandEditRoom()
        {
            Help.Add("Use the Edit command to edit existing objects properties.");
            Help.Add("Usage: EditRoom Realm>Room>Room");
            Help.Add("Example: EditRoom MyRealmName>MyRoom>MyRoom");
        }

        public override void Execute(String command, BaseCharacter player)
        {
            if ((player.Role == SecurityRoles.Admin) || (player.Role == SecurityRoles.GM))
            {
                //Get the admin-entered room filename
                String[] tokens;
                String filename;

                //Other scripts calling this script typically don't supply 'EditRoom' within the command argument
                if (command.ToLower().StartsWith("editroom"))
                    tokens = command.Substring("EditRoom".Length).Trim().Split('>');
                else
                    tokens = command.Trim().Split('>');

                if (tokens.Length == 0)
                {
                    player.Send("Room Editing canceled. No Room name was supplied.");
                    return;
                }
                else if (tokens.Length == 1)
                {
                    filename = tokens[0];
                }
                else if (tokens.Length <= 2)
                {
                    player.Send("Room Editing canceled. You must use the Rooms fully qualified path.");
                    IGameCommand gc = CommandEngine.GetCommand("CommandHelp");
                    gc.Execute("Help EditRoom", player);
                    return;
                }
                else
                    filename = tokens[2]; //Room filename is the 3rd index in the array.

                //Raise the scope of the player reference to class level instead of method level.
                this.player = player;

                if (tokens.Length == 3)
                {
                    try
                    {
                        room = player.ActiveGame.World.GetRealm(tokens[0]).GetZone(tokens[1])[0].GetRoom(filename)[0];
                    }
                    catch
                    {
                        player.Send("Room Editing canceled. The supplied path does not exist. Did you type the correct names in?");
                    }
                }
                else
                {
                    try
                    {
                        room = player.ActiveGame.World.GetRealm(player.CurrentRoom.Realm).GetZone(player.CurrentRoom.Zone)[0].GetRoom(filename)[0];
                    }
                    catch
                    {
                        player.Send("Room Editing canceled. The supplied Room does not exist.");
                        return;
                    }

                }
                //If no Room was retrieved (due to it not existing), let the admin know
                //that the Room filename was not valid.
                if (room == null)
                {
                    player.Send("Room Editing canceled. The supplied Room name is not valid.");
                    return;
                }
                //Otherwise, the Room does exist and was retrieved.
                //Lets build our Editing menu's and allow for Room Editing.
                else
                {
                    isEditing = true;

                    while (isEditing)
                    {
                        //Construct the main editing menu.
                        BuildMenuMain();

                        //Find out what menu option the admin wants to use.
                        Int32 value = 0;
                        //Attempt to convert the String entered by the admin into a numeric value
                        try
                        {
                            value = Convert.ToInt32(player.ReadInput());
                        }
                        //If a non-numeric value is supplied, the conversion failed. This is us catching that failure.
                        catch
                        {
                            player.Send("Room Editing canceled. The supplied value was not numeric!");
                            return;
                        }

                        //Parse the menu option that the admin supplied.
                        ParseMenuSelection(value);
                    }
                    //let the admin know that we have now exited the editor.
                    player.Send("Editing completed.");
                }
            }
        }

        /// <summary>
        /// Constructs the Main Editing menu for admins to look it.
        /// </summary>
        private void BuildMenuMain()
        {
            player.FlushConsole();
            player.Send(Path.GetFileNameWithoutExtension(room.Filename));
            player.Send("Select from the available options below:");
            player.Send("1: Descriptions");
            player.Send("2: Names");
            player.Send("3: Senses");
            player.Send("4: Initial Room");
            //player.Send("5: Settings");
            player.Send("6: Doorways");
            player.Send("9: Exit");
            player.Send("Enter numeric selection: ", false);
        }

        /// <summary>
        /// Constructs the Descriptions Editing menu for admins to look at.
        /// </summary>
        private void BuildMenuDescriptions()
        {
            player.FlushConsole();
            player.Send(Path.GetFileNameWithoutExtension(room.Filename));
            player.Send("Select from the available options below:");
            player.Send("1: Simple Description");
            player.Send("2: Detailed Descriptions");
            player.Send("9: Exit");
            player.Send("Enter a numeric selection: ", false);
        }

        /// <summary>
        /// Constructs the Name Editing menu for admins to look at.
        /// </summary>
        private void BuildMenuNames()
        {
            player.FlushConsole();
            player.Send(Path.GetFileNameWithoutExtension(room.Filename));
            player.Send("When you assign a Room Name, the Filename is overwrote with your RoomName as a filename.");
            player.Send("Example: RoomName of 'ExampleRoom' would automatically set a Filename of 'ExampleRoom.Room'");
            player.Send("");

            player.Send("If you wish to have multiple Rooms with the same visible name, you will need to specify a different Filename for each Room.");
            player.Send("Filenames are what you use when accessing objects as a Admin. Typically without the file extension.");
            player.Send("Example: A Room with a Visible name of \"My Test Room\" can have a filename of \"Test.Room\". You would access this object as a Admin by specifying a object name of \"Test\"");
            player.Send("Select from the available options below:");
            player.Send("");

            player.Send("1: Room Visibile Name");
            player.Send("2: Room Filename");
            player.Send("9: Exit");
            player.Send("Enter numeric selection: ", false);
        }

        private void BuildMenuSenses()
        {
            player.FlushConsole();
            player.Send(Path.GetFileName(room.Filename));
            player.Send("Senses are what allow the players to get a better understanding of the environment around them.");
            player.Send("Choose what sense you would like to edit, make your adjustments and press 'ENTER' to save the changes.");
            player.Send("Senses defined for Rooms will override any senses defined for it's parent Zone or Realm.");
            player.Send("Select from the available options below:");
            player.Send("1: Feel Sense");
            player.Send("2: Listen Sense");
            player.Send("3: Smell Sense");
            player.Send("9: Exit");
        }

        private void BuildMenuInitial()
        {
            player.FlushConsole();
            player.Send(room.Name);
            player.Send("Initial Room Settings.");
            player.Send("The Initial Room setting determins if the Room will be the starting location for all newly created players or not.");
            if (room.IsInitialRoom)
            {
                player.Send("If you disable this Room from being the Initial Room, new players will not have a starting location assigned to them.");
                player.Send("You will need to enable Initial Room on another Room in order for new players to have a starting location.");
                player.Send("Select from the available options below:");
                player.Send("1: Disable Initial Room");
            }
            else
            {
                player.Send("If you enable Initial Room, then new players will start at this location from now on.");
                player.Send("Select from the available options below:");
                player.Send("1: Enable Initial Room");
            }
            player.Send("9: Exit");
            player.Send("Enter numeric selection: ", false);
        }

        private void BuildMenuDoorways()
        {
            player.FlushConsole();

            player.Send(room.Name);
            player.Send("Doorway setup.");
            player.Send("You may create and edit doorways from the options available below.");
            player.Send("Doorways allow you to link two rooms together, giving characters the ability to traverse your game world.");
            player.Send("Please select from the available options below:");
            player.Send("1: Create a doorway.");
            if (room.Doorways.Count != 0)
                player.Send("2: Edit a doorway.");
            player.Send("9: Exit");
            player.Send("Enter a numeric selection: ", false);
        }

        /// <summary>
        /// This method parses the Admin input based off the main editing menu
        /// and sends the admin to what-ever sub-menu method we need to.
        /// </summary>
        /// <param name="value"></param>
        private void ParseMenuSelection(Int32 value)
        {
            Int32 entry = 0;

            switch (value)
            {
                case 1: //Descriptions
                    //build the menu and parse their menu selections.
                    BuildMenuDescriptions();
                    try
                    {
                        entry = Convert.ToInt32(player.ReadInput());
                    }
                    catch
                    {
                        player.Send("Room Editing canceled. The supplied value was not numeric!");
                        return;
                    }

                    ParseDescriptionSelection(entry);
                    break;
                case 2: //Names
                    //build the menu and parse their menu selections.
                    BuildMenuNames();
                    try
                    {
                        entry = Convert.ToInt32(player.ReadInput());
                    }
                    catch
                    {
                        player.Send("Room Editing canceled. The supplied value was not numeric!");
                        return;
                    }

                    ParseNameSelection(entry);
                    break;
                case 3://Senses
                    BuildMenuSenses();

                    try
                    {
                        entry = Convert.ToInt32(player.ReadInput());
                    }
                    catch
                    {
                        player.Send("Realm Editing Canceled. The supplied value was not numeric!");
                        return;
                    }

                    ParseSensesSelection(entry);
                    break;
                case 4: //Initial Realm
                    //build the menu and parse their menu selections
                    BuildMenuInitial();
                    try
                    {
                        entry = Convert.ToInt32(player.ReadInput());
                    }
                    catch
                    {
                        player.Send("Room Editing canceled. The supplied value was not numeric!");
                        return;
                    }

                    ParseInitialSelection(entry);
                    break;
                case 5: //Settings
                    break;
                case 6: //Doorways
                    BuildMenuDoorways();

                    try
                    {
                        entry = Convert.ToInt32(player.ReadInput());
                    }
                    catch
                    {
                        player.Send("Room Editing canceled. The supplied value was not numeric!");
                    }

                    ParseDoorwaySelection(entry);
                    break;
                case 9:
                    isEditing = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// This method parses the admins menu selection from the Description menu
        /// and adjusts the Rooms descriptions as specified by the admin
        /// </summary>
        /// <param name="value"></param>
        private void ParseDescriptionSelection(Int32 value)
        {
            String input = "";

            switch (value)
            {
                //Simple Description
                case 1:
                    player.FlushConsole();
                    player.Send("Enter a simple description for this Room.");
                    player.Send("Simple Descriptions are single line only.");
                    player.Send("To create a blank description, you may simply press ENTER.");
                    player.Send("To cancel editing this description, you may type 'Exit'");
                    if (!String.IsNullOrEmpty(room.Description))
                        player.Send("Current Description: " + room.Description);

                    player.Send("Entry: ", false);
                    //Read in the admins new simple description
                    input = player.ReadInput();

                    if (input.ToLower() == "exit")
                        return;
                    else
                        room.Description = input;

                    //Save the game world.
                    player.ActiveGame.Save();
                    player.Send("New Simple Description saved.");
                    break;
                case 2://Detailed Description
                    Boolean isEditing = true;
                    Int32 line = 1;

                    //Loop until the admin is finished entering his/her multi-line description.
                    while (isEditing)
                    {
                        player.FlushConsole();
                        line = 1; //reset our line to the first line, so we can re-print the content to the admin.

                        //print some help info to the admin
                        player.Send("Enter a Detailed Description for this Room.");
                        player.Send("Detailed Descriptions are multi-lined descriptions.");
                        player.Send("When you are finished entering a line, press ENTER to save it and move onto the next line.");
                        player.Send("When you are finished, you may type 'Exit' to save your changes and quit the Description editor.");
                        player.Send("To clear all detailed description lines, you may type 'Clear All'");
                        player.Send("To clear a single Detailed Description line, you may type 'Clear x', where x = line number to clear.");
                        player.Send("Example: 'Clear 2'");
                        player.Send("This will clear all contents from line number two in the detailed description.");
                        player.Send("If you make a mistake and need to edit a previously entered line, type 'Edit x' where x = line number you wish to edit.");
                        player.Send("An example would be 'Edit 3' which would allow you to change line #3.");
                        player.Send("");
                        player.Send("Detailed Description:");

                        //Loop through each description currently within the collection
                        //This will include lines created within this loop as well.
                        foreach (String desc in room.DetailedDescription)
                        {
                            player.Send(line.ToString() + ": " + desc);
                            line++;
                        }

                        //Print the current line to the user and read their new description for that line.
                        player.Send(line.ToString() + ": ", false);
                        input = player.ReadInput();

                        //Check what content the admin entered.
                        //If he/she typed 'exit' then we need to exit the editor.
                        if (input.ToLower() == "exit")
                            isEditing = false;
                        //If he/she typed 'edit' then we need to edit a supplied line number.
                        else if (input.ToLower().StartsWith("edit"))
                        {
                            //Retrieve the line number from the users input.
                            String editLine = input.Substring("edit".Length).Trim();

                            //If no line number was provided, cancel.
                            if (String.IsNullOrEmpty(editLine))
                            {
                                player.Send("Line editing canceled. You need to supply a line number.");
                            }
                            //Line number was provided, so lets skip to the specified line number
                            //and replace the contents of that line with whatever the admin enters now.
                            else
                            {
                                //convert the users specified line number from string to integer.
                                line = Convert.ToInt32(editLine);

                                //Make sure the line number specified isn't greater than the number
                                //of lines we currently have.
                                if (room.DetailedDescription.Count >= line)
                                {
                                    //Get the new description for this line...
                                    player.Send("New Description: ", false);
                                    input = player.ReadInput();
                                    //-1 correction due to collection index starting at 0 and not 1.
                                    //replace the existing description with the new one.
                                    room.DetailedDescription[line - 1] = input;
                                }
                                //Let the admin know that the line number specified does not exist.
                                else
                                {
                                    player.Send("Line Editing canceled. The Room does not contain that many Description lines.");
                                }
                            }
                        }
                        //If he/she typed 'clear' then we need to check if the admin wants to clear
                        //every single line of description or just a single line.
                        else if (input.ToLower().StartsWith("clear"))
                        {
                            //Find out what additional content is included with the clear input.
                            String clear = input.ToLower().Substring("clear".Length).Trim();

                            //if no additional values supplied with the clear command, we cancel.
                            if (String.IsNullOrEmpty(clear))
                            {
                                player.Send("Line Clearing canceled. You need to supply a line number or specify 'All' to clear all lines.");
                            }
                            //Admin specified to clear 'all' lines of the descrpition.
                            else if (clear == "all")
                            {
                                //Wipe out every line of description.
                                room.DetailedDescription.Clear();
                            }
                            //Admin specified a single line. Find the admins specified line number for clearing.
                            else
                            {
                                //Convert the specified line number to a integer
                                Int32 i = Convert.ToInt32(clear);

                                //make sure the line number provided does in-fact exist.
                                if (room.DetailedDescription.Count >= i)
                                    //Remove the specified line number for the descriptions collection.
                                    room.DetailedDescription.Remove(room.DetailedDescription[i - 1]);
                                //Line provided is larger than the number of lines available to check. Cancel.
                                else
                                    player.Send("Line Clearing canceled. The Room  does not contain that many description lines.");
                            }
                        }
                        //No special tokens provided, so we assume this line is a description. 
                        //Add the contents to the realm's detailed description collection.
                        else
                        {
                            room.DetailedDescription.Add(input);
                        }
                    }

                    //Loop is finished, so lets save the game world.
                    player.ActiveGame.Save();
                    player.Send("Detailed Description saved.");
                    break;
                case 9:
                    break;
            }
        }

        /// <summary>
        /// This method parses the values supplied by the admin from the Name editing menu.
        /// Allows for editing visible names and filenames
        /// </summary>
        /// <param name="value"></param>
        private void ParseNameSelection(Int32 value)
        {
            switch (value)
            {
                case 1: //Visible Name
                    player.FlushConsole();
                    player.Send("Enter a new Visible name for this Room.");
                    player.Send("Enter Value: ", false);

                    //Get the new name for this Room
                    String newName = player.ReadInput();

                    //Ensure the new name is not blank.
                    if (String.IsNullOrEmpty(newName))
                    {
                        player.Send("Room Name Editing canceled. No name supplied.");
                    }
                    //We have a valid name, so lets try assigning it to the Room
                    else
                    {
                        //Check to see if the supplied name already exists by checking for a existing Room
                        //that has a matching filename.
                        if (player.ActiveGame.World.GetRealm(room.Realm).GetZone(room.Zone)[0].GetRoom(newName + ".room").Count == 0)
                        {
                            //No matching Room was found, so we can go ahead and make the change.
                            //Store the new name within this Room
                            String oldName = room.Filename;
                            room.Name = newName;

                            //Update all of the objects that are within the Room to have the
                            //new name assigned to them.
                            UpdateRoomObjects(oldName);

                            //TODO: Any Items/NPC's etc within this Room will need to be updated as well.
                        }
                        else
                        {
                            player.Send("Room Name Editing canceled. " + newName + " already exists within the Zone.");
                            player.Send("If you want to keep the provided Visible Name for this Room, you must create a unique Filename.");
                            player.Send("Would you like to assign a unique Filename now?");
                            player.Send("1: Yes");
                            player.Send("2: No");
                            try
                            {
                                Int32 i = Convert.ToInt32(player.ReadInput());

                                switch (i)
                                {
                                    case 1:
                                        player.FlushConsole();
                                        player.Send("The default filename for this Room (using the new Visible Name) is currently:");
                                        player.Send(newName + ".Room");
                                        player.Send("Please supply a different filename:");
                                        player.Send("Enter Value: ", false);

                                        String filename = player.ReadInput();

                                        if (String.IsNullOrEmpty(filename))
                                        {
                                            player.Send("Room Name Editing Canceled. No filename supplied.");
                                        }
                                        else
                                        {
                                            if (player.ActiveGame.World.GetRealm(room.Realm).GetZone(filename)[0].GetRoom(filename)[0] != null)
                                            {
                                                player.Send("Room Name Editing Canceled. A Room with this filename already exists!");
                                            }
                                            else
                                            {
                                                String oldName = room.Filename;
                                                room.Name = newName;
                                                room.Filename = filename;

                                                UpdateRoomObjects(oldName);
                                            }
                                        }
                                        break;
                                    case 2:
                                        player.Send("Room Name Editing Canceled. A Room with this filename already exists!");
                                        break;
                                }
                            }
                            catch
                            {
                                player.Send("Room Name Editing canceled. The supplied value was not numeric.");
                            }
                        }
                    }
                    player.ActiveGame.Save();
                    break;
                case 2: //Room Filename
                    player.FlushConsole();
                    player.Send("Enter a new Filenamename for this Room.");
                    player.Send("Enter Value: ", false);

                    String fname = player.ReadInput();

                    if (String.IsNullOrEmpty(fname))
                    {
                        player.Send("Room Name Editing canceled. No name supplied.");
                    }
                    else if (player.ActiveGame.World.GetRealm(room.Realm).GetZone(room.Zone)[0].GetRoom(fname).Count != 0)
                    {
                        player.Send("Room Name Editing canceled. A Room with this filename already exists!");
                    }
                    else
                    {
                        String oldName = "";
                        oldName = room.Filename;
                        room.Filename = fname;
                        UpdateRoomObjects(oldName);
                        player.ActiveGame.Save();
                    }
                    break;
                case 9: //Exit
                    return;
            }
        }

        private void ParseSensesSelection(Int32 value)
        {
            player.FlushConsole();

            switch (value)
            {
                case 1: //Feel
                    player.Send("Enter the new default FEEL description for this Room.");
                    player.Send("If you wish to clear the current description, just press ENTER to save a blank description.");

                    if (!String.IsNullOrEmpty(room.Feel))
                    {
                        player.Send("Current Value: ", false);
                        player.Send(room.Feel);
                    }

                    player.Send("Enter Value: ", false);
                    room.Feel = player.ReadInput();
                    break;
                case 2: //Listen
                    player.Send("Enter the new default LISTEN description for this Room.");
                    player.Send("If you wish to clear the current description, just press ENTER to save a blank description.");

                    if (!String.IsNullOrEmpty(room.Listen))
                    {
                        player.Send("Current Value: ", false);
                        player.Send(room.Listen);
                    }

                    player.Send("Enter value: ", false);
                    room.Listen = player.ReadInput();
                    break;
                case 3: //Smell
                    player.Send("Enter the new default SMELL description for this Room.");
                    player.Send("If you wish to clear the current description, just press ENTER to save a blank description.");

                    if (!String.IsNullOrEmpty(room.Smell))
                    {
                        player.Send("Current Value: ", false);
                        player.Send(room.Smell);
                    }

                    player.Send("Enter value: ", false);
                    room.Smell = player.ReadInput();
                    break;
                case 9: //Exit
                    return;
            }
        }

        private void ParseInitialSelection(Int32 value)
        {
            switch (value)
            {
                case 1: //Enable/Disable Initial Realm
                    if (player.ActiveGame.InitialRealm == null)
                    {
                        player.Send("Room Initial editing canceled. You must have a Initial Realm defined before setting a Initial Room.");
                        return;
                    }

                    if (player.ActiveGame.InitialRealm.InitialZone == null)
                    {
                        player.Send("Room Initial editing canceled. You must have a Initial Zone before setting a Initial Room.");
                        return;
                    }

                    if ((player.ActiveGame.InitialRealm.Filename == room.Realm) && (player.ActiveGame.InitialRealm.InitialZone.Filename == room.Zone))
                    {
                        if (room.IsInitialRoom)
                        {
                            room.IsInitialRoom = false;
                            player.ActiveGame.InitialRealm.InitialZone.InitialRoom = null;
                        }
                        else
                        {
                            room.IsInitialRoom = true;
                            player.ActiveGame.InitialRealm.InitialZone.InitialRoom = room;
                        }
                    }
                    else
                    {
                        player.Send("Room Initial editing canceled. You cannot define this Room as initial if it's parent Zone or Realm is not set as Initial.");
                    }
                    break;
                case 9: //Exit
                    return;
            }

            player.ActiveGame.Save();
        }

        private void ParseDoorwaySelection(Int32 value)
        {
            Int32 input = 0;

            switch (value)
            {
                case 1: //Create doorway
                    player.FlushConsole();
                    player.Send("Link Editor:");
                    player.Send("If you choose to link to a non-existant Room, the Room will be created for you during the link process.");
                    player.Send("Please select from one of the available options below:");
                    player.Send("1: Link to a non-existant Room.");
                    player.Send("2: Link to a existing Room.");

                    try
                    {
                        input = Convert.ToInt32(player.ReadInput());
                    }
                    catch
                    {
                        player.Send("Room Editing Canceled. Supplied value was not numeric!");
                        return;
                    }
                    switch (input)
                    {
                        case 1:
                            LinkNewRoom();
                            break;
                        case 2:
                            LinkExistingRoom();
                            break;
                    }
                    break;
                case 2: //Edit doorway
                    break;
            }
        }

        private void LinkNewRoom()
        {
            player.FlushConsole();
            player.Send("Please enter the name for the new Room.");
            player.Send("If you want to link to a different Realm or Zone, please supply their full path.");
            player.Send("Example: MyRealm>MyZone>NewRoomName");
            player.Send("If you supply only a Room name, then the Room will be created within the same Realm/Zone as the current Room being edited.");
            player.Send("Enter Name: ", false);

            Room r = new Room(player.ActiveGame);
            String roomName = player.ReadInput();

            if (String.IsNullOrEmpty(roomName))
            {
                player.Send("Invalid name supplied!");
                return;
            }

            r.Name = roomName;
            player.ActiveGame.World.GetRealm(player.CurrentRoom.Realm).GetZone(player.CurrentRoom.Zone)[0].AddRoom(r);

            player.FlushConsole();
            player.Send("Please select the direction you want to travel from " + room.Name.ToUpper());
            Array directions = Enum.GetValues(typeof(AvailableTravelDirections));
            Int32 number = 1;
            foreach (Int32 value in directions)
            {
                String direction = Enum.GetName(typeof(AvailableTravelDirections), value);

                if (direction == "None")
                    continue;

                Boolean isUsed = false;

                foreach (Door door in room.Doorways)
                {
                    if (door.TravelDirection == (AvailableTravelDirections)Enum.Parse(typeof(AvailableTravelDirections), direction))
                    {
                        isUsed = true;
                    }
                }

                if (isUsed)
                    continue;

                player.Send(number + ": " + direction);
                number++;
            }

            //TODO Get input for travel direction and link the rooms via Zone.LinkRooms()
            player.Send("Enter numeric selection: ", false);

            Int32 input = 0;

            try
            {
                input = Convert.ToInt32(player.ReadInput());
            }
            catch
            {
                player.Send("Room Linking canceled! You must supply a valid numeric selection.");
                return;
            }
            AvailableTravelDirections d = AvailableTravelDirections.None;
            String displayName = Enum.GetName(typeof(AvailableTravelDirections), input + room.Doorways.Count);

            if ((String.IsNullOrEmpty(displayName)) || (displayName == "None"))
            {
                player.Send("Invalid direction selected. Linking canceled.");
                return;
            }

            d = (AvailableTravelDirections)Enum.Parse(typeof(AvailableTravelDirections), displayName);
            Zone z = new Zone(player.ActiveGame);
            z = player.ActiveGame.World.GetRealm(room.Realm).GetZone(room.Zone)[0];
            z.LinkRooms(d, r, room);
            //z.AddRoom(r);
            player.FlushConsole();
            player.Send("Would you like to edit the properties for the newly created " + r.Name + " room?");
            player.Send("Available Selections:");
            player.Send("1: Yes");
            player.Send("2: No");
            player.Send("Enter selection: ", false);

            try
            {
                input = Convert.ToInt32(player.ReadInput());
            }
            catch
            {
                player.Send("Invalid selection!");
                player.Send(r.Name + " created and linked to " + room.Name + ", however no properties for " + r.Name + " were set. Please use EditRoom " + r.Realm + ">" + r.Zone + ">" + r.Name + " to edit them.");
                return;
            }

            if (input == 1)
            {
                IGameCommand gc = CommandEngine.GetCommand("CommandEditRoom");
                gc.Execute(r.RoomLocationWithoutExtension, player);
                isEditing = true; //Previous EditRoom command sets this to False if the admin exits the editing of the new Room.
            }
            player.Send(r.Name + " created and linked to " + room.Name);
        }

        private void LinkExistingRoom()
        {
            player.FlushConsole();
            player.Send("Please enter the name for the existing Room.");
            player.Send("If you want to link to a different Realm or Zone, please supply their full path.");
            player.Send("Example: MyRealm>MyZone>NewRoomName");
            player.Send("If you supply only a Room name, then the Room will be created within the same Realm/Zone as the current Room being edited.");
            player.Send("Enter Name: ", false);

            Room r = new Room(player.ActiveGame);
            String roomName = player.ReadInput();

            if (String.IsNullOrEmpty(roomName))
            {
                player.Send("Invalid name supplied!");
                return;
            }

            r.Name = roomName;
            player.ActiveGame.World.GetRealm(player.CurrentRoom.Realm).GetZone(player.CurrentRoom.Zone)[0].AddRoom(r);

            player.FlushConsole();
            player.Send("Please select the direction you want to travel from " + room.Name.ToUpper());
            Array directions = Enum.GetValues(typeof(AvailableTravelDirections));
            Int32 number = 1;
            foreach (Int32 value in directions)
            {
                String direction = Enum.GetName(typeof(AvailableTravelDirections), value);

                if (direction == "None")
                    continue;

                Boolean isUsed = false;

                foreach (Door door in room.Doorways)
                {
                    if (door.TravelDirection == (AvailableTravelDirections)Enum.Parse(typeof(AvailableTravelDirections), direction))
                    {
                        isUsed = true;
                    }
                }

                if (isUsed)
                    continue;

                player.Send(number + ": " + direction);
                number++;
            }

            //TODO Get input for travel direction and link the rooms via Zone.LinkRooms()
            player.Send("Enter numeric selection: ", false);

            Int32 input = 0;

            try
            {
                input = Convert.ToInt32(player.ReadInput());
            }
            catch
            {
                player.Send("Room Linking canceled! You must supply a valid numeric selection.");
                return;
            }
            AvailableTravelDirections d = AvailableTravelDirections.None;
            String displayName = Enum.GetName(typeof(AvailableTravelDirections), input + room.Doorways.Count);

            if ((String.IsNullOrEmpty(displayName)) || (displayName == "None"))
            {
                player.Send("Invalid direction selected. Linking canceled.");
                return;
            }

            d = (AvailableTravelDirections)Enum.Parse(typeof(AvailableTravelDirections), displayName);
            Zone z = new Zone(player.ActiveGame);
            z = player.ActiveGame.World.GetRealm(room.Realm).GetZone(room.Zone)[0];
            z.LinkRooms(d, r, room);
            //z.AddRoom(r);
            player.FlushConsole();
            player.Send("Would you like to edit the properties for the newly created " + r.Name + " room?");
            player.Send("Available Selections:");
            player.Send("1: Yes");
            player.Send("2: No");
            player.Send("Enter selection: ", false);

            try
            {
                input = Convert.ToInt32(player.ReadInput());
            }
            catch
            {
                player.Send("Invalid selection!");
                player.Send(r.Name + " created and linked to " + room.Name + ", however no properties for " + r.Name + " were set. Please use EditRoom " + r.Realm + ">" + r.Zone + ">" + r.Name + " to edit them.");
                return;
            }

            if (input == 1)
            {
                IGameCommand gc = CommandEngine.GetCommand("CommandEditRoom");
                gc.Execute(r.RoomLocationWithoutExtension, player);
                isEditing = true; //Previous EditRoom command sets this to False if the admin exits the editing of the new Room.
            }
            player.Send(r.Name + " created and linked to " + room.Name);
        }

        /// <summary>
        /// This method is used when the Rooms name or filename has changed.
        /// It updates all objects that refer to this Rooms filename/roomname
        /// </summary>
        /// <param name="oldName"></param>
        private void UpdateRoomObjects(String oldName)
        {
            //Check if this Room is the initial Room. If so then we need to update the
            //current Game so that when it launches, it checks the new Room and not the old
            //one.
            if ((room.IsInitialRoom) && (player.ActiveGame.InitialRealm.InitialZone.Filename == room.Zone))
            {
                player.ActiveGame.InitialRealm.InitialZone.InitialRoom = room;
            }

            //Loop through each player currently logged in and see if they are currently
            //within the selected Room. If they are then we need to change their current
            //Room to the new one.
            foreach (BaseCharacter p in player.ActiveGame.GetPlayerCollection())
            {
                if (p.Name.StartsWith("Base"))
                    continue;

                if (p.CurrentRoom.Filename == oldName)
                {
                    p.CurrentRoom = room;
                    p.Save();
                }
            }

            //Loop through every player on file to see if any are currently within
            //the selected Realm. If they are, we need to edit their files so that they
            //log-in next time into the correct Realm. Otherwise Admins will need to manually
            //edit the files and move the players. 
            //This is done after scanning logged in players, as logged in players will of had
            //their location already updated and files saved, preventing any need for us to
            //modify their files again.
            foreach (String file in Directory.GetFiles(player.ActiveGame.DataPaths.Players))
            {
                BaseCharacter ch = new BaseCharacter(player.ActiveGame);
                ch.Load(file);
                if (ch.CurrentRoom.Filename == oldName)
                {
                    ch.CurrentRoom = room;
                    ch.Save();
                }
            }

            //Next, we need to loop through every Room within this Room
            //and update their Rooms names to match the new one
            foreach (Realm r in player.ActiveGame.World.RealmCollection)
            {
                foreach (Zone z in r.ZoneCollection)
                {
                    if (z.InitialRoom.Filename == oldName)
                    {
                        z.InitialRoom = room;
                    }

                    foreach (Room rm in z.RoomCollection)
                    {
                        foreach (Door d in rm.Doorways)
                        {
                            if (d.ArrivalRoom.Filename == oldName)
                                d.ArrivalRoom = room;
                            if (d.DepartureRoom.Filename == oldName)
                                d.DepartureRoom = room;
                        }
                    }
                }
            }
        }
    }
}
