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
    public class CommandEditZone : BaseCommand
    {
        public CommandEditZone()
        {
            Help.Add("Use the Edit command to edit existing objects properties.");
            Help.Add("Usage: EditZone Realm>Zone");
            Help.Add("Example: EditZone MyRealmName>MyZone");
        }

        public void Execute(String command, BaseCharacter player)
        {
            if ((player.Role == SecurityRoles.Admin) || (player.Role == SecurityRoles.GM))
            {
                //Get the admin-entered zone filename
                String[] tokens = command.Substring("EditZone".Length).Trim().Split('>');


                if (tokens.Length == 0)
                {
                    player.Send("Zone Editing canceled. No Zone name was supplied.");
                    return;
                }
                else if (tokens.Length == 1)
                {
                    player.Send("Zone Editing canceled. Realm name supplied, but no Zone name.");
                    return;
                }

                String filename = tokens[1];

                //Raise the scope of the player reference to class level instead of method level.
                this.player = player;

                //We have a filename, retrieve the Zone the admin wants to edit.
                if ((player.ActiveGame.World.GetRealm(tokens[0]) == null) && (player.ActiveGame.World.GetRealm(tokens[0]).GetZone(filename).Count == 0))
                {
                    player.Send("Zone editing canceled. Zones owning Realm or the Zone itself does not exists.");
                    return;
                }

                zone = player.ActiveGame.World.GetRealm(tokens[0]).GetZone(filename)[0];

                //If no Zone was retrieved (due to it not existing), let the admin know
                //that the Zone filename was not valid.
                if (zone == null)
                {
                    player.Send("Zone Editing canceled. The supplied Zone name is not valid.");
                    return;
                }
                //Otherwise, the Zone does exist and was retrieved.
                //Lets build our Editing menu's and allow for Zone Editing.
                else
                {
                    //Always re-build the menu so the user doesn't need to re-enter the edit command.
                    //When the user selects the exit option, the loop will end.
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
                            player.Send("Zone Editing canceled. The supplied value was not numeric!");
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
            player.Send(Path.GetFileNameWithoutExtension(zone.Filename));
            player.Send("Select from the available options below:");
            player.Send("1: Descriptions");
            player.Send("2: Names");
            player.Send("3: Senses");
            player.Send("4: Initial Zone");
            player.Send("5: Settings");
            player.Send("9: Exit");
            player.Send("Enter numeric selection: ", false);
        }

        /// <summary>
        /// Constructs the Descriptions Editing menu for admins to look at.
        /// </summary>
        private void BuildMenuDescriptions()
        {
            player.FlushConsole();
            player.Send(Path.GetFileNameWithoutExtension(zone.Filename));
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
            player.Send(Path.GetFileNameWithoutExtension(zone.Filename));
            player.Send("When you assign a Zone Name, the Filename is overwrote with your ZoneName as a filename.");
            player.Send("Example: ZoneName of 'ExampleZone' would automatically set a Filename of 'ExampleZone.Zone'");
            player.Send("");

            player.Send("If you wish to have multiple Zones with the same visible name, you will need to specify a different Filename for each Zone.");
            player.Send("Filenames are what you use when accessing objects as a Admin. Typically without the file extension.");
            player.Send("Example: A Zone with a Visible name of \"My Test Zone\" can have a filename of \"Test.Zone\". You would access this object as a Admin by specifying a object name of \"Test\"");
            player.Send("Select from the available options below:");
            player.Send("");

            player.Send("1: Zone Visibile Name");
            player.Send("2: Zone Filename");
            player.Send("9: Exit");
            player.Send("Enter numeric selection: ", false);
        }

        private void BuildMenuSenses()
        {
            player.FlushConsole();
            player.Send(Path.GetFileName(zone.Filename));
            player.Send("Senses are what allow the players to get a better understanding of the environment around them.");
            player.Send("Choose what sense you would like to edit, make your adjustments and press 'ENTER' to save the changes.");
            player.Send("Senses defined for Zones will be applied as the default for every Room created within a Zone.");
            player.Send("Select from the available options below:");
            player.Send("1: Feel Sense");
            player.Send("2: Listen Sense");
            player.Send("3: Smell Sense");
            player.Send("9: Exit");
        }

        private void BuildMenuInitial()
        {
            player.FlushConsole();
            player.Send(zone.Name);
            player.Send("Initial Zone Settings.");
            player.Send("The Initial Zone setting determins if the Zone will be the starting location for all newly created players or not.");
            if (zone.IsInitialZone)
            {
                player.Send("If you disable this Zone from being the Initial Zone, new players will not have a starting location assigned to them.");
                player.Send("You will need to enable Initial Zone on another Zone in order for new players to have a starting location.");
                player.Send("Select from the available options below:");
                player.Send("1: Disable Initial Zone");
            }
            else
            {
                player.Send("If you enable Initial Zone, then new players will start at this location from now on.");
                player.Send("Select from the available options below:");
                player.Send("1: Enable Initial Zone");
            }
            player.Send("9: Exit");
            player.Send("Enter numeric selection: ", false);
        }

        private void BuildMenuSettings()
        {
            player.FlushConsole();
            player.Send(zone.Name);
            player.Send("Zone Settings.");
            player.Send("The following settings are available for you to edit.");
            player.Send("Select from the available options below:");
            player.Send("");
            player.Send("1: Zone is Always Safe.");
            //player.Send("2: Zone drains stats.");
            player.Send("9: Exit");
            player.Send("Enter numeric selection: ", false);
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
                        player.Send("Zone Editing canceled. The supplied value was not numeric!");
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
                        player.Send("Zone Editing canceled. The supplied value was not numeric!");
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
                        player.Send("Zone Editing canceled. The supplied value was not numeric!");
                        return;
                    }

                    ParseInitialSelection(entry);
                    break;
                case 5: //Settings
                    BuildMenuSettings();
                    try
                    {
                        entry = Convert.ToInt32(player.ReadInput());
                    }
                    catch
                    {
                        player.Send("Zone Editing canceled. The supplied value was not numeric!");
                        return;
                    }

                    ParseSettingsSelection(entry);
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
        /// and adjusts the Zones descriptions as specified by the admin
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
                    player.Send("Enter a simple description for this Zone.");
                    player.Send("Simple Descriptions are single line only.");
                    player.Send("To create a blank description, you may simply press ENTER.");
                    player.Send("To cancel editing this description, you may type 'Exit'");
                    if (!String.IsNullOrEmpty(zone.Description))
                        player.Send("Current Description: " + zone.Description);

                    player.Send("Entry: ", false);
                    //Read in the admins new simple description
                    input = player.ReadInput();

                    if (input.ToLower() == "exit")
                        return;
                    else
                        zone.Description = input;
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
                        player.Send("Enter a Detailed Description for this Zone.");
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
                        foreach (String desc in zone.DetailedDescription)
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
                                if (zone.DetailedDescription.Count >= line)
                                {
                                    //Get the new description for this line...
                                    player.Send("New Description: ", false);
                                    input = player.ReadInput();
                                    //-1 correction due to collection index starting at 0 and not 1.
                                    //replace the existing description with the new one.
                                    zone.DetailedDescription[line - 1] = input;
                                }
                                //Let the admin know that the line number specified does not exist.
                                else
                                {
                                    player.Send("Line Editing canceled. The Zone does not contain that many Description lines.");
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
                                zone.DetailedDescription.Clear();
                            }
                            //Admin specified a single line. Find the admins specified line number for clearing.
                            else
                            {
                                //Convert the specified line number to a integer
                                Int32 i = Convert.ToInt32(clear);

                                //make sure the line number provided does in-fact exist.
                                if (zone.DetailedDescription.Count >= i)
                                    //Remove the specified line number for the descriptions collection.
                                    zone.DetailedDescription.Remove(zone.DetailedDescription[i - 1]);
                                //Line provided is larger than the number of lines available to check. Cancel.
                                else
                                    player.Send("Line Clearing canceled. The Zone  does not contain that many description lines.");
                            }
                        }
                        //No special tokens provided, so we assume this line is a description. 
                        //Add the contents to the Zone's detailed description collection.
                        else
                        {
                            zone.DetailedDescription.Add(input);
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
                    player.Send("Enter a new Visible name for this Zone.");
                    player.Send("Enter Value: ", false);

                    //Get the new name for this Zone
                    String newName = player.ReadInput();

                    //Ensure the new name is not blank.
                    if (String.IsNullOrEmpty(newName))
                    {
                        player.Send("Zone Name Editing canceled. No name supplied.");
                    }
                    //We have a valid name, so lets try assigning it to the Zone
                    else
                    {
                        //Check to see if the supplied name already exists by checking for a existing Zone
                        //that has a matching filename.
                        if (player.ActiveGame.World.GetRealm(zone.Realm).GetZone(newName + ".zone").Count == 0)
                        {
                            //No matching Zone was found, so we can go ahead and make the change.
                            //Store the new name within this Zone
                            String oldName = zone.Filename;
                            zone.Name = newName;

                            //Update all of the objects that are within the Zone to have the
                            //new name assigned to them.
                            UpdateZoneObjects(oldName);

                            //TODO: Any Items/NPC's etc within this Zone will need to be updated as well.
                        }
                        else
                        {
                            player.Send("Zone Name Editing canceled. " + newName + " already exists within the Realm.");
                            player.Send("If you want to keep the provided Visible Name for this Zone, you must create a unique Filename.");
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
                                        player.Send("The default filename for this Zone (using the new Visible Name) is currently:");
                                        player.Send(newName + ".Zone");
                                        player.Send("Please supply a different filename:");
                                        player.Send("Enter Value: ", false);

                                        String filename = player.ReadInput();

                                        if (String.IsNullOrEmpty(filename))
                                        {
                                            player.Send("Zone Name Editing Canceled. No filename supplied.");
                                        }
                                        else
                                        {
                                            if (player.ActiveGame.World.GetRealm(zone.Realm).GetZone(filename).Count != 0)
                                            {
                                                player.Send("Zone Name Editing Canceled. A Zone with this filename already exists!");
                                            }
                                            else
                                            {
                                                String oldName = zone.Filename;
                                                zone.Name = newName;
                                                zone.Filename = filename;

                                                UpdateZoneObjects(oldName);
                                            }
                                        }
                                        break;
                                    case 2:
                                        player.Send("Zone Name Editing Canceled. A Zone with this filename already exists!");
                                        break;
                                }
                            }
                            catch
                            {
                                player.Send("Zone Name Editing canceled. The supplied value was not numeric.");
                            }
                        }
                    }
                    player.ActiveGame.Save();
                    break;
                case 2: //Zone Filename
                    player.FlushConsole();
                    player.Send("Enter a new Filenamename for this Zone.");
                    player.Send("Enter Value: ", false);

                    String fname = player.ReadInput();

                    if (String.IsNullOrEmpty(fname))
                    {
                        player.Send("Zone Name Editing canceled. No name supplied.");
                    }
                    else if (player.ActiveGame.World.GetRealm(zone.Realm).GetZone(fname) != null)
                    {
                        player.Send("Zone Name Editing canceled. A Zone with this filename already exists!");
                    }
                    else
                    {
                        String oldName = "";
                        oldName = zone.Filename;
                        zone.Filename = fname;
                        UpdateZoneObjects(oldName);
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
                    player.Send("Enter the new default FEEL description for this Zone.");
                    player.Send("If you wish to clear the current description, just press ENTER to save a blank description.");

                    if (!String.IsNullOrEmpty(zone.Feel))
                    {
                        player.Send("Current Value: ", false);
                        player.Send(zone.Feel);
                    }

                    player.Send("Enter Value: ", false);
                    zone.Feel = player.ReadInput();
                    break;
                case 2: //Listen
                    player.Send("Enter the new default LISTEN description for this Zone.");
                    player.Send("If you wish to clear the current description, just press ENTER to save a blank description.");

                    if (!String.IsNullOrEmpty(zone.Listen))
                    {
                        player.Send("Current Value: ", false);
                        player.Send(zone.Listen);
                    }

                    player.Send("Enter value: ", false);
                    zone.Listen = player.ReadInput();
                    break;
                case 3: //Smell
                    player.Send("Enter the new default SMELL description for this Zone.");
                    player.Send("If you wish to clear the current description, just press ENTER to save a blank description.");

                    if (!String.IsNullOrEmpty(zone.Smell))
                    {
                        player.Send("Current Value: ", false);
                        player.Send(zone.Smell);
                    }

                    player.Send("Enter value: ", false);
                    zone.Smell = player.ReadInput();
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
                        player.Send("Zone Initial Realm editing canceled. You must have a Initial Realm defined before setting a Initial Zone.");
                        return;
                    }

                    if (zone.IsInitialZone)
                    {
                        zone.IsInitialZone = false;
                        player.ActiveGame.InitialRealm.InitialZone = null;
                    }
                    else
                    {
                        zone.IsInitialZone = true;
                        player.ActiveGame.InitialRealm.InitialZone = zone;
                    }

                    break;
                case 9: //Exit
                    return;
            }

            player.ActiveGame.Save();
        }

        private void ParseSettingsSelection(Int32 value)
        {
            Int32 input = 0;
            switch (value)
            {
                case 1://IsSafe property
                    player.FlushConsole();
                    player.Send("Setting a Zone to SAFE will allow players to traverse the Zone without fear of being attacked by Monsters.");
                    player.Send("Even if there are Monsters in the Zone.");
                    player.Send("This feature can be extended on by writing a script that checks the current time of day, and disables SAFE zone settings at certain times (such as at night) allowing for monsters to attack players at night, but not day.");
                    player.Send("All Rooms created within this Zone will default to this value, however a Room may override this option.");
                    player.Send("Please select from one of the available options below:");

                    if (zone.IsSafe)
                        player.Send("1: Disable Safe Zone");
                    else
                        player.Send("1: Enable Safe Zone");

                    player.Send("9: Exit");

                    try
                    {
                        input = Convert.ToInt32(player.ReadInput());
                    }
                    catch
                    {
                        player.Send("Zone Editor Canceled. Entry must be in a numeric format.");
                        return;
                    }

                    switch (input)
                    {
                        case 1:
                            if (zone.IsSafe)
                                zone.IsSafe = false;
                            else
                                zone.IsSafe = true;
                            break;
                        case 9:
                            break;
                    }
                    break;
                case 2: //StatDrain property
                    player.FlushConsole();
                    player.Send("Enabling STAT DRAIN will cause any Room within the Zone to drain or increase the specified stats on a player.");
                    player.Send("Please select from the available options below:");

                    if (zone.StatDrain)
                        player.Send("1: Disable");
                    else
                        player.Send("1: Enable");

                    player.Send("2: Edit Affected Stats");
                    player.Send("9: Exit");

                    try
                    {
                        input = Convert.ToInt32(player.ReadInput());
                    }
                    catch
                    {
                        player.Send("Zone Editor Canceled. Entry must be in a numeric format.");
                        return;
                    }

                    switch (input)
                    {
                        case 1:
                            if (zone.StatDrain)
                                zone.StatDrain = false;
                            else
                                zone.StatDrain = true;
                            break;
                        case 2:
                            player.Send("Select which Stat you would like to edit.");
                            //TODO: Implement stat affects.
                            break;
                        case 9:
                            break;
                    }
                    break;
            }

            player.ActiveGame.Save();
        }

        private void UpdateZoneObjects(String oldName)
        {
            //Check if this Zone is the initial Zone. If so then we need to update the
            //current Game so that when it launches, it checks the new Zone and not the old
            //one.
            if ((zone.IsInitialZone) && (player.ActiveGame.InitialRealm.Filename == zone.Realm))
            {
                player.ActiveGame.InitialRealm.InitialZone = zone;
            }
            else
            {
                player.Send("Error: Unable to set this Zone as the initial Zone due to the parent Realm not being a Initial Realm.");
            }

            //Loop through each player currently logged in and see if they are currently
            //within the selected Zone. If they are then we need to change their current
            //Zone to the new one.
            foreach (BaseCharacter p in player.ActiveGame.GetPlayerCollection())
            {
                if (p.Name.StartsWith("Base"))
                    continue;

                if (p.CurrentRoom.Zone == oldName)
                {
                    p.CurrentRoom.Zone = zone.Filename;
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
                if (ch.CurrentRoom.Zone == oldName)
                {
                    ch.CurrentRoom.Zone = zone.Filename;
                    ch.Save();
                }
            }

            //Next, we need to loop through every Room within this Zone
            //and update their Zones names to match the new one
            foreach (Realm realm in player.ActiveGame.World.RealmCollection)
            {
                if (realm.InitialZone.Filename == oldName)
                    realm.InitialZone = zone;

                foreach (Zone z in realm.ZoneCollection)
                {
                    foreach (Room r in zone.RoomCollection)
                    {
                        if (r.Zone == oldName)
                            r.Zone = zone.Filename;

                        foreach (Door d in r.Doorways)
                        {
                            if (d.ArrivalRoom.Zone == oldName)
                                d.ArrivalRoom.Zone = zone.Filename;
                            if (d.DepartureRoom.Zone == oldName)
                                d.DepartureRoom.Zone = zone.Filename;
                        }
                    }
                }
            }
        }
    }
}
